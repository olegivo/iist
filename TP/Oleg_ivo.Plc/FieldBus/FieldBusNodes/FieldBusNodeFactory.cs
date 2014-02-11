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
    /// ������� ����� ������� ����
    ///</summary>
    public abstract class FieldBusNodeFactory : FactoryBase, IFieldBusNodeFactory
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
        /// ��������� ���� ������� ����
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<returns></returns>
        public FieldBusNodeCollection BuildFieldBusNodes(FieldBusManager fieldBusManager)
        {
            var fieldNodesLevel = fieldBusManager.FieldBusLoadOptions.FieldNodesLevel;
            //��������� ���� ������� ���� �� ���������� ������������
            FieldBusNodeCollection loadedFieldBusNodes = fieldNodesLevel.LoadSavedConfiguration
                                                             ? LoadFieldBusNodes(fieldBusManager)
                                                             : new FieldBusNodeCollection();

            //���������� ���� ������� ���� �� ������� ������������
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
                    currentFieldBusNodes =
                        new FieldBusNodeCollection(
                            fieldBusManager.GetPLCAddressRange()
                                .Select(
                                    fieldBusNodeAddress =>
                                    {
                                        var modbusAccessor = fieldBusFactory.CreateFieldbusAccessor(fieldBusType, fieldBusNodeAddress);
                                        var entity = new Entities.FieldBusNode
                                        {
                                            AddressPart1 = fieldBusNodeAddress.AddressPart1,
                                            AddressPart2 = fieldBusNodeAddress.AddressPart2,
                                            Id = fieldBusNodeAddress.Id,
                                            FieldBusId = fieldBusManager.Entity.Id,
                                            FieldBusTypeId = fieldBusManager.Entity.FieldBusTypeId
                                        };
                                        return new ActiveFieldBusNode(fieldBusManager, modbusAccessor, entity);
                                    }));
                }

            }
            else
            {
                currentFieldBusNodes = new FieldBusNodeCollection();
            }

            if (currentFieldBusNodes.Count > 0 && loadedFieldBusNodes.Count > 0)
            {
                //�������� ������� ������������, �������� �����������
                foreach (var currentFieldBusNode in currentFieldBusNodes)
                {
                    //���� ����������� ����, ����������� � �������
                    var find =
                        loadedFieldBusNodes.FirstOrDefault(
                            loadedFieldBusNode =>
                            loadedFieldBusNode.EqualsPredicate(currentFieldBusNode));

                    if (find != null && currentFieldBusNode.Id != find.Id)//��������� ������ �� ������������ ���� � �������
                    {
                        currentFieldBusNode.Id = find.Id;
                    }
                }
            }
            else if (loadedFieldBusNodes.Count > 0)
            {
                //���� � ������� ������������ �����������, ��������� �� �� �����������
                currentFieldBusNodes = loadedFieldBusNodes;
            }

            InitFieldBusNodesPlc(currentFieldBusNodes);

            return currentFieldBusNodes;
        }

        /// <summary>
        /// ��������� ����������� ���� ��� ������� ����
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <returns></returns>
        public abstract FieldBusNodeCollection LoadFieldBusNodes(FieldBusManager fieldBusManager);

        /// <summary>
        /// ������� ��� ��� ����� ������� ����
        /// </summary>
        /// <param name="fieldBusNodes"></param>
        private void InitFieldBusNodesPlc(IEnumerable<FieldBusNode> fieldBusNodes)
        {
            if (fieldBusNodes != null)
                foreach (FieldBusNode fieldBusNode in fieldBusNodes)
                    CreatePlcForFieldBusNode(fieldBusNode);
        }

        /// <summary>
        /// ������� ��� ��� ���� ������� ����
        /// </summary>
        /// <param name="fieldBusNode"></param>
        protected abstract void CreatePlcForFieldBusNode(FieldBusNode fieldBusNode);

        #endregion

        ///<summary>
        /// ���������, ��������� �� ��� ��� ���������� � ��������� ����� ������� ����
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        protected abstract bool CheckPLC(PLC plc);

        ///<summary>
        /// ����� ���� ������� ����, ������������ � ��������� ������� ����
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<param name="modbusAccessor"></param>
        ///<returns></returns>
        public FieldBusNodeCollection FindNodes(FieldBusManager fieldBusManager, ModbusAccessor modbusAccessor)
        {
            Log.Debug("����� ���������, ������������ � ����� " + modbusAccessor.PortName);
            FieldBusNodeCollection nodes = new FieldBusNodeCollection();

            // ��������� ������ ������������ ��������
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

                // ����� ������ � ����� ������ ������� ���������
                // � ������� �� ��������� ��������� ����: 
                // ���� ���������� �� ������� ������ ��������, ������ ������ ������-���������� � ��� �����
                foreach (FieldBusNodeAddress fieldBusNodeAddress in fieldBusManager.GetPLCAddressRange())
                {
                    var entity = new Entities.FieldBusNode
                    {
                        AddressPart1 = fieldBusNodeAddress.AddressPart1,
                        AddressPart2 = fieldBusNodeAddress.AddressPart2,
                        Id = fieldBusNodeAddress.Id,
                        FieldBusId = fieldBusManager.Entity.Id,
                        FieldBusTypeId = fieldBusManager.Entity.FieldBusTypeId
                    };
                    FieldBusNode fieldBusNode = CreateFieldBusNode(fieldBusManager, entity);

                    if (TryGetReply(fieldBusNode))
                    {
                        Log.Debug("\n���������� �� ������ {0} �������� �� ��� �������.\n",
                                        fieldBusNodeAddress.AddressPart2);
                        nodes.Add(fieldBusNode);
                        //PLC plc =
                        //    DistributedMeasurementInformationSystemBase.Instance.PLCManager.PlcFactory.CreatePLC(
                        //        fieldBusNodeAddress, this);

                        //if (plc != null)
                        //    devices.Add(plc);
                    }
                    else
                    {
                        Log.Debug("\n���������� �� ������ {0} �� �������� �� ��� �������.\n",
                                        fieldBusNodeAddress.AddressPart2);
                    }
                }
            }
            finally
            {
                // ������ ������ ������������ ��������
                modbusAccessor.ModbusAccessorTimeouts = oldModbusAccessorTimeouts;
            }

            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public FieldBusNode CreateFieldBusNode(FieldBusManager fieldBusManager, Entities.FieldBusNode entity)
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
                Log.Debug("������ �������� ���� ������� ����...");
                var fieldBusAccessor = fieldBusFactory.CreateFieldbusAccessor(fieldBusManager.FieldBusType, entity.AddressPart1);
                fieldBusNode = new ActiveFieldBusNode(fieldBusManager, fieldBusAccessor, entity);
            }
            else
            {
                Log.Debug("������ ���� ������� ����...");
                fieldBusNode = new FieldBusNode(fieldBusManager, entity);
            }
            return fieldBusNode;
        }

        /// <summary>
        /// ������� ���
        /// </summary>
        public IPlcFactory PlcFactory { get; private set; }


        /// <summary>
        /// ������� �������� ����� �� ���������� �� ������� ������
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <returns></returns>
        protected abstract bool TryGetReply(IFieldBusNodeAccessor fieldBusNode);

        /// <summary>
        /// TODO: ������ ����������� ������ (�������� � ����������)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract FieldBusNodeAddress GetFieldBusNodeAddress(Entities.FieldBusNode entity);
    }
}