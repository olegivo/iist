using System.Collections.Generic;
using System.Linq;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.Tools.Utils;

namespace Oleg_ivo.Plc.FieldBus.FieldBusManagers
{
    ///<summary>
    /// Диспетчер полевой шины.
    /// Реализует доступ к ресурсам полевой шины.
    ///</summary>
    public class FieldBusManager : IFieldBusAccessor
    {

        #region fields

        private readonly FieldBusType _fieldBusType;
        private readonly IDistributedMeasurementInformationSystem dmis;

        #endregion

        #region properties

        ///<summary>
        /// Диапазон адресов на шине
        ///</summary>
        public FieldBusNodeAddressCollection FieldBusAddresses
        {
            get
            {
                return FieldBusLoadOptions.FieldBusNodeAddresses;
            }
        }

        ///<summary>
        /// Получить диапазон адресов для данного порта
        ///</summary>
        ///<returns></returns>
        public virtual IEnumerable<FieldBusNodeAddress> GetPLCAddressRange()
        {
            FieldBusNodeAddressCollection plcAddresses = new FieldBusNodeAddressCollection();
            switch (FieldBusType)
            {
                case FieldBusType.Ethernet:
                    plcAddresses.AddRange(FieldBusAddresses.Cast<FieldBusNodeIpAddress>().Cast<FieldBusNodeAddress>());
                    break;
            }
            return plcAddresses.ToArray();

        }


        ///<summary>
        ///
        ///</summary>
        public virtual FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
        }

        ///<summary>
        /// Узлы полевой шины данного, подключенных к порту
        ///</summary>
        public FieldBusNodeCollection FieldBusNodes { get; private set; }

        ///<summary>
        ///
        ///</summary>
        public IEnumerable<LogicalChannel> LogicalChannels
        {
            get
            {
                LogicalChannelCollection channels = new LogicalChannelCollection();
                
                foreach (FieldBusNode fieldBusNode in FieldBusNodes)
                {
                    if (fieldBusNode.LogicalChannels != null) 
                        channels.AddRange(fieldBusNode.LogicalChannels);
                }

                return channels;
            }
        }

        #endregion

        #region constructors

        ///<summary>
        /// Диспетчер полевой шины
        ///</summary>
        public FieldBusManager(FieldBusType fieldBusType, IDistributedMeasurementInformationSystem dmis):this()
        {
            this.dmis = dmis;
            _fieldBusType = fieldBusType;
        }

        /// <summary>
        /// Для дизайнера
        /// </summary>
        protected FieldBusManager()
        {
            FieldBusNodes = new FieldBusNodeCollection();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public FieldBusLoadOptions FieldBusLoadOptions
        {
            get
            {
                return dmis.Settings.FieldBusLoadOptions[FieldBusType];
            }
        }

        ///<summary>
        /// Полевая шина онлайн
        ///</summary>
        public bool IsOnline { get; protected set; }

        ///<summary>
        /// Построить узлы полевой шины для порта
        ///</summary>
        ///<param name="fieldBusNodesFactory">Фабрика для построения узлов полевых шин</param>
        public void BuildFieldBusNodes(IFieldBusNodeFactory fieldBusNodesFactory)
        {
            FieldBusNodeCollection fieldBusNodes = fieldBusNodesFactory.BuildFieldBusNodes(this);

            FieldBusNodes = fieldBusNodes;

            if (FieldBusLoadOptions.PhysicalChannelsLevel.IsNeedComputeOrLoad)
                BuildPhysicalChannels();
        }

        /// <summary>
        /// Построить физические каналы для всех узлов данной полевой шины
        /// </summary>
        protected void BuildPhysicalChannels()
        {
            if (FieldBusNodes != null)
                foreach (FieldBusNode fieldNode in FieldBusNodes)
                    fieldNode.BuildPhysicalChannels();
        }

        ///<summary>
        /// Построить логические каналы по умолчанию для данной полевой шины
        ///</summary>
        public void BuildDefaultLogicalChannels()
        {
            foreach (FieldBusNode fieldBusNode in FieldBusNodes)
            {
                fieldBusNode.BuildDefaultLogicalChannels();
            }
        }

        ///<summary>
        /// Загрузить настроенные логические каналы для данной полевой шины
        ///</summary>
        public void LoadLogicalChannels()
        {
            foreach (FieldBusNode fieldBusNode in FieldBusNodes)
            {
                fieldBusNode.LoadLogicalChannels();
            }
        }

        #region Implementation of IFieldBusAccessor

        ///<summary>
        /// Проверить доступность полевой шины
        ///</summary>
        ///<returns></returns>
        public virtual bool CheckOnline()
        {
            bool result = false;

            //если хоть один узел онлайн, значит вся полевая шина тоже онлайн

            foreach (ActiveFieldBusNode fieldBusNode in FieldBusNodes.OfType<ActiveFieldBusNode>())
            {
                result |= fieldBusNode.CheckOnline();
                if (result) break;
            }

            return (IsOnline = result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual ushort[] ReadHoldingRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            return fieldBusNodeAccessor.ReadHoldingRegisters(address, numberOfPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual ushort[] ReadInputRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            return fieldBusNodeAccessor.ReadInputRegisters(address, numberOfPoints);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        public virtual bool WriteSingleRegister(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort value)
        {
            return fieldBusNodeAccessor.WriteSingleRegister(address, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual bool[] ReadCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            return fieldBusNodeAccessor.ReadCoils(address, numberOfPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool WriteSingleCoil(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool value)
        {
            return fieldBusNodeAccessor.WriteSingleCoil(address, value);
        }

        /// <summary>
        /// Инициализировать управление по Modbus 
        /// </summary>
        public virtual void InitializeModbusMaster()
        {
            if (FieldBusNodes != null)
                foreach (FieldBusNode fieldBusNode in FieldBusNodes)
                {
                    fieldBusNode.InitializeModbusMaster();
                }
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public virtual bool WriteMultipleRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort[] values)
        {
            return fieldBusNodeAccessor.WriteMultipleRegisters(address, values);
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public virtual bool WriteMultipleCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool[] values)
        {
            return fieldBusNodeAccessor.WriteMultipleCoils(address, values);
        }

        #endregion

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            if (FieldBusNodes != null)
                foreach (FieldBusNode fieldBusNode in FieldBusNodes)
                {
                    fieldBusNode.Dispose();
                }
        }

        /// <summary>
        /// Возвращает объект <see cref="T:System.String"/>, который представляет текущий объект <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.String"/>, представляющий текущий объект <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("{0}", Reflection.GetDescription(FieldBusType));
        }
    }
}