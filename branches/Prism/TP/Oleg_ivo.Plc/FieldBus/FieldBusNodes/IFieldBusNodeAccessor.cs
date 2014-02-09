using System;
using Oleg_ivo.Plc.Devices.Contollers;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    /// <summary>
    /// Интерфейс доступа к ресурсам узла полевой шины
    /// </summary>
    public interface IFieldBusNodeAccessor : IDisposable
    {
        /// <summary>
        /// Компонент доступа к ресурсам полевой шины
        /// </summary>
        IFieldBusAccessor FieldBusAccessor { get; }

        /// <summary>
        /// Прочитать регистры
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        ushort[] ReadHoldingRegisters(ushort address, ushort numberOfPoints);

        /// <summary>
        /// Прочитать входные регистры
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        ushort[] ReadInputRegisters(ushort address, ushort numberOfPoints);

        ///<summary>
        /// Записать в регистр
        ///</summary>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        bool WriteSingleRegister(ushort address, ushort value);

        /// <summary>
        /// Прочитать биты
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        bool[] ReadCoils(ushort address, ushort numberOfPoints);

        /// <summary>
        /// Записать один бит
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool WriteSingleCoil(ushort address, bool value);

        ///<summary>
        /// Записать несколько регистров
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        bool WriteMultipleRegisters(ushort address, ushort[] values);

        ///<summary>
        /// Записать несколько битов
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        bool WriteMultipleCoils(ushort address, bool[] values);

        /// <summary>
        /// Инициализировать управление по Modbus 
        /// </summary>
        void InitializeModbusMaster();

        ///<summary>
        /// Узел полевой шины онлайн
        ///</summary>
        bool IsOnline { get; }

        string AddressPart1 //TODO:пока есть FieldBusAddress, используется только для редактирования базы в CMS
        { get; set; }

        int AddressPart2 //TODO:пока есть FieldBusAddress, используется только для редактирования базы в CMS
        { get; set; }

        ///<summary>
        /// Проверить доступность узла полевой шины
        ///</summary>
        ///<returns></returns>
        bool CheckOnline();
    }
}