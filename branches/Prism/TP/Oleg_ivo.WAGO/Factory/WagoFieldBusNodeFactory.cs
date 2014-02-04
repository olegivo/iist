using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.WAGO.Devices;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    /// Фабрика по производству узлов полевой шины (WAGO)
    ///</summary>
    public class WagoFieldBusNodeFactory : FieldBusNodeFactory
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WagoFieldBusNodeFactory" />.
        /// </summary>
        public WagoFieldBusNodeFactory(IPlcFactory plcFactory, IFieldBusFactory fieldBusFactory)
            : base(plcFactory, fieldBusFactory)
        {
        }

        /// <summary>
        /// Загрузить настроенные узлы для полевой шины
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <returns></returns>
        public override FieldBusNodeCollection LoadFieldBusNodes(FieldBusManager fieldBusManager)
        {
            var nodes = new FieldBusNodeCollection();
            foreach (var row in DataContext.FieldBusNodes.Where(fbn=>fbn.Enabled && fbn.FieldBusTypeId==fieldBusManager.FieldBus.FieldBusTypeId))
            {
                FieldBusNode fieldBusNode;
                if (fieldBusManager is ActiveFieldBusManager)
                {
                    fieldBusNode = new FieldBusNode(fieldBusManager, GetFieldBusNodeAddress(row), row) { Id = row.Id };
                }
                else//нужно построить активный узел полевой шины
                {
                    //по адресу из row получаем FieldBusNodeAddress
                    FieldBusNodeAddress fieldBusNodeAddress = fieldBusManager.FieldBusAddresses.Find(row.AddressPart1, row.AddressPart2.Value);

                    if (fieldBusNodeAddress == null)
                    {
                        throw new Exception("Не найден адрес для активного узла полевой шины");
                    }

                    fieldBusNode = CreateFieldBusNode(fieldBusManager, fieldBusNodeAddress, row);
                }
                nodes.Add(fieldBusNode);
            }
            //FieldBusNodeDAC fieldBusNodeDAC = new FieldBusNodeDAC {FieldBusNodesFactory = this};
            //return fieldBusNodeDAC.GetFieldBusNodes(fieldBusManager);
            return nodes;
        }

        private FieldBusNodeAddress GetFieldBusNodeAddress(Plc.Entities.FieldBusNode fieldBusNode)
        {
            switch (fieldBusNode.FieldBusType.FieldBusTypeEnum)
            {
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    return GetSerialAddresses(fieldBusNode);
                case FieldBusType.Ethernet:
                    return GetEthernetAddresses(fieldBusNode);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal static FieldBusNodeAddress GetEthernetAddresses(Plc.Entities.FieldBusNode fieldBusNode)
        {
            FieldBusNodeIpAddress address = null;
            //todo: порт должен удовлетворять требованиям IP-Address
            if (fieldBusNode != null) address = new FieldBusNodeIpAddress(GetIPAddress(fieldBusNode.AddressPart1), fieldBusNode.AddressPart2.Value, fieldBusNode.Id);
            return address;
        }

        internal static FieldBusNodeAddress GetSerialAddresses(Plc.Entities.FieldBusNode fieldBusNode)
        {
            FieldBusNodeSerialAddress address = null;
            //todo: порт должен удовлетворять требованиям COMx, адрес на шине - [1..99]
            if (fieldBusNode != null) address = new FieldBusNodeSerialAddress(fieldBusNode.AddressPart1, (byte)fieldBusNode.AddressPart2, fieldBusNode.Id);
            return address;
        }

        private static byte[] GetIPAddress(string address)
        {
            string ip = address;
            var bytes = new List<byte>();
            while (ip.Length > 0)
            {
                //if (ip.StartsWith(".")) ip = ip.Substring(1, ip.Length - 1);
                int dot = ip.IndexOf(".");
                string b = dot >= 0 ? ip.Substring(0, dot) : ip;
                byte parseResult;
                byte.TryParse(b, out parseResult);
                bytes.Add(parseResult);
                ip = dot >= 0 ? ip.Substring(dot + 1, ip.Length - b.Length - 1) : "";
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// Создать ПЛК для узла полевой шины
        /// </summary>
        /// <param name="fieldBusNode"></param>
        protected override void CreatePlcForFieldBusNode(FieldBusNode fieldBusNode)
        {
            if (fieldBusNode != null)
            {
                if (PlcFactory != null)
                {
                    PlcFactory.CreatePLC(fieldBusNode);
                }
                else
                    throw new InvalidProgramException(
                        "В фабрике узлов полевой шины не проинициализировано свойство 'Фабрика ПЛК'");
            }
        }

        ///<summary>
        /// Проверить, корректен ли ПЛК для добавления в коллекцию узлов полевой шины
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        protected override bool CheckPLC(Plc.Devices.Contollers.PLC plc)
        {
            return (plc as WagoPlc != null);
        }

        /// <summary>
        /// Серия
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        private ushort GetINFO_SERIES(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            return ReadHoldingRegister(fieldBusNodeAccessor, 0x2011);
        }

        /// <summary>
        /// WAGO item number, e. g. 841 for the controller
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        private ushort GetINFO_ITEM(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            return ReadHoldingRegister(fieldBusNodeAccessor, 0x2012);
        }

        /// <summary>
        /// Number of word-based outputs registers in the process image in bits (divide
        /// by 16 to get the total number of analog words)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        private ushort GetICnfLen_AnalogOut(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            return ReadHoldingRegister(fieldBusNodeAccessor, 0x1022);
        }

        /// <summary>
        /// Number of word-based inputs registers in the process image in bits (divide
        /// by 16 to get the total number of analog words)
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        private ushort GetICnfLen_AnalogInp(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            return ReadHoldingRegister(fieldBusNodeAccessor, 0x1023);
        }

        /// <summary>
        /// Number of digital output bits in the process image
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        private ushort GetICnfLen_DigitalOut(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            return ReadHoldingRegister(fieldBusNodeAccessor, 0x1024);
        }

        /// <summary>
        /// Number of digital input bits in the process image
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        private ushort GetICnfLen_DigitalInp(IFieldBusNodeAccessor fieldBusNodeAccessor)
        {
            return ReadHoldingRegister(fieldBusNodeAccessor, 0x1025);
        }

        #region константы, которые могут быть использованы для тестирования связи с ведущим устройством
        /// <summary>
        /// Значение Нули
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <returns></returns>
        private bool TryGetGP_ZERO(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_ONES(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_1234(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_AAAA(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_5555(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_MAX_POS(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_MAX_NEG(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_HALF_POS(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
        private bool TryGetGP_HALF_NEG(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
            return ReadHoldingRegisters(fieldBusNodeAccessor, address, 1)[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public ushort ReadInputRegister(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address)
        {
            return ReadInputRegisters(fieldBusNodeAccessor, address, 1)[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public ushort[] ReadHoldingRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort numberOfPoints)
        {
            ushort[] registers = fieldBusNodeAccessor.ReadHoldingRegisters(address, numberOfPoints);
            return registers;
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
            ushort[] registers = fieldBusNodeAccessor.ReadInputRegisters(address, numberOfPoints);
            return registers;
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
            bool[] registers = fieldBusNodeAccessor.ReadCoils(address, numberOfPoints);
            return registers;
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
            return fieldBusNodeAccessor.WriteSingleRegister(address, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool WriteRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort[] values)
        {
            return fieldBusNodeAccessor.WriteMultipleRegisters(address, values);
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
            return fieldBusNodeAccessor.WriteSingleCoil(address, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNodeAccessor"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool WriteCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool[] values)
        {
            return fieldBusNodeAccessor.WriteMultipleCoils(address, values);
        }

        #endregion

        ///<summary>
        /// Попытка получить ответ от устройства по данному адресу
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<returns></returns>
        protected override bool TryGetReply(IFieldBusNodeAccessor fieldBusNodeAccessor)
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
                                                //int ireg = 0;

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
                                                        //register = ModbusAccessor.ReadInputRegisters(fieldBusNodeAccessor.SlaveAddress, address, 1);
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

        /*
                private void Test(FieldBusNode plcAddress)
                {
                    /// опрашиваем регистры:
                    /// GP_ZERO (нули)
                    /// GP_ONES (единицы)
                    /// 
                    ushort[] registers = null;

                    try
                    {
                        registers = ModbusAccessor.ReadHoldingRegisters(plcAddress, 0x2000, 1);
                        //byte[] bytes = BitConverter.GetBytes(registers[0]);
                    }
                    catch (Exception ex)
                    {
                    }


                    ushort length = 64;
                    ushort startAddress;// = 255;//0x2000;
                    List<ushort> inputRegisters = new List<ushort>();
                    List<ushort> holdingRegisters = new List<ushort>();
                    ushort endAddress = 1024;

                    for (startAddress = 0; startAddress <= endAddress - length; startAddress += length)
                        try
                        {
                            registers = ModbusAccessor.ReadHoldingRegisters(plcAddress, startAddress, length);
                            holdingRegisters.AddRange(registers);

                            registers = ModbusAccessor.ReadInputRegisters(plcAddress, startAddress, length);
                            inputRegisters.AddRange(registers);
                        }
                        catch (Exception ex)
                        {
                        }

            
                    List<bool> coils = new List<bool>();
                    length = 1024;
                    endAddress = 3000;
                    bool[] cells = null;
                    for (startAddress = 0; startAddress <= endAddress - length; startAddress += length)
                        try
                        {
                            cells = ModbusAccessor.ReadCoils(plcAddress, startAddress, length);
                            coils.AddRange(cells);
                        }
                        catch (Exception ex)
                        {
                        }

                    registers = ModbusAccessor.ReadHoldingRegisters(plcAddress, 0x2000, 1);
                    registers = ModbusAccessor.ReadHoldingRegisters(plcAddress, 0x2001, 1);
                    byte[] bytes = BitConverter.GetBytes(registers[0]);
                    registers = ModbusAccessor.ReadInputRegisters(plcAddress, 0x2000, 1);
                    cells = ModbusAccessor.ReadCoils(plcAddress, 0, 2000);
                    cells = ModbusAccessor.ModbusAdapter.ReadInputs(plcAddress.SlaveAddress, 0, 2000);



                    List<bool> inputs = new List<bool>();
                    try
                    {
                        cells = ModbusAccessor.ReadCoils(plcAddress, 1, length);
                        inputs.AddRange(cells);
                    }
                    catch (Exception ex)
                    {
                    }

                    //try
                    //{
                    //    registers = ModbusAccessor.ReadHoldingRegisters(plcAddress.SlaveAddress, startAddress, length);
                    //    holdingRegisters.AddRange(registers);
                    //}
                    //catch (Exception ex) 
                    //{ 

                    //}
                }
        */

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNodeAccessor"></param>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public bool WriteMultipleRegisters(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, ushort[] values)
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
        public bool WriteMultipleCoils(IFieldBusNodeAccessor fieldBusNodeAccessor, ushort address, bool[] values)
        {
            return fieldBusNodeAccessor.WriteMultipleCoils(address, values);
        }

    }
}