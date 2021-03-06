using System;
using NLog;
using Oleg_ivo.Base.Autofac;
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
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly FieldBusManager fieldBusManager;


        #region fields

        private PLC plc;
        private readonly Entities.FieldBusNode entity;
        #endregion

        #region properties

        /// <summary>
        /// ���
        /// </summary>
        public PLC Plc
        {
            get { return plc; }
        }

        /// <summary>
        /// ��������� ������� ����
        /// </summary>
        public FieldBusManager FieldBusManager
        {
            get { return fieldBusManager; }
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
                    Log.Debug("{0}: ���������� ������ �� ����������", this);


                return channels;
            }
        }

        /// <summary>
        /// ��������� ������� � �������� ������� ����
        /// </summary>
        public IFieldBusAccessor FieldBusAccessor
        {
            get { return FieldBusManager; }
        }

        ///<summary>
        /// �������������
        ///</summary>
        public int Id
        {
            get { return Entity.Id; }
            set { Entity.Id = value; }
        }

        public Entities.FieldBusNode Entity
        {
            get { return entity; }
        }

        public string AddressPart1//TODO:���� ���� FieldBusAddress, ������������ ������ ��� �������������� ���� � CMS
        {
            get { return Entity.AddressPart1; }
            set { Entity.AddressPart1 = value; }
        }

        public int AddressPart2//TODO:���� ���� FieldBusAddress, ������������ ������ ��� �������������� ���� � CMS
        {
            get { return Entity.AddressPart2.Value; }
            set { Entity.AddressPart2 = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <param name="entity"></param>
        public FieldBusNode(FieldBusManager fieldBusManager, Entities.FieldBusNode entity)
        {
            if (this is ActiveFieldBusNode && fieldBusManager is ActiveFieldBusManager) 
                throw new InvalidOperationException("ActiveFieldBusManager �� ����� ��������� ActiveFieldBusNode");
            this.fieldBusManager = Enforce.ArgumentNotNull(fieldBusManager,"fieldBusManager");
            this.entity = Enforce.ArgumentNotNull(entity,"entity");
        }

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
            this.plc = plc;
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
                Log.Debug("{0}: ���������� ������ �� �������. ��������� ���������� ������ �� ��������� ������.", this);

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
                Log.Debug("{0}: ���������� ������ �� �������. ��������� ���������� ������ ������.", this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <returns></returns>
        public bool EqualsPredicate(FieldBusNode fieldBusNode)
        {
            return AddressPart1 == fieldBusNode.AddressPart1 && AddressPart2 == fieldBusNode.AddressPart2;
        }
    }
}