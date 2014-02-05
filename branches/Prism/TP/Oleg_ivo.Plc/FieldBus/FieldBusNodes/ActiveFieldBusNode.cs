using System;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    ///<summary>
    /// Узел полевой шины, управляющий своими ресурсами сам
    ///</summary>
    public class ActiveFieldBusNode : FieldBusNode
    {
        private readonly IFieldBusAccessor _fieldBusAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <param name="fieldBusAccessor"></param>
        /// <param name="fieldBusNodeAddress"></param>
        /// <param name="entity"></param>
        public ActiveFieldBusNode(FieldBusManager fieldBusManager, IFieldBusAccessor fieldBusAccessor, FieldBusNodeAddress fieldBusNodeAddress, Entities.FieldBusNode entity)
            : base(fieldBusManager, fieldBusNodeAddress, entity)
        {
            if (fieldBusAccessor == null) throw new ArgumentNullException("fieldBusAccessor");
            if (fieldBusManager is ActiveFieldBusManager) throw new ArgumentNullException("fieldBusManager");
            _fieldBusAccessor = fieldBusAccessor;
        }

        #region Implementation of IFieldBusAccessor

        /// <summary>
        /// Прочитать регистры
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public override ushort[] ReadHoldingRegisters(ushort address, ushort numberOfPoints)
        {
            return _fieldBusAccessor.ReadHoldingRegisters(this, address, numberOfPoints);
        }

        /// <summary>
        /// Прочитать входные регистры
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public override ushort[] ReadInputRegisters(ushort address, ushort numberOfPoints)
        {
            return _fieldBusAccessor.ReadInputRegisters(this, address, numberOfPoints);
        }

        ///<summary>
        /// Записать в регистр
        ///</summary>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        public override bool WriteSingleRegister(ushort address, ushort value)
        {
            return _fieldBusAccessor.WriteSingleRegister(this, address, value);
        }

        /// <summary>
        /// Прочитать биты
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public override bool[] ReadCoils(ushort address, ushort numberOfPoints)
        {
            return _fieldBusAccessor.ReadCoils(this, address, numberOfPoints);
        }

        /// <summary>
        /// Записать один бит
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool WriteSingleCoil(ushort address, bool value)
        {
            return _fieldBusAccessor.WriteSingleCoil(this, address, value);
        }

        ///<summary>
        /// Записать несколько регистров
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public override bool WriteMultipleRegisters(ushort address, ushort[] values)
        {
            return _fieldBusAccessor.WriteMultipleRegisters(this, address, values);
        }

        ///<summary>
        /// Записать несколько битов
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public override bool WriteMultipleCoils(ushort address, bool[] values)
        {
            return _fieldBusAccessor.WriteMultipleCoils(this, address, values);
        }

        /// <summary>
        /// Инициализировать управление по Modbus 
        /// </summary>
        public override void InitializeModbusMaster()
        {
            _fieldBusAccessor.InitializeModbusMaster();
        }

        ///<summary>
        /// Узел полевой шины онлайн
        ///</summary>
        public override bool IsOnline { get; protected set; }

        ///<summary>
        /// Проверить доступность узла полевой шины
        ///</summary>
        ///<returns></returns>
        public override bool CheckOnline()
        {
            return (IsOnline = _fieldBusAccessor.CheckOnline());
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public override void Dispose()
        {
            _fieldBusAccessor.Dispose();
        }

        #endregion

    }
}