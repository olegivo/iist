using System;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Common;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    ///<summary>
    /// Узел полевой шины.
    /// Реализует доступ к ресурсам узла полевой шины.
    ///</summary>
    public class FieldBusNode : IFieldBusNodeAccessor, IIdentified
    {
        private readonly FieldBusManager _fieldBusManager;

        #region fields

        private readonly FieldBusNodeAddress _fieldBusNodeAddress;
        private PLC _plc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <param name="fieldBusNodeAddress"></param>
        public FieldBusNode(FieldBusManager fieldBusManager, FieldBusNodeAddress fieldBusNodeAddress)
        {
            if (this is ActiveFieldBusNode && fieldBusManager is ActiveFieldBusManager) throw new InvalidOperationException("fieldBusManager");
            _fieldBusManager = fieldBusManager;
            _fieldBusNodeAddress = fieldBusNodeAddress;
        }

        #endregion

        #region properties

        /// <summary>
        /// ПЛК
        /// </summary>
// ReSharper disable MemberCanBePrivate.Global
        public PLC Plc
// ReSharper restore MemberCanBePrivate.Global
        {
            get { return _plc; }
        }

        /// <summary>
        /// Диспетчер полевой шины
        /// </summary>
        public FieldBusManager FieldBusManager
        {
            get { return _fieldBusManager; }
        }

        ///<summary>
        /// Физические каналы узла сети полевой шины
        ///</summary>
        public PhysicalChannelCollection PhysicalChannels { get; set; }

        ///<summary>
        /// Логические каналы данного узла полевой шины
        ///</summary>
        public LogicalChannelCollection LogicalChannels
        {
            get
            {
                LogicalChannelCollection channels = new LogicalChannelCollection();

                if (PhysicalChannels != null)
                    foreach (PhysicalChannel physicalChannel in PhysicalChannels)
                    {
                        if (physicalChannel.LogicalChannels != null)
                            channels.AddRange(physicalChannel.LogicalChannels);
                    }
                else
                    Console.WriteLine("{0}: физические каналы не определены", this);


                return channels;
            }
        }

        ///<summary>
        /// Адрес узла в полевой шине
        ///</summary>
        public FieldBusNodeAddress FieldBusNodeAddress
        {
            get { return _fieldBusNodeAddress; }
        }

        /// <summary>
        /// Компонент доступа к ресурсам полевой шины
        /// </summary>
        public IFieldBusAccessor FieldBusAccessor
        {
            get { return FieldBusManager; }
        }

        #endregion

        #region constructors

        #endregion

        #region methods

        /// <summary>
        /// Построить физические каналы для узла полевой шины
        /// </summary>
        public void BuildPhysicalChannels()
        {
            if (Plc != null)//ПЛК ответственнен за построение модулей ввода-вывода
            {
                Plc.BuildPhysicalChannels();//построение физических каналов
            }

            //построение логических каналов
            BuildLogicalChannels();
        }

        /// <summary>
        /// Построить логические каналы для данного узла полевой шины
        /// </summary>
        private void BuildLogicalChannels()
        {
            //загрузка логических каналов
            LoadLogicalChannels();
            
            BuildDefaultLogicalChannels();
        }

        #endregion

        #region Implementation of IFieldBusNodeAccessor

        /// <summary>
        /// Прочитать регистры
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual ushort[] ReadHoldingRegisters(ushort address, ushort numberOfPoints)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.ReadHoldingRegisters(this, address, numberOfPoints);
            throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
        }

        /// <summary>
        /// Прочитать входные регистры
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual ushort[] ReadInputRegisters(ushort address, ushort numberOfPoints)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.ReadInputRegisters(this, address, numberOfPoints);
            throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
        }

        ///<summary>
        /// Записать в регистр
        ///</summary>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        public virtual bool WriteSingleRegister(ushort address, ushort value)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteSingleRegister(this, address, value);
            throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
        }

        /// <summary>
        /// Прочитать биты
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual bool[] ReadCoils(ushort address, ushort numberOfPoints)
        {
            if (FieldBusManager != null) 
                return FieldBusManager.ReadCoils(this, address, numberOfPoints);
            throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
        }

        /// <summary>
        /// Записать один бит
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool WriteSingleCoil(ushort address, bool value)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteSingleCoil(this, address, value);
            throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
        }

        ///<summary>
        /// Записать несколько регистров
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public virtual bool WriteMultipleRegisters(ushort address, ushort[] values)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteMultipleRegisters(this, address, values);
            throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
        }

        ///<summary>
        /// Записать несколько битов
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public virtual bool WriteMultipleCoils(ushort address, bool[] values)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteMultipleCoils(this, address, values);
            throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
        }

        /// <summary>
        /// Инициализировать управление по Modbus 
        /// </summary>
        public virtual void InitializeModbusMaster()
        {
            if ((FieldBusManager!=null && FieldBusManager is ActiveFieldBusManager))
            {
                FieldBusManager.InitializeModbusMaster();
            }
            else
                throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");

        }

        ///<summary>
        /// Узел полевой шины онлайн
        ///</summary>
        public virtual bool IsOnline
        {
            get
            {
                ActiveFieldBusManager activeFieldBusManager = FieldBusManager as ActiveFieldBusManager;
                if (activeFieldBusManager != null)
                    return FieldBusManager.IsOnline;

                throw new InvalidOperationException("Не доступен диспетчер полевой шины. Операция не может быть выполнена.");
            }
            protected set { throw new NotImplementedException(); }
        }

        ///<summary>
        /// Проверить доступность узла полевой шины
        ///</summary>
        ///<returns></returns>
        public virtual bool CheckOnline()
        {
            return FieldBusManager.CheckOnline();
        }

        #endregion

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Plc!=null ? Plc.ToString() : "ПЛК не определён";
        }

        /// <summary>
        /// Инициализировать узел полевой шины контроллером
        /// </summary>
        /// <param name="plc"></param>
        internal protected /*virtual*/ void InitPlc(PLC plc)
        {
            _plc = plc;
        }

        ///<summary>
        /// Построить логические каналы по умолчанию для данного узла полевой шины
        ///</summary>
        public void BuildDefaultLogicalChannels()
        {
            if (PhysicalChannels != null)
                foreach (PhysicalChannel physicalChannel in PhysicalChannels)
                    physicalChannel.BuildDefaultLogicalChannels();
            else
                Console.WriteLine("{0}: физические каналы не найдены. Построить логические каналы по умолчанию нельзя.", this);

        }

        ///<summary>
        /// Загрузить настроенные логические каналы для данного узла полевой шины
        ///</summary>
        public void LoadLogicalChannels()
        {
            if (FieldBusManager.FieldBusLoadOptions.LogicalChannelsLevel.LoadSavedConfiguration && PhysicalChannels != null)
                foreach (PhysicalChannel physicalChannel in PhysicalChannels)
                    physicalChannel.LoadLogicalChannels();
            else
                Console.WriteLine("{0}: физические каналы не найдены. Загрузить логические каналы нельзя.", this);
        }

        ///<summary>
        /// Идентификатор
        ///</summary>
        public int Id
        {
            get { return FieldBusNodeAddress.Id; }
            set { FieldBusNodeAddress.Id = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <returns></returns>
        public bool EqualsPredicate(FieldBusNode fieldBusNode)
        {
            return FieldBusNodeAddress == fieldBusNode.FieldBusNodeAddress;
        }
    }
}