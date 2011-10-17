using System;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;

namespace Oleg_ivo.Plc
{
    ///<summary>
    ///
    ///</summary>
    [DefaultEvent("LogMessage"), DefaultProperty("PortName")]
    public class SerialPortConnect : Component
    {
        #region fields
        private SerialPort _port;
        private string _portName;
        private int _baudRate;
        private int _dataBits;
        private Parity _parity;
        private StopBits _stopBits;

        #endregion

        #region properties
        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue(null)]
        public SerialPort Port
        {
            get { return _port; }
            set
            {
                ClearEvents();
                _port = value;
                SubscribePortEvents();
            }
        }

        /// <summary>
        /// Отписаться от событий порта
        /// </summary>
        private void ClearEvents()
        {
            if(Port!=null)
            {
                Port.DataReceived -= Port_DataReceived;
                Port.ErrorReceived -= Port_ErrorReceived;
                Port.PinChanged -= Port_PinChanged;
            }
        }

        /// <summary>
        /// Подписаться на события порта
        /// </summary>
        private void SubscribePortEvents()
        {
            if (Port != null)
            {
                Port.DataReceived += Port_DataReceived;
                Port.ErrorReceived += Port_ErrorReceived;
                Port.PinChanged += Port_PinChanged;
            }
        }

        public event SerialDataReceivedEventHandler DataReceived;
        public event SerialErrorReceivedEventHandler ErrorReceived;
        public event SerialPinChangedEventHandler PinChanged;

        void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            OnDataReceived(e);
        }

        private void OnDataReceived(SerialDataReceivedEventArgs e)
        {
            if (DataReceived != null)
                DataReceived(this, e);
        }

        void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            OnErrorReceived(e);
        }

        private void OnErrorReceived(SerialErrorReceivedEventArgs e)
        {
            if (ErrorReceived != null)
                ErrorReceived(this, e);
        }

        void Port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            OnPinChanged(e);
        }

        private void OnPinChanged(SerialPinChangedEventArgs e)
        {
            if (PinChanged != null)
                PinChanged(this, e);
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue("")]
        public string PortName
        {
            get
            {
                return Port!=null 
                           ? Port.PortName
                           : _portName;
            }
            set
            {
                if (Port != null)
                    Port.PortName = value;
                else
                    _portName = value;
            }
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue("")]
        public int BaudRate
        {
            get
            {
                return Port!=null 
                           ? Port.BaudRate
                           : _baudRate;
            }
            set
            {
                if (Port != null)
                    Port.BaudRate = value;
                else
                    _baudRate = value;
            }
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue("")]
        public int DataBits
        {
            get
            {
                return Port!=null 
                           ? Port.DataBits
                           : _dataBits;
            }
            set
            {
                if (Port != null)
                    Port.DataBits = value;
                else
                    _dataBits = value;
            }
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue("")]
        public StopBits StopBits
        {
            get
            {
                return Port!=null
                           ? Port.StopBits
                           : _stopBits;
            }
            set
            {
                if (Port != null)
                    Port.StopBits = value;
                else
                    _stopBits = value;
            }
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue("")]
        public Parity Parity
        {
            get
            {
                return Port!=null 
                           ? Port.Parity
                           : _parity;
            }
            set
            {
                if (Port != null)
                    Port.Parity = value;
                else
                    _parity = value;
            }
        }

        ///<summary>
        /// Открыт ли существующий порт
        ///</summary>
        public bool IsPortOpen
        {
            get { return Port != null && Port.IsOpen; }
        }

        #endregion

        #region constructors

        #endregion


        #region methods
        /// <summary>
        /// Создать порт
        /// </summary>
        /// <returns></returns>
        public SerialPort CreatePort()
        {
            SerialPort port = null;

            try
            {
                sendMessage(string.Format("Поиск порта {0}...", PortName));

                port = new SerialPort(PortName);
                port.BaudRate = _baudRate;
                port.DataBits = _dataBits;
                port.Parity = _parity;
                port.StopBits = _stopBits;

                sendMessage(string.Format("Порт {0} найден", PortName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message, ex);
            }


            Port = port;

            return port;
        }
        

        ///<summary>
        /// 
        ///</summary>
        ///<returns></returns>
        public bool TryOpenPort()
        {
            if (Port != null)
            {
                sendMessage(string.Format("Порт {0} открывается...", PortName));
                try
                {
                    Port.Open();
                    sendMessage(string.Format("Порт {0} успешно открыт", PortName));
                    return true;
                }
                catch (IOException ex)
                {
                    sendMessage(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.GetBaseException().Message, ex);
                }

            }
            else
            {
                sendMessage(string.Format("Порт {0} не создан", PortName));
            }

            sendMessage(string.Format("Ошибка при открытии порта {0}", PortName));
            return false;
        }

        ///<summary>
        ///
        ///</summary>
        public void ClosePort()
        {
            if (Port != null)
            {
                if (Port.IsOpen)
                {
                    Port.Close();
                    sendMessage(string.Format("Порт {0} закрыт", PortName));
                }
                else
                {
                    sendMessage(string.Format("Порт уже был {0} закрыт", PortName));
                }
            }
            else
            {
                sendMessage(string.Format("Порт {0} не был создан", PortName));
            }
        }

        internal void sendMessage(string logMessage)
        {
            sendMessage(logMessage, FlowType.Connect);
        }

        internal void sendMessage(string logMessage, FlowType flowType)
        {
            if (LogMessage!=null)
            {
                string msg = string.Format("{0}: {1}{2}", DateTime.Now, logMessage, Environment.NewLine);
                LogMessage(this, new SerialPortConnectMessageEventArgs(msg, flowType));
            }
        }

        #endregion



        #region events

        ///<summary>
        ///
        ///</summary>
        public event SerialPortConnectMessageEventHandler LogMessage;
        #endregion


    }

    public enum FlowType
    {
        Connect,
        Error,
        Input,
        Output
    }

    ///<summary>
    ///
    ///</summary>
    public delegate void SerialPortConnectMessageEventHandler(SerialPortConnect sender, SerialPortConnectMessageEventArgs eventArgs);

    ///<summary>
    ///
    ///</summary>
    public class SerialPortConnectMessageEventArgs
    {
        private readonly FlowType _flowType;
        private readonly string _message;

        #region fields

        #endregion

        #region properties

        #endregion

        #region constructors
        ///<summary>
        ///
        ///</summary>
        ///<param name="message"></param>
        ///<param name="flowType"></param>
        public SerialPortConnectMessageEventArgs(string message, FlowType flowType)
        {
            _message = message;
            _flowType = flowType;
        }

        #endregion

        public FlowType FlowType
        {
            get { return _flowType; }
        }

        ///<summary>
        ///
        ///</summary>
        public string Message
        {
            get { return _message; }
        }
    }
}