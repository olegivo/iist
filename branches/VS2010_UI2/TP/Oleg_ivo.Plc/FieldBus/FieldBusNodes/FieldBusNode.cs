using System;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Common;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    ///<summary>
    /// ���� ������� ����.
    /// ��������� ������ � �������� ���� ������� ����.
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
        /// ���
        /// </summary>
// ReSharper disable MemberCanBePrivate.Global
        public PLC Plc
// ReSharper restore MemberCanBePrivate.Global
        {
            get { return _plc; }
        }

        /// <summary>
        /// ��������� ������� ����
        /// </summary>
        public FieldBusManager FieldBusManager
        {
            get { return _fieldBusManager; }
        }

        ///<summary>
        /// ���������� ������ ���� ���� ������� ����
        ///</summary>
        public PhysicalChannelCollection PhysicalChannels { get; set; }

        ///<summary>
        /// ���������� ������ ������� ���� ������� ����
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
                    Console.WriteLine("{0}: ���������� ������ �� ����������", this);


                return channels;
            }
        }

        ///<summary>
        /// ����� ���� � ������� ����
        ///</summary>
        public FieldBusNodeAddress FieldBusNodeAddress
        {
            get { return _fieldBusNodeAddress; }
        }

        /// <summary>
        /// ��������� ������� � �������� ������� ����
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
        /// ��������� ���������� ������ ��� ���� ������� ����
        /// </summary>
        public void BuildPhysicalChannels()
        {
            if (Plc != null)//��� ������������� �� ���������� ������� �����-������
            {
                Plc.BuildPhysicalChannels();//���������� ���������� �������
            }

            //���������� ���������� �������
            BuildLogicalChannels();
        }

        /// <summary>
        /// ��������� ���������� ������ ��� ������� ���� ������� ����
        /// </summary>
        private void BuildLogicalChannels()
        {
            //�������� ���������� �������
            LoadLogicalChannels();
            
            BuildDefaultLogicalChannels();
        }

        #endregion

        #region Implementation of IFieldBusNodeAccessor

        /// <summary>
        /// ��������� ��������
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual ushort[] ReadHoldingRegisters(ushort address, ushort numberOfPoints)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.ReadHoldingRegisters(this, address, numberOfPoints);
            throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
        }

        /// <summary>
        /// ��������� ������� ��������
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual ushort[] ReadInputRegisters(ushort address, ushort numberOfPoints)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.ReadInputRegisters(this, address, numberOfPoints);
            throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
        }

        ///<summary>
        /// �������� � �������
        ///</summary>
        ///<param name="address"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        public virtual bool WriteSingleRegister(ushort address, ushort value)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteSingleRegister(this, address, value);
            throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
        }

        /// <summary>
        /// ��������� ����
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public virtual bool[] ReadCoils(ushort address, ushort numberOfPoints)
        {
            if (FieldBusManager != null) 
                return FieldBusManager.ReadCoils(this, address, numberOfPoints);
            throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
        }

        /// <summary>
        /// �������� ���� ���
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool WriteSingleCoil(ushort address, bool value)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteSingleCoil(this, address, value);
            throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
        }

        ///<summary>
        /// �������� ��������� ���������
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public virtual bool WriteMultipleRegisters(ushort address, ushort[] values)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteMultipleRegisters(this, address, values);
            throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
        }

        ///<summary>
        /// �������� ��������� �����
        ///</summary>
        ///<param name="address"></param>
        ///<param name="values"></param>
        ///<returns></returns>
        public virtual bool WriteMultipleCoils(ushort address, bool[] values)
        {
            if (FieldBusManager != null && FieldBusManager is ActiveFieldBusManager)
                return FieldBusManager.WriteMultipleCoils(this, address, values);
            throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
        }

        /// <summary>
        /// ���������������� ���������� �� Modbus 
        /// </summary>
        public virtual void InitializeModbusMaster()
        {
            if ((FieldBusManager!=null && FieldBusManager is ActiveFieldBusManager))
            {
                FieldBusManager.InitializeModbusMaster();
            }
            else
                throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");

        }

        ///<summary>
        /// ���� ������� ���� ������
        ///</summary>
        public virtual bool IsOnline
        {
            get
            {
                ActiveFieldBusManager activeFieldBusManager = FieldBusManager as ActiveFieldBusManager;
                if (activeFieldBusManager != null)
                    return FieldBusManager.IsOnline;

                throw new InvalidOperationException("�� �������� ��������� ������� ����. �������� �� ����� ���� ���������.");
            }
            protected set { throw new NotImplementedException(); }
        }

        ///<summary>
        /// ��������� ����������� ���� ������� ����
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
            return Plc!=null ? Plc.ToString() : "��� �� ��������";
        }

        /// <summary>
        /// ���������������� ���� ������� ���� ������������
        /// </summary>
        /// <param name="plc"></param>
        internal protected /*virtual*/ void InitPlc(PLC plc)
        {
            _plc = plc;
        }

        ///<summary>
        /// ��������� ���������� ������ �� ��������� ��� ������� ���� ������� ����
        ///</summary>
        public void BuildDefaultLogicalChannels()
        {
            if (PhysicalChannels != null)
                foreach (PhysicalChannel physicalChannel in PhysicalChannels)
                    physicalChannel.BuildDefaultLogicalChannels();
            else
                Console.WriteLine("{0}: ���������� ������ �� �������. ��������� ���������� ������ �� ��������� ������.", this);

        }

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ������� ���� ������� ����
        ///</summary>
        public void LoadLogicalChannels()
        {
            if (FieldBusManager.FieldBusLoadOptions.LogicalChannelsLevel.LoadSavedConfiguration && PhysicalChannels != null)
                foreach (PhysicalChannel physicalChannel in PhysicalChannels)
                    physicalChannel.LoadLogicalChannels();
            else
                Console.WriteLine("{0}: ���������� ������ �� �������. ��������� ���������� ������ ������.", this);
        }

        ///<summary>
        /// �������������
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