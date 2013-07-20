using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using Microsoft.VisualBasic.Devices;
using NLog;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.Ports;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// Фабрика полевых шин
    ///</summary>
    public class FieldBusFactory : IFieldBusFactory
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FieldBusFactory" />.
        /// </summary>
        public FieldBusFactory(IDistributedMeasurementInformationSystem dmis)
        {
            this.dmis = dmis;
        }

        #region methods

        ///<summary>
        /// Найти порты системы
        ///</summary>
        ///<returns></returns>
        public object[] FindPorts()
        {
            return FindPorts(DefaultFieldBusType);
        }

        /// <summary>
        /// Тип порта по умолчанию
        /// </summary>
        public FieldBusType DefaultFieldBusType
        {
            get { return FieldBusType.RS485;}
        }

        ///<summary>
        /// Найти порты подключения к полевым шинам заданного типа
        ///</summary>
        ///<param name="fieldBusType"></param>
        ///<returns></returns>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        public object[] FindPorts(FieldBusType fieldBusType)
        {
            switch (fieldBusType)
            {
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    Computer computer = new Computer();
                    if (computer.Ports != null && computer.Ports.SerialPortNames != null)
                    {
                        List<string> portNames = new List<string>();
                        try
                        {
                            throw new NotImplementedException("определение последовательных портов");
                            //todo: FieldBusFactory.FindPorts - определение последовательных портов, которые сохранены
                            /* 
                                foreach (string portName in computer.Ports.SerialPortNames)
                                {
                                    //foreach (System.Web.UI.Pair pair in DistributedMeasurementInformationSystemBase.Instance.Settings.PortsRanges[fieldBusType])
                                    //{
                                    //    if (String.Equals(pair.First as string, portName, StringComparison.InvariantCultureIgnoreCase))
                                    //        portNames.Add(portName);
                                    //}
                                                        }
                            */
                        }
                        catch (Exception ex)
                        {
                            Log.Debug(ex.ToString());
                        }
                        return portNames.ToArray();
                    }
                    break;

                case FieldBusType.Ethernet:
                    return new object[] { "Подключение по локальной сети Ethernet" };

                default:
                    throw new ArgumentOutOfRangeException("fieldBusType");
            }

            return null;
        }

        private readonly Hashtable _serialPorts = new Hashtable();
        private readonly IDistributedMeasurementInformationSystem dmis;

        /// <summary>
        /// Создать <see cref="ModbusSerialFieldBusPort"/>
        /// </summary>
        /// <param name="serialPortParameters"></param>
        /// <returns></returns>
        private ModbusSerialAccessor CreateModbusSerialAccessor(SerialPortParameters serialPortParameters)
        {
            //проблема с последовательными портами типа COM: если не освободить их ресурс, 
            //потом до них не достучаться. Решение - кэширование портов (выдача уже существующих портов вместо создания новых)
            SerialPort port = _serialPorts[serialPortParameters.PortName] as SerialPort;
            if(port==null)
            {
                port = new SerialPort(serialPortParameters.PortName);
                serialPortParameters.Assign(port);
                _serialPorts.Add(serialPortParameters.PortName, port);
            }

            ModbusSerialAccessor modbusSerialAccessor = new ModbusSerialAccessor(port, serialPortParameters.Mode, serialPortParameters.FieldBusType);
            
            return modbusSerialAccessor;
        }

        /// <summary>
        /// Создать <see cref="ModbusTcpFieldBusPort"/>
        /// </summary>
        /// <param name="fieldBusPortParameters"></param>
        /// <returns></returns>
        private ModbusTcpIpAccessor CreateModbusTcpIpAccessor(TcpFieldBusPortParameters fieldBusPortParameters)
        {
            ModbusTcpIpAccessor modbusIpAccessor = new ModbusTcpIpAccessor(fieldBusPortParameters.Port,
                                                                           fieldBusPortParameters.IpAddress,
                                                                           fieldBusPortParameters.FieldBusType);

            return modbusIpAccessor;
        }

        /// <summary>
        /// Получить адреса устройств на шине
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <param name="fieldBusType"></param>
        /// <param name="onlyCustomized">Только настроенные (<see langword="false"/> - все, что можно найти на шине)</param>
        /// <param name="skipOffline">Пропускать отключенные</param>
        /// <returns></returns>
        public object[] GetAvailableFieldBusAddresses(FieldBusManager fieldBusManager, FieldBusType fieldBusType, bool onlyCustomized, bool skipOffline)
        {
            List<object> objects = new List<object>();

            switch (fieldBusType)
            {
                case FieldBusType.Unknown:
                    break;
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    object[] ports = FindPorts(fieldBusType);
                    if (ports!=null)
                    {
                        //foreach (object port in ports)
                        //{
                            
                        //}
                        GetAvailableSerialFieldBusAddresses(onlyCustomized, skipOffline);
                    }
                    break;
                case FieldBusType.Ethernet:
                    break;
            }

            return objects.ToArray();
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="onlyCustomized"></param>
        ///<param name="skipOffline"></param>
        ///<returns></returns>
        private object[] GetAvailableSerialFieldBusAddresses(bool onlyCustomized, bool skipOffline)
        {
            var list = new List<object>();
            return list.ToArray();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusAccessor"></param>
        /// <returns></returns>
        public FieldBusManager CreateFieldBusManager(IFieldBusAccessor fieldBusAccessor)
        {
            FieldBusManager fieldBusManager = null;
            if (fieldBusAccessor!=null)
            {
                switch (fieldBusAccessor.FieldBusType)
                {
                    case FieldBusType.RS232:
                    case FieldBusType.RS485:
                        fieldBusManager = new ActiveFieldBusManager(fieldBusAccessor);
                        break;
                    case FieldBusType.Ethernet:
                        fieldBusManager = new FieldBusManager(fieldBusAccessor.FieldBusType, dmis);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("fieldBusAccessor", fieldBusAccessor.FieldBusType, "неизвестный тип полевой шины");
                }
            }

            return fieldBusManager;
        }

        ///<summary>
        /// 
        ///</summary>
        ///<param name="fieldBusType"></param>
        ///<param name="port"></param>
        ///<returns></returns>
        public IFieldBusAccessor CreateFieldbusAccessor(FieldBusType fieldBusType, object port)
        {
            ModbusAccessor modbusAccessor;
            FieldBusPortParameters portParameters = CreatePortParameters(fieldBusType, port);
            switch (fieldBusType)
            {
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    modbusAccessor = CreateModbusSerialAccessor(portParameters as SerialPortParameters);
                    break;
                case FieldBusType.Ethernet:
                    modbusAccessor = CreateModbusTcpIpAccessor(portParameters as TcpFieldBusPortParameters);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fieldBusType");
            }
            return modbusAccessor;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusType"></param>
        ///<param name="port"></param>
        ///<returns></returns>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        public FieldBusPortParameters CreatePortParameters(FieldBusType fieldBusType, object port)
        {
            FieldBusPortParameters retValue;
            switch (fieldBusType)
            {
                case FieldBusType.RS232:
                case FieldBusType.RS485:
                    var serialPortParameters = new SerialPortParameters
                        {
                            Mode = AsciiRtuMode.RTU,
                            MaxAddress = 2,
                            Port = port
                        };
                    retValue = serialPortParameters;
                    break;
                case FieldBusType.Ethernet:
                    var tcpFieldBusPortParameters = new TcpFieldBusPortParameters();
                    var ipAddress = port as FieldBusNodeIpAddress;
                    if (ipAddress != null)
                        tcpFieldBusPortParameters.IpAddress = new IPAddress(ipAddress.IpSlaveAddress);
                    retValue = tcpFieldBusPortParameters;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fieldBusType");
            }
            return retValue;
        }
    }
}