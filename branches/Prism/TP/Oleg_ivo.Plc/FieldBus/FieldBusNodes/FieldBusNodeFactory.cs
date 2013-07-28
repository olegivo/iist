using System.Collections.Generic;
using System.Linq;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    ///<summary>
    /// Фабрика узлов полевой шины
    ///</summary>
    public abstract class FieldBusNodeFactory : IFieldBusNodeFactory
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        ///<summary>
        ///
        ///</summary>
        private readonly int _minimumRetries;

        ///<summary>
        ///
        ///</summary>
        private readonly int _minimumWaitToRetryMilliseconds;

        private readonly IFieldBusFactory fieldBusFactory;

        ///<summary>
        ///
        ///</summary>
        protected FieldBusNodeFactory(IPlcFactory plcFactory, IFieldBusFactory fieldBusFactory)
        {
            PlcFactory = Enforce.ArgumentNotNull(plcFactory, "plcFactory");
            this.fieldBusFactory = Enforce.ArgumentNotNull(fieldBusFactory, "fieldBusFactory");
            _minimumRetries = 3;
            _minimumWaitToRetryMilliseconds = 3;
        }

        #region fields

        #endregion

        #region properties

        #endregion

        #region methods

        ///<summary>
        /// Построить узлы полевой шины
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<returns></returns>
        public FieldBusNodeCollection BuildFieldBusNodes(FieldBusManager fieldBusManager)
        {
            var fieldNodesLevel = fieldBusManager.FieldBusLoadOptions.FieldNodesLevel;
            //загружаем узлы полевой шины из сохранённой конфигурации
            FieldBusNodeCollection loadedFieldBusNodes = fieldNodesLevel.LoadSavedConfiguration
                                                             ? LoadFieldBusNodes(fieldBusManager)
                                                             : new FieldBusNodeCollection();

            //определяем узлы полевой шины из текущей конфигурации
            FieldBusNodeCollection currentFieldBusNodes;

            if (fieldNodesLevel.ComputeCurrentConfiguration)
            {
                FieldBusType fieldBusType = fieldBusManager.FieldBusType;

                ActiveFieldBusManager activeFieldBusManager = fieldBusManager as ActiveFieldBusManager;
                if (activeFieldBusManager != null)
                {
                    activeFieldBusManager.InitializeModbusMaster();
                    currentFieldBusNodes = FindNodes(activeFieldBusManager, activeFieldBusManager.FieldBusAccessor as ModbusAccessor);
                }
                else
                {
                    currentFieldBusNodes = new FieldBusNodeCollection();
                    currentFieldBusNodes.AddRange((from address in fieldBusManager.GetPLCAddressRange()
                                                   let modbusAccessor = fieldBusFactory.CreateFieldbusAccessor(fieldBusType, address)
                                                   select new ActiveFieldBusNode(fieldBusManager, modbusAccessor, address)));
                }

            }
            else
            {
                currentFieldBusNodes = new FieldBusNodeCollection();
            }

            if (currentFieldBusNodes.Count > 0 && loadedFieldBusNodes.Count > 0)
            {
                //изменяем текущую конфигурацию, учитывая загруженную
                foreach (var currentFieldBusNode in currentFieldBusNodes)
                {
                    //ищем загруженный узел, совпадающий с текущим
                    var find =
                        loadedFieldBusNodes.FirstOrDefault(
                            loadedFieldBusNode =>
                            loadedFieldBusNode.EqualsPredicate(currentFieldBusNode));

                    if (find != null && currentFieldBusNode.Id != find.Id)//переносим данные из загруженного узла в текущий
                    {
                        currentFieldBusNode.Id = find.Id;
                    }
                }
            }
            else if (loadedFieldBusNodes.Count > 0)
            {
                //узлы в текущей конфигурации отсутствуют, добавляем их из загруженной
                currentFieldBusNodes = loadedFieldBusNodes;
            }

            InitFieldBusNodesPlc(currentFieldBusNodes);

            return currentFieldBusNodes;
        }

        /// <summary>
        /// Загрузить настроенные узлы для полевой шины
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <returns></returns>
        public abstract FieldBusNodeCollection LoadFieldBusNodes(FieldBusManager fieldBusManager);

        /// <summary>
        /// Создать ПЛК для узлов полевой шины
        /// </summary>
        /// <param name="fieldBusNodes"></param>
        private void InitFieldBusNodesPlc(IEnumerable<FieldBusNode> fieldBusNodes)
        {
            if (fieldBusNodes != null)
                foreach (FieldBusNode fieldBusNode in fieldBusNodes)
                    CreatePlcForFieldBusNode(fieldBusNode);
        }

        /// <summary>
        /// Создать ПЛК для узла полевой шины
        /// </summary>
        /// <param name="fieldBusNode"></param>
        protected abstract void CreatePlcForFieldBusNode(FieldBusNode fieldBusNode);

        #endregion

        ///<summary>
        /// Проверить, корректен ли ПЛК для добавления в коллекцию узлов полевой шины
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        protected abstract bool CheckPLC(PLC plc);

        ///<summary>
        /// Найти узлы полевой шины, подключенные к указанной полевой шине
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<param name="modbusAccessor"></param>
        ///<returns></returns>
        public FieldBusNodeCollection FindNodes(FieldBusManager fieldBusManager, ModbusAccessor modbusAccessor)
        {
            Log.Debug("Поиск устройств, подключенные к порту " + modbusAccessor.PortName);
            FieldBusNodeCollection nodes = new FieldBusNodeCollection();

            // установка режима минимального ожидания
            ModbusAccessorTimeouts oldModbusAccessorTimeouts = modbusAccessor.ModbusAccessorTimeouts;
            ModbusAccessorTimeouts minimumModbusAccessorTimeouts = new ModbusAccessorTimeouts
            {
                WaitToRetryMilliseconds = _minimumWaitToRetryMilliseconds,
                Retries = _minimumRetries,
                ReadTimeout = 100,
                WriteTimeout = 100

            };

            try
            {
                modbusAccessor.ModbusAccessorTimeouts = minimumModbusAccessorTimeouts;

                // опрос портов с целью поиска ведомых устройств
                // с первого по последний возможный порт: 
                // если устройство по данному адресу ответило, значит создаём объект-устройство и его адрес
                foreach (FieldBusNodeAddress fieldBusNodeAddress in fieldBusManager.GetPLCAddressRange())
                {
                    FieldBusNode fieldBusNode = CreateFieldBusNode(fieldBusManager, fieldBusNodeAddress);

                    if (TryGetReply(fieldBusNode))
                    {
                        Log.Debug("\nУстройство по адресу {0} ответило на все запросы.\n",
                                        fieldBusNodeAddress.SlaveAddress);
                        nodes.Add(fieldBusNode);
                        //PLC plc =
                        //    DistributedMeasurementInformationSystemBase.Instance.PLCManager.PlcFactory.CreatePLC(
                        //        fieldBusNodeAddress, this);

                        //if (plc != null)
                        //    devices.Add(plc);
                    }
                    else
                    {
                        Log.Debug("\nУстройство по адресу {0} не ответило на все запросы.\n",
                                        fieldBusNodeAddress.SlaveAddress);
                    }
                }
            }
            finally
            {
                // снятие режима минимального ожидания
                modbusAccessor.ModbusAccessorTimeouts = oldModbusAccessorTimeouts;
            }

            return nodes;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<param name="fieldBusNodeAddress"></param>
        ///<returns></returns>
        public FieldBusNode CreateFieldBusNode(FieldBusManager fieldBusManager, FieldBusNodeAddress fieldBusNodeAddress)
        {
            FieldBusNode fieldBusNode;
            bool isNodeActive = false;
            switch (fieldBusManager.FieldBusType)
            {
                case FieldBusType.Ethernet:
                    isNodeActive = true;
                    break;
            }

            if (isNodeActive)
            {
                Log.Debug("создаём активный узел полевой шины...");
                IFieldBusAccessor fieldBusAccessor = fieldBusFactory.CreateFieldbusAccessor(fieldBusManager.FieldBusType, fieldBusNodeAddress);
                fieldBusNode = new ActiveFieldBusNode(fieldBusManager, fieldBusAccessor, fieldBusNodeAddress);
            }
            else
            {
                Log.Debug("создаём узел полевой шины...");
                fieldBusNode = new FieldBusNode(fieldBusManager, fieldBusNodeAddress);
            }
            return fieldBusNode;
        }

        /// <summary>
        /// Фабрика ПЛК
        /// </summary>
        public IPlcFactory PlcFactory { get; private set; }


        /// <summary>
        /// Попытка получить ответ от устройства по данному адресу
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <returns></returns>
        protected abstract bool TryGetReply(IFieldBusNodeAccessor fieldBusNode);
    }
}