using System;
using System.Collections.Generic;
using System.ComponentModel;
using NLog;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// Компонент доступа к ресурсам полевой шины, поддерживающий одну из разновидностей протокола Modbus
    ///</summary>
    public abstract class ModbusAccessor : Component, IFieldBusAccessor
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region fields

        ///<summary>
        ///
        ///</summary>
        protected ModbusAdapter _modbusAdapter;

        ///<summary>
        /// Минимальное количество миллисекунд для ответа
        ///</summary>
        protected int _minimumWaitToRetryMilliseconds;

        ///<summary>
        /// Минимальное количество попыток для ответа
        ///</summary>
        protected int _minimumRetries;

        ///<summary>
        /// Диапазон адресов на шине
        ///</summary>
        protected List<object> _addressRange;

        #endregion

        #region properties

        ///<summary>
        /// 
        ///</summary>
        public ModbusAdapter ModbusAdapter
        {
            get
            {
                //TODO: ModbusAccessor.ModbusAdapter - Инициализировать управление по Modbus при каждом обращении к данному свойству??? Может лучше кэшировать?
                //if (_modbusAdapter == null || _modbusAdapter)
                InitializeModbusMaster();
                return _modbusAdapter;
            }
            protected set { _modbusAdapter = value; }
        }

        /// <summary>
        /// Параметры транспорта Modbus
        /// </summary>
        /// <remarks>Действует только для NModbusAdapter</remarks>
        public ModbusAccessorTimeouts ModbusAccessorTimeouts//NOTE:Внимание! Действует только для NModbusAdapter!
        {
            get
            {
                var nModbusAdapter = ModbusAdapter as NModbusAdapter;
                if (nModbusAdapter != null)
                    return nModbusAdapter.ModbusAccessorTimeouts;
                return null;
            }
            set
            {
                var nModbusAdapter = ModbusAdapter as NModbusAdapter;
                if (nModbusAdapter != null)
                    nModbusAdapter.ModbusAccessorTimeouts = value;
            }
        }

        ///<summary>
        /// Тип полевой шины
        ///</summary>
        public abstract FieldBusType FieldBusType { get; }

        #endregion

        #region constructors

        ///<summary>
        ///
        ///</summary>
        protected ModbusAccessor()
            : this(null, null)
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="modbusAdapter"></param>
        protected ModbusAccessor(ModbusAdapter modbusAdapter)
            : this(modbusAdapter, null)
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="addressRange"></param>
        protected ModbusAccessor(List<object> addressRange)
            : this(null, addressRange)
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="modbusAdapter"></param>
        ///<param name="addressRange"></param>
        protected ModbusAccessor(ModbusAdapter modbusAdapter, List<object> addressRange)
        {
            _modbusAdapter = modbusAdapter;

            _addressRange = addressRange;

            _minimumRetries = 3;
            _minimumWaitToRetryMilliseconds = 20;
        }

        #endregion

        #region IFieldBusPort Members

        /// <summary>
        /// Инициализировать управление портом
        /// </summary>
        public void InitializeControlMode()
        {
            Log.Debug("Инициализация управления портом...");
            InitializeModbusMaster();
        }

        ///<summary>
        /// Имя порта
        ///</summary>
        public abstract string PortName { get; }

        #endregion

        ///<summary>
        /// Получить диапазон адресов для данного порта
        ///</summary>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        protected abstract FieldBusNodeAddress[] GetPLCAddressRange();
        /// <summary>
        /// Серия
        /// </summary>
        /// <param name="fieldBusNodeAddress"></param>
        /// <returns></returns>
        protected ushort GetINFO_SERIES(IFieldBusNodeAccessor fieldBusNodeAddress)
        {
            return ReadHoldingRegister(fieldBusNodeAddress, 0x2011);
        }

        /// <summary>
        /// WAGO item number, e. g. 841 for the controller
        /// </summary>
        /// <param name="fieldBusNodeAddress"></param>
        /// <returns></returns>
        protected ushort GetINFO_ITEM(IFieldBusNodeAccessor fieldBusNodeAddress)
        {
            return ReadHoldingRegister(fieldBusNodeAddress, 0x2012);
        }

        /// <summary>
        /// Number of word-based outputs registers in the process image in bits (divide
        /// by 16 to get the total number of analog words)
        /// </summary>
        /// <param name="fieldBusNodeAddress"></param>
        /// <returns></returns>
        protected ushort GetICnfLen_AnalogOut(IFieldBusNodeAccessor fieldBusNodeAddress)
        {
            return ReadHoldingRegister(fieldBusNodeAddress, 0x1022);
        }

        /// <summary>
        /// Number of word-based inputs registers in the process image in bits (divide
        /// by 16 to get the total number of analog words)
        /// </summary>
        /// <param name="fieldBusNodeAddress"></param>
        /// <returns></returns>
        protected ushort GetICnfLen_AnalogInp(IFieldBusNodeAccessor fieldBusNodeAddress)
        {
            return ReadHoldingRegister(fieldBusNodeAddress, 0x1023);
        }

        /// <summary>
        /// Number of digital output bits in the process image
        /// </summary>
        /// <param name="fieldBusNodeAddress"></param>
        /// <returns></returns>
        protected ushort GetICnfLen_DigitalOut(IFieldBusNodeAccessor fieldBusNodeAddress)
        {
            return ReadHoldingRegister(fieldBusNodeAddress, 0x1024);
        }

        /// <summary>
        /// Number of digital input bits in the process image
        /// </summary>
        /// <param name="fieldBusNodeAddress"></param>
        /// <returns></returns>
        protected ushort GetICnfLen_DigitalInp(IFieldBusNodeAccessor fieldBusNodeAddress)
        {
            return ReadHoldingRegister(fieldBusNodeAddress, 0x1025);
        }

        #region константы, которые могут быть использованы для тестирования связи с ведущим устройством
        /// <summary>
        /// Значение Нули
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_ZERO(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                if (ReadHoldingRegister(fieldBusNodeAccessor, 0x2000) == 0x0000)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Значение Единицы
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_ONES(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                if (ReadHoldingRegister(fieldBusNodeAccessor, 0x2001) == 0xFFFF)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Значение 1,2,3,4
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_1234(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                if (ReadHoldingRegister(fieldBusNodeAccessor, 0x2002) == 0x1234)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Значение Маска 1 (Константа, используемая для проверки наличия всех битов)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_AAAA(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                if (ReadHoldingRegister(fieldBusNodeAccessor, 0x2003) == 0xAAAA)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Значение Маска 1 (Константа, используемая для проверки наличия всех битов)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_5555(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                if (ReadHoldingRegister(fieldBusNodeAccessor, 0x2004) == 0x5555)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Максимальное положительное число (Константа, используемая для контроля арифметического устройства)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_MAX_POS(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                ushort reg = ReadHoldingRegister(fieldBusNodeAccessor, 0x2005);
                if (reg == 32767)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Максимальное отрицательное число (Константа, используемая для контроля арифметического устройства)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_MAX_NEG(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                ushort reg = ReadHoldingRegister(fieldBusNodeAccessor, 0x2006);
                if (reg == 32768)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Максимальное половинное положительное число (Константа, используемая для контроля арифметического устройства)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_HALF_POS(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                ushort reg = ReadHoldingRegister(fieldBusNodeAccessor, 0x2007);
                if (reg == 16383)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Максимальное половинное отрицательное число (Константа, используемая для контроля арифметического устройства)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        protected bool TryGetGP_HALF_NEG(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            try
            {
                ushort reg = ReadHoldingRegister(fieldBusNodeAccessor, 0x2008);
                if (reg == 16384)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return false;
        }

        #endregion

        #region ModBus Read/Write

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private ushort ReadHoldingRegister(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address)
        {
            try
            {
                return ReadHoldingRegisters(fieldBusNodeAccessor, address, 1)[0];
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ошибка чтения регистра по адресу {0}", address), ex);
            }
        }

        ///<summary>
        /// Проверить доступность полевой шины
        ///</summary>
        ///<returns></returns>
        public abstract bool CheckOnline();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public ushort[] ReadHoldingRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            try
            {
                var modbusAdapter = ModbusAdapter;

                Log.Debug("Попытка чтения регистра. Порт - {0}, адрес ПЛК - {1}, адрес регистра - {2}",
                                PortName,
                                fieldBusNodeAccessor.AddressPart2,
                                address);

                return modbusAdapter.ReadHoldingRegisters((byte) fieldBusNodeAccessor.AddressPart2, address, numberOfPoints);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    string.Format("Ошибка чтения выходных {0} регистров (Holding Registers) по адресу {1}",
                                  numberOfPoints, address), ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public ushort ReadInputRegister(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address)
        {
            try
            {
                return ReadInputRegisters(fieldBusNodeAccessor, address, 1)[0];
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ошибка чтения входного регистра по адресу {0}", address), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public ushort[] ReadInputRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            try
            {
                var modbusAdapter = ModbusAdapter;
                Log.Debug(
                    "Попытка чтения регистров (Input Registers). Порт - {0}, адрес ПЛК - {1}, адрес регистра - {2}",
                    PortName,
                    fieldBusNodeAccessor.AddressPart2,
                    address);
                return modbusAdapter.ReadInputRegisters((byte) fieldBusNodeAccessor.AddressPart2, address, numberOfPoints);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    string.Format("Ошибка чтения входных {0} регистров (Input Registers) по адресу {1}", numberOfPoints,
                                  address), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public bool[] ReadCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            try
            {
                var modbusAdapter = ModbusAdapter;
                Log.Debug("Чтение {0} ячеек по адресу {1}", numberOfPoints, address);
                return modbusAdapter.ReadCoils((byte) fieldBusNodeAccessor.AddressPart2, address, numberOfPoints);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    string.Format("Ошибка чтения {0} ячеек по адресу {1}", numberOfPoints, address), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteSingleRegister(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort value)
        {
            try
            {
                var modbusAdapter = ModbusAdapter;
                Log.Debug("Попытка записи регистра. Порт - {0}, адрес ПЛК - {1}, адрес регистра - {2}",
                                PortName,
                                fieldBusNodeAccessor.AddressPart2,
                                address);
                modbusAdapter.WriteSingleRegister((byte) fieldBusNodeAccessor.AddressPart2, address, value);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ошибка записи регистра по адресу {0}", address), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteSingleCoil(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool value)
        {
            try
            {
                var modbusAdapter = ModbusAdapter;
                Log.Debug("Попытка записи ячейки. Порт - {0}, адрес ПЛК - {1}, адрес ячейки - {2}",
                                PortName,
                                fieldBusNodeAccessor.AddressPart2,
                                address);
                modbusAdapter.WriteSingleCoil((byte) fieldBusNodeAccessor.AddressPart2, address, value);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ошибка записи ячейки по адресу {0}", address), ex);
            }
        }

        #endregion

        ///<summary>
        /// Попытка получить ответ от устройства по данному адресу
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<returns></returns>
        protected bool TryGetReply(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            if (TryGetGP_ZERO(fieldBusNodeAccessor))
            {
                if (TryGetGP_ONES(fieldBusNodeAccessor))
                {
                    if (TryGetGP_1234(fieldBusNodeAccessor))
                    {
                        if (TryGetGP_AAAA(fieldBusNodeAccessor))
                        {
                            if (TryGetGP_5555(fieldBusNodeAccessor))
                            {
                                if (TryGetGP_MAX_POS(fieldBusNodeAccessor))
                                {
                                    if (TryGetGP_MAX_NEG(fieldBusNodeAccessor))
                                    {
                                        if (TryGetGP_HALF_POS(fieldBusNodeAccessor))
                                        {
                                            if (TryGetGP_HALF_NEG(fieldBusNodeAccessor))
                                            {
                                                ushort register = 0;

                                                try
                                                {
                                                    Log.Debug(string.Format("{0}-{1}", GetINFO_SERIES(fieldBusNodeAccessor),
                                                                                  GetINFO_ITEM(fieldBusNodeAccessor)));
                                                    Log.Debug(string.Format("Выходных аналоговых слов: {0}", GetICnfLen_AnalogOut(fieldBusNodeAccessor) / 16));
                                                    Log.Debug(string.Format("Входных аналоговых слов: {0}", GetICnfLen_AnalogInp(fieldBusNodeAccessor) / 16));
                                                    Log.Debug(string.Format("Выходных цифровых битов: {0}", GetICnfLen_DigitalOut(fieldBusNodeAccessor)));
                                                    Log.Debug(string.Format("Входных цифровых битов:{0}", GetICnfLen_DigitalInp(fieldBusNodeAccessor)));
                                                }
                                                catch (Exception ex)
                                                {
                                                    Log.Debug(ex.Message);
                                                }

                                                const ushort start = 0x2030;
                                                const ushort end = 0x2033;

                                                //461-469-530-600

                                                for (ushort address = start; address <= end; address++)
                                                {
                                                    try
                                                    {
                                                        //register = ModbusAdapter.ReadInputRegisters(fieldBusNodeAccessor.AddressPart2, address, 1);
                                                        byte[] bytes = BitConverter.GetBytes(register);
                                                        string s = string.Format("address 0x{0}, value {1} ({2})", address.ToString("X"), BitConverter.ToString(bytes), register);
                                                        Log.Debug(s);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Log.Debug(ex.Message);
                                                    }
                                                }

                                                for (ushort address = start; address <= end; address++)
                                                {
                                                    try
                                                    {
                                                        register = ReadInputRegister(fieldBusNodeAccessor, address);
                                                        byte[] bytes = BitConverter.GetBytes(register);
                                                        string s = string.Format("address 0x{0}, value {1} ({2})", address.ToString("X"), BitConverter.ToString(bytes), register);
                                                        Log.Debug(s);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Log.Debug(ex.Message);
                                                    }
                                                }
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Инициализировать управление по Modbus 
        /// </summary>
        public abstract void InitializeModbusMaster();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool WriteMultipleRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort[] values)
        {
            try
            {
                var modbusAdapter = ModbusAdapter;
                Log.Debug("Попытка записи регистров ({3}). Порт - {0}, адрес ПЛК - {1}, адрес регистров - {2}",
                                PortName,
                                fieldBusNodeAccessor.AddressPart2,
                                address,
                                values.Length);
                modbusAdapter.WriteMultipleRegisters((byte) fieldBusNodeAccessor.AddressPart2, address, values);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ошибка записи {0} регистров по адресу {1}", values.Length, address), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool WriteMultipleCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool[] values)
        {
            try
            {
                var modbusAdapter = ModbusAdapter;
                Log.Debug("Попытка записи ячеек ({3}). Порт - {0}, адрес ПЛК - {1}, адрес ячеек - {2}",
                                PortName,
                                fieldBusNodeAccessor.AddressPart2,
                                address,
                                values.Length);
                modbusAdapter.WriteMultipleCoils((byte) fieldBusNodeAccessor.AddressPart2, address, values);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Ошибка записи {0} ячеек по адресу {1}", values.Length, address), ex);
            }
        }
    }
}