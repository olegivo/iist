using System;
using System.Collections.Generic;
using System.Linq;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.WAGO.Meta;

namespace Oleg_ivo.WAGO.Devices
{
    ///<summary>
    /// ��� WAGO
    ///</summary>
    public class WagoPlc : PLC, IWagoMetaContainer
    {
        private readonly IPhysicalChannelsFactory physicalChannelsFactory;
        private WagoMetaFactory wagoMetaFactory;

        ///<summary>
        ///
        ///</summary>
        private const ushort ADDRESS_CONNECTED_IO_MODULES = 0x2030;

        ///<summary>
        ///
        ///</summary>
        private const ushort ADDRESS_MAC_ID = 0x1031;


        #region fields

        #endregion

        #region properties

        #endregion

        #region constructors

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <param name="physicalChannelsFactory"></param>
        public WagoPlc(FieldBusNode fieldBusNode, IPhysicalChannelsFactory physicalChannelsFactory, WagoMetaFactory wagoMetaFactory) : base(fieldBusNode)
        {
            this.physicalChannelsFactory = physicalChannelsFactory;
            this.wagoMetaFactory = wagoMetaFactory;
        }

        ///<summary>
        /// ����-��������
        ///</summary>
        public WagoMeta Meta { get; set; }

        /// <summary>
        /// �������� �������� ������������ ������� �����-������
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">��� ����������� ��������������� ������� ������</exception>
        /// <returns></returns>
        public IEnumerable<WagoMeta> GetModulesMeta()
        {
            List<WagoMeta> moduleMetas = null;
            ushort[] modulesRegisters = GetMetaModulesRegisters();

            if (modulesRegisters != null)
            {
                moduleMetas = new List<WagoMeta>();
                ushort addressShift = 0;//������� ����� ������
                foreach (ushort register in modulesRegisters)
                {
                    if (register > 0)
                    {
                        WagoMeta wagoMeta = ParseModuleMetaRegister(register, addressShift);
                        if (wagoMeta != null)
                        {
                            moduleMetas.Add(wagoMeta);
                            WagoIOModuleMeta moduleMeta = wagoMeta as WagoIOModuleMeta;
                            if (moduleMeta!=null)
                            {
                                ushort shift;
                                if (moduleMeta.IsDiscrete)
                                {
                                    //����� �� ����� ���������� ������� (���������� �����)
                                    shift = moduleMeta.Size;
                                }
                                else if(moduleMeta.IsAnalog && (moduleMeta.Size % 16) == 0)
                                {
                                    //����� �� ����� ���������� ������� (���������� ���������)
                                    shift = (ushort)(moduleMeta.Size / 16);
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException(string.Format("��������� ����������� ������: {0}", moduleMeta));
                                }

                                addressShift += shift;
                            }
                        }
                    }
                }

                SortAndSizeModules(moduleMetas);
            }
            
            return moduleMetas;
        }

        private void SortAndSizeModules(List<WagoMeta> wagoMetas)
        {
            wagoMetas.Sort(new WagoIoModuleComparer());
            ushort addressShiftInputRead = 0;//������� ����� ������ ������ ������� �������
            ushort addressShiftOutputRead = 512;//������� ����� ������ ������ �������� �������
            ushort addressShiftWriteAnalog = 0;//������� ����� ������ ������ ���������� �������
            ushort addressShiftWriteDiscrete = 0;//������� ����� ������ ������ ���������� �������

            foreach (WagoMeta wagoMeta in wagoMetas)
            {
                WagoIOModuleMeta moduleMeta = wagoMeta as WagoIOModuleMeta;
                if (moduleMeta != null)
                {
                    if (!moduleMeta.IsInput && !moduleMeta.IsOutput)
                    {
                        throw new ArgumentOutOfRangeException(string.Format("������ �� ������� � �� ��������: {0}",
                                                                            moduleMeta));
                    }
                    if(moduleMeta.IsInput && moduleMeta.IsOutput)
                    {
                        throw new ArgumentOutOfRangeException(string.Format("������ � �������, � �������� ������������: {0}",
                                                                            moduleMeta));
                    }
                    if (!moduleMeta.IsAnalog && !moduleMeta.IsDiscrete)
                    {
                        throw new ArgumentOutOfRangeException(string.Format("������ �� ���������� � �� ����������: {0}",
                                                                                moduleMeta));
                    }
                    if (moduleMeta.IsAnalog && moduleMeta.IsDiscrete)
                    {
                        throw new ArgumentOutOfRangeException(string.Format("������ � ����������, � ���������� ������������: {0}",
                                                                                moduleMeta));
                    }

                    //����������� ������ � ��������� modbus ��� ���������� ������:
                    ushort shift;
                    if (moduleMeta.IsAnalog)
                    {
                        //���������� ������
                        if (moduleMeta.Size % 16 != 0)
                            throw new ArgumentOutOfRangeException(string.Format("��������� ����������� ������: {0}",
                                                                                moduleMeta));
                        shift = (ushort)(moduleMeta.Size / 16);
                    }
                    else
                    {
                        shift = moduleMeta.Size;
                    }

                    //��������� ������� ������ � ������, ������ � ��������� modbus ��� ��������� �������
                    if (moduleMeta.IsOutput)
                    {
                        if (moduleMeta.IsAnalog)
                        {
                            moduleMeta.WriteAddress = addressShiftWriteAnalog;
                            addressShiftWriteAnalog += shift;
                        }
                        else
                        {
                            moduleMeta.WriteAddress = addressShiftWriteDiscrete;
                            addressShiftWriteDiscrete += shift;
                        }

                        moduleMeta.ReadAddress = addressShiftOutputRead;
                        addressShiftOutputRead += shift;
                    }
                    else if (moduleMeta.IsInput)
                    {
                        moduleMeta.ReadAddress = addressShiftInputRead;
                        addressShiftInputRead += shift;
                    }

                }
            }
        }

        /// <summary>
        /// ������������ ���������� � ������������ ������ �� �������� � ��������� �� � ������ �������� ������
        /// </summary>
        /// <param name="register"></param>
        /// <param name="address">�������� ������</param>
        /// <returns></returns>
        private WagoMeta ParseModuleMetaRegister(ushort register, ushort address)
        {
            return wagoMetaFactory.ParseModuleMetaRegister(register, address, this);
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        protected internal byte[] GetMacID()
        {
            ushort[] registers = FieldBusNode.ReadHoldingRegisters(ADDRESS_MAC_ID, 3);
            return registers.Select(register => (byte) register).ToArray();
        }

        ///<summary>
        /// ������� ������ ������������ ������ �� �������� 0x2030 (Modbus ����� 408241, ����� 65 ����)
        ///</summary>
        ///<returns></returns>
        private ushort[] GetMetaModulesRegisters()
        {
            ushort[] registers = FieldBusNode.ReadHoldingRegisters(ADDRESS_CONNECTED_IO_MODULES, 65);
            return registers;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="address"></param>
        ///<returns></returns>
        public ushort ReadInputRegister(ushort address)
        {
            //ushort[] registers = null;
            //registers = ModbusAccessor.ReadInputRegisters(plcAddress.SlaveAddress, address, 1);
            //return registers[0];
            return 0;
        }

        /// <summary>
        /// ��������� ���������� ������
        /// </summary>
        public override void BuildPhysicalChannels()
        {
            PhysicalChannelCollection physicalChannels = physicalChannelsFactory.BuildPhysicalChannels(this);
            if (physicalChannels==null)
            {
                Console.WriteLine("�� ��������� �� ������ ����������� ������");
            }
            else
            {
                Console.WriteLine("��������� ���������� �������: {0}", physicalChannels.Count);
            }
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
            return Meta!=null ? Meta.ToString() : base.ToString();
        }
    }
}