using System;
using System.IO.Ports;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// Параметры подключения к последовательной полевой шине
    ///</summary>
    public class SerialPortParameters : FieldBusPortParameters
    {
        #region fields

        private object _port;

        #endregion

        #region properties

        ///<summary>
        ///
        ///</summary>
        public string PortName { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int BaudRate { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int DataBits { get; set; }

        ///<summary>
        ///
        ///</summary>
        public StopBits StopBits { get; set; }

        ///<summary>
        ///
        ///</summary>
        public Parity Parity { get; set; }

        public object Port
        {
            get { return _port; }
            set
            {
                _port = value;
                PortName = value as string;
            }
        }

        public AsciiRtuMode Mode { get; set; }

        public byte MaxAddress { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// Параметры последовательного порта (по умолчанию)
        /// </summary>
        public SerialPortParameters():base(FieldBusType.RS485)
        {
            BaudRate = 9600;
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
        }

        #endregion


        #region methods
        ///<summary>
        /// Назначить порту данные параметры
        ///</summary>
        ///<param name="serialPort">Порт</param>
        ///<exception cref="ArgumentNullException"></exception>
        public void Assign(SerialPort serialPort)
        {
            if (serialPort == null) throw new ArgumentNullException("serialPort");

            serialPort.BaudRate = BaudRate;
            serialPort.DataBits = DataBits;
            serialPort.Parity = Parity;
            serialPort.StopBits = StopBits;
        }

        #endregion


    }
}