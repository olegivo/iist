using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    ///<summary>
    /// ������� ����� ������� ����
    ///</summary>
    public abstract class FieldBusNodeFactory : IFieldBusNodeFactory
    {
        ///<summary>
        ///
        ///</summary>
        private readonly int _minimumRetries;

        ///<summary>
        ///
        ///</summary>
        private readonly int _minimumWaitToRetryMilliseconds;

        ///<summary>
        ///
        ///</summary>
        protected FieldBusNodeFactory()
        {
            _minimumRetries = 3;
            _minimumWaitToRetryMilliseconds = 3;
        }

        #region fields

        #endregion

        #region properties

        #endregion

        #region methods

        ///<summary>
        /// ��������� ���� ������� ����
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<returns></returns>
        public FieldBusNodeCollection BuildFieldBusNodes(FieldBusManager fieldBusManager)
        {
            var fieldNodesLevel = fieldBusManager.FieldBusLoadOptions.FieldNodesLevel;
            //��������� ���� ������� ���� �� ����������� ������������
            FieldBusNodeCollection loadedFieldBusNodes = fieldNodesLevel.LoadSavedConfiguration
                                                             ? LoadFieldBusNodes(fieldBusManager)
                                                             : new FieldBusNodeCollection();

            //���������� ���� ������� ���� �� ������� ������������
            FieldBusNodeCollection currentFieldBusNodes;

            if (fieldNodesLevel.ComputeCurrentConfiguration)
            {
                FieldBusType fieldBusType = fieldBusManager.FieldBusType;

                ActiveFieldBusManager activeFieldBusManager = fieldBusManager as ActiveFieldBusManager;
                if (activeFieldBusManager != null)
                {
                    activeFieldBusManager.InitializeModbusMaster();
                    currentFieldBusNodes = DistributedMeasurementInformationSystemBase.Instance.PlcManagerBase.FieldBusNodesFactory.FindNodes(activeFieldBusManager, activeFieldBusManager.FieldBusAccessor as ModbusAccessor);
                }
                else
                {
                    currentFieldBusNodes = new FieldBusNodeCollection();
                    currentFieldBusNodes.AddRange((from address in fieldBusManager.GetPLCAddressRange()
                                                   let modbusAccessor = DistributedMeasurementInformationSystemBase.Instance.PlcManagerBase.FieldBusFactory.CreateFieldbusAccessor(fieldBusType, address)
                                                   select new ActiveFieldBusNode(fieldBusManager, modbusAccessor, address)).Cast<FieldBusNode>());
                }

            }
            else
            {
                currentFieldBusNodes = new FieldBusNodeCollection();
            }

            if (currentFieldBusNodes.Count > 0 && loadedFieldBusNodes.Count > 0)
            {
                //�������� ������� ������������, �������� �����������
                foreach (var currentFieldBusNode in currentFieldBusNodes)
                {
                    //���� ����������� ����, ����������� � �������
                    var find =
                        loadedFieldBusNodes.FirstOrDefault(
                            loadedFieldBusNode =>
                            loadedFieldBusNode.EqualsPredicate(currentFieldBusNode));

                    if (find != null && currentFieldBusNode.Id != find.Id)//��������� ������ �� ������������ ���� � �������
                    {
                        currentFieldBusNode.Id = find.Id;
                    }
                }
            }
            else if (loadedFieldBusNodes.Count > 0)
            {
                //���� � ������� ������������ �����������, ��������� �� �� �����������
                currentFieldBusNodes = loadedFieldBusNodes;
            }

            InitFieldBusNodesPlc(currentFieldBusNodes);

            return currentFieldBusNodes;
        }

        /// <summary>
        /// ��������� ����������� ���� ��� ������� ����
        /// </summary>
        /// <param name="fieldBusManager"></param>
        /// <returns></returns>
        public abstract FieldBusNodeCollection LoadFieldBusNodes(FieldBusManager fieldBusManager);

        /// <summary>
        /// ������� ��� ��� ����� ������� ����
        /// </summary>
        /// <param name="fieldBusNodes"></param>
        private void InitFieldBusNodesPlc(IEnumerable<FieldBusNode> fieldBusNodes)
        {
            if (fieldBusNodes != null)
                foreach (FieldBusNode fieldBusNode in fieldBusNodes)
                    CreatePlcForFieldBusNode(fieldBusNode);
        }

        /// <summary>
        /// ������� ��� ��� ���� ������� ����
        /// </summary>
        /// <param name="fieldBusNode"></param>
        protected abstract void CreatePlcForFieldBusNode(FieldBusNode fieldBusNode);

        #endregion

        ///<summary>
        /// ���������, ��������� �� ��� ��� ���������� � ��������� ����� ������� ����
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        protected abstract bool CheckPLC(PLC plc);

        ///<summary>
        /// ����� ���� ������� ����, ������������ � ��������� ������� ����
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<param name="modbusAccessor"></param>
        ///<returns></returns>
        public FieldBusNodeCollection FindNodes(FieldBusManager fieldBusManager, ModbusAccessor modbusAccessor)
        {
            Debug.WriteLine("����� ���������, ������������ � ����� " + modbusAccessor.PortName);
            FieldBusNodeCollection nodes = new FieldBusNodeCollection();

            // ��������� ������ ������������ ��������
            ModbusAccessorTimeouts oldModbusAccessorTimeouts = modbusAccessor.ModbusAccessorTimeouts;
            ModbusAccessorTimeouts minimumModbusAccessorTimeouts = new ModbusAccessorTimeouts
            {
                WaitToRetryMilliseconds = _minimumWaitToRetryMilliseconds,
                Retries = _minimumRetries,
                ReadTimeout = 100,
                WriteTimeout = 100

            };

            try
            {
                modbusAccessor.ModbusAccessorTimeouts = minimumModbusAccessorTimeouts;

                // ����� ������ � ����� ������ ������� ���������
                // � ������� �� ��������� ��������� ����: 
                // ���� ���������� �� ������� ������ ��������, ������ ������ ������-���������� � ��� �����
                foreach (FieldBusNodeAddress fieldBusNodeAddress in fieldBusManager.GetPLCAddressRange())
                {
                    FieldBusNode fieldBusNode = CreateFieldBusNode(fieldBusManager, fieldBusNodeAddress);

                    if (TryGetReply(fieldBusNode))
                    {
                        Debug.WriteLine(string.Format("\n���������� �� ������ {0} �������� �� ��� �������.\n",
                                                      fieldBusNodeAddress.SlaveAddress));
                        nodes.Add(fieldBusNode);
                        //PLC plc =
                        //    DistributedMeasurementInformationSystemBase.Instance.PLCManager.PlcFactory.CreatePLC(
                        //        fieldBusNodeAddress, this);

                        //if (plc != null)
                        //    devices.Add(plc);
                    }
                    else
                    {
                        Debug.WriteLine(string.Format("\n���������� �� ������ {0} �� �������� �� ��� �������.\n",
                                                      fieldBusNodeAddress.SlaveAddress));
                    }
                }
            }
            finally
            {
                // ������ ������ ������������ ��������
                modbusAccessor.ModbusAccessorTimeouts = oldModbusAccessorTimeouts;
            }

            return nodes;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusManager"></param>
        ///<param name="fieldBusNodeAddress"></param>
        ///<returns></returns>
        public FieldBusNode CreateFieldBusNode(FieldBusManager fieldBusManager, FieldBusNodeAddress fieldBusNodeAddress)
        {
            FieldBusNode fieldBusNode;
            bool isNodeActive = false;
            switch (fieldBusManager.FieldBusType)
            {
                case FieldBusType.Ethernet:
                    isNodeActive = true;
                    break;
            }

            if (isNodeActive)
            {
                Debug.WriteLine("������ �������� ���� ������� ����...");
                IFieldBusAccessor fieldBusAccessor = DistributedMeasurementInformationSystemBase.Instance.PlcManagerBase.FieldBusFactory.CreateFieldbusAccessor(fieldBusManager.FieldBusType, fieldBusNodeAddress.SlaveAddress);
                fieldBusNode = new ActiveFieldBusNode(fieldBusManager, fieldBusAccessor, fieldBusNodeAddress);
            }
            else
            {
                Debug.WriteLine("������ ���� ������� ����...");
                fieldBusNode = new FieldBusNode(fieldBusManager, fieldBusNodeAddress);
            }
            return fieldBusNode;
        }

        /// <summary>
        /// ������� ���
        /// </summary>
        public IPlcFactory PlcFactory { get; set; }


        /// <summary>
        /// ������� �������� ����� �� ���������� �� ������� ������
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <returns></returns>
        protected abstract bool TryGetReply(IFieldBusNodeAccessor fieldBusNode);
    }
}