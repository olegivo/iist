using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using Microsoft.VisualBasic.Devices;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.Ports;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// ������� ������� ���
    ///</summary>
    public class FieldBusFactory : IFieldBusFactory
    {
        #region Singleton

        private static FieldBusFactory _instance;

        ///<summary>
        /// ������������ ���������
        ///</summary>
        public static FieldBusFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FieldBusFactory();
                }
                return _instance;
            }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="FieldBusFactory" />.
        /// </summary>
        private FieldBusFactory()
        {
        }

        #endregion


        #region methods

        ///<summary>
        /// ����� ����� �������
        ///</summary>
        ///<returns></returns>
        public object[] FindPorts()
        {
            return FindPorts(DefaultFieldBusType);
        }

        /// <summary>
        /// ��� ����� �� ���������
        /// </summary>
        public FieldBusType DefaultFieldBusType
        {
            get { return FieldBusType.RS485;}
        }

        ///<summary>
        /// ����� ����� ����������� � ������� ����� ��������� ����
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
                            throw new NotImplementedException("����������� ���������������� ������");
                            //todo: FieldBusFactory.FindPorts - ����������� ���������������� ������, ������� ���������
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
                            Debug.WriteLine(ex.ToString());
                        }
                        return portNames.ToArray();
                    }
                    break;

                case FieldBusType.Ethernet:
                    return new object[] { "����������� �� ��������� ���� Ethernet" };

                default:
                    throw new ArgumentOutOfRangeException("fieldBusType");
            }

            return null;
        }

        private readonly Hashtable _serialPorts = new Hashtable();

        /// <summary>
        /// ������� <see cref="ModbusSerialFieldBusPort"/>
        /// </summary>
        /// <param name="serialPortParameters"></param>
        /// <returns></returns>
        private ModbusSerialAccessor CreateModbusSerialAccessor(SerialPortParameters serialPortParameters)
        {
            //�������� � ����������������� ������� ���� COM: ���� �� ���������� �� ������, 
            //����� �� ��� �� �����������. ������� - ����������� ������ (������ ��� ������������ ������ ������ �������� �����)
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
        /// ������� <see cref="ModbusTcpFieldBusPort"/>
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
        /// �������� ������ ��������� �� ����
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <param name="fieldBusType"></param>
        /// <param name="onlyCustomized">������ ����������� (<see langword="false"/> - ���, ��� ����� ����� �� ����)</param>
        /// <param name="skipOffline">���������� �����������</param>
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
            List<object> list = new List<object>();
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
                        fieldBusManager = new FieldBusManager(fieldBusAccessor.FieldBusType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("fieldBusAccessor", fieldBusAccessor.FieldBusType, "����������� ��� ������� ����");
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
                    SerialPortParameters serialPortParameters = new SerialPortParameters();
                    serialPortParameters.Mode = AsciiRtuMode.RTU;
                    serialPortParameters.MaxAddress = 2;
                    serialPortParameters.Port = port;
                    retValue = serialPortParameters;
                    break;
                case FieldBusType.Ethernet:
                    TcpFieldBusPortParameters tcpFieldBusPortParameters = new TcpFieldBusPortParameters();
                    FieldBusNodeIpAddress ipAddress = port as FieldBusNodeIpAddress;
                    if(ipAddress!=null) tcpFieldBusPortParameters.IpAddress=new IPAddress(ipAddress.IpSlaveAddress);
                    retValue = tcpFieldBusPortParameters;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("fieldBusType");
            }
            return retValue;
        }
    }
}