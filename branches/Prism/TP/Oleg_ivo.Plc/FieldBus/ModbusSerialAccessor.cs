using System;
using System.IO.Ports;
using Modbus.Device;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Ports;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// Компонент доступа к ресурсам полевой шины, поддерживающий одну из разновидностей протокола Modbus по последовательному интерфейсу
    ///</summary>
    public class ModbusSerialAccessor : ModbusAccessor
    {

        #region fields
        private readonly SerialPort _serialPort;
        private readonly FieldBusType _fieldBusType;

        #endregion

        ///<summary>
        ///
        ///</summary>
        ///<param name="serialPort"></param>
        ///<param name="mode"></param>
        ///<param name="fieldBusType"></param>
        public ModbusSerialAccessor(SerialPort serialPort, AsciiRtuMode mode, FieldBusType fieldBusType)
        {
            if (serialPort == null) throw new ArgumentNullException("serialPort");

            _serialPort = serialPort;
            _fieldBusType = fieldBusType;
            Mode = mode;
        }

        ///<summary>
        /// Тип полевой шины
        ///</summary>
        public override FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
        }

        ///<summary>
        /// Имя порта
        ///</summary>
        public override string PortName
        {
            get
            {
                return _serialPort != null
                           ? _serialPort.PortName
                           : "";
            }
        }

        ///<summary>
        /// 
        ///</summary>
        public AsciiRtuMode Mode { get; set; }

        ///<summary>
        /// Получить диапазон адресов для данного порта
        ///</summary>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        protected override FieldBusNodeAddress[] GetPLCAddressRange()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        /// Проверить доступность полевой шины
        ///</summary>
        ///<returns></returns>
        public override bool CheckOnline()
        {
            Console.WriteLine("Тестирование подключения к {0}...", _serialPort);
            return _serialPort != null && _serialPort.IsOpen;
        }

        ///<summary>
        /// Инициализировать управление по Modbus 
        ///</summary>
        public override void InitializeModbusMaster()
        {
#if EMULATIONMODE
            Console.WriteLine("Инициализация управления по Modbus/RS-485 в режиме эмуляции не требуется");
#else
            try
            {
                //открываем или переоткрываем порт
                if (!_serialPort.IsOpen)
                    _serialPort.Open();
                else
                {
                    _serialPort.Close();
                    _serialPort.Open();
                }
            }
            catch (System.IO.IOException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Tools.ExceptionCatcher.Debug.Instance.ThrowOnlyInDebug(ex);
            }

            switch (Mode)
            {
                case AsciiRtuMode.ASCII:
                    Console.WriteLine("Инициализация управления по Modbus в ASCII-режиме...");
                    ModbusAdapter = new NModbusAdapter(ModbusSerialMaster.CreateAscii(_serialPort));
                    break;
                case AsciiRtuMode.RTU:
                    Console.WriteLine("Инициализация управления по Modbus в RTU-режиме...");
                    ModbusAdapter = new NModbusAdapter(ModbusSerialMaster.CreateRtu(_serialPort));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
#endif
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"></see> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && _serialPort != null)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _serialPort.Dispose();
            }
        }

    }
}