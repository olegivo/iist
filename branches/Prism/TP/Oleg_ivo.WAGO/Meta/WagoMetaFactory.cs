using System;
using System.Collections;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.WAGO.Devices;

namespace Oleg_ivo.WAGO.Meta
{
    ///<summary>
    ///
    ///</summary>
    public class WagoMetaFactory
    {
        private DtsWago _dtsWago;
        private readonly ILogicalChannelsFactory logicalChannelsFactory;

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="WagoMetaFactory" />.
        /// </summary>
        public WagoMetaFactory(ILogicalChannelsFactory logicalChannelsFactory)
        {
            this.logicalChannelsFactory = logicalChannelsFactory;
        }

        private void FillWagoMeta()
        {
            WagoMetaDAC wagoMetaDAC = new WagoMetaDAC(logicalChannelsFactory);
            wagoMetaDAC.FillChannelsFromDb(0);
            _dtsWago = wagoMetaDAC.DataSet;
        }

        private DtsWago.WagoMetaRow TryParsePredefined(int register)
        {
            if (_dtsWago == null)
                FillWagoMeta();

            DtsWago.WagoMetaRow row = null;
            if (_dtsWago != null)
            {
                row = _dtsWago.WagoMeta.FindById(register);
            }
        
            return row;
        }

        ///<summary>
        /// ������������ ���������� � ������������ ������ �� �������� � ��������� �� � ������ �������� ������
        ///</summary>
        ///<param name="register"></param>
        ///<param name="address"></param>
        ///<param name="wagoPlc"></param>
        ///<returns></returns>
        ///<exception cref="Exception"></exception>
        public WagoMeta ParseModuleMetaRegister(ushort register, ushort address, WagoPlc wagoPlc)
        {
            WagoMeta wagoMeta = null;
            bool isPlc;
            bool isInput = false;
            bool isOutput = false;
            bool isDiscrete = false;
            bool isAnalog = false;
            ushort size = 0;
            FieldBusType fieldBusType = FieldBusType.Unknown;

            DtsWago.WagoMetaRow predefined = TryParsePredefined(register);
            if (predefined != null)
            {//���� ���� ����������������� ���������� �� ��
                isPlc = predefined.IsPLC;
                isInput = predefined.IsInput;
                isOutput = predefined.IsOutput;
                isDiscrete = predefined.IsDescrete;
                isAnalog = predefined.IsAnalog;
                if (!predefined.IsSizeNull()) size = (ushort) predefined.Size;
                if (!predefined.IsFieldBusTypeIdNull()) fieldBusType = (FieldBusType) predefined.FieldBusTypeId;
            }
            else
            {//������ �� ����
                //hack: hardcode
                switch (register)
                {
                    case 841:
                        fieldBusType = FieldBusType.Ethernet;
                        break;
                }
                isPlc = (fieldBusType != FieldBusType.Unknown);

                if (!isPlc)
                {
                    BitArray bitArray = new BitArray(BitConverter.GetBytes(register));

                    isInput = bitArray[0];
                    isOutput = bitArray[1];
                    isDiscrete = bitArray[15];
                    isAnalog = !isDiscrete;

                    //�������� ������� 8-14 -> ������ ������ � �����:
                    bool[] array = new bool[8];
                    for (int i = 8; i < 15; i++) array[i - 8] = bitArray[i];
                    for (int i = 0; i < 8; i++)
                    {
                        //System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}. {2}", i, array[i], (int)(Math.Pow(2, i))));
                        if (array[i]) size += (ushort) (Math.Pow(2, i));
                    }

                    if(!isInput && !isOutput)
                        throw new Exception(
                            "������ �� �������� �� �������, �� ��������! ���������� ������������ �������� ������.");

                }


            }

            //����������, ����������
            if (isPlc)
            {
                switch (fieldBusType)
                {
                    case FieldBusType.Ethernet:
                        byte[] macID = wagoPlc != null ? wagoPlc.GetMacID() : null;
                        wagoMeta = new WagoPlcMetaEthernet(register, macID);
                        break;
                    default:
                        wagoMeta = new WagoPlcMeta(register);
                        break;
                }
            }
            else
            {
                try
                {
                    if (!isDiscrete)//��� ���������� �������
                        size *= 16;//������ � ��������� - ��������� � ���� (1 ������� = 16 ���)

                    WagoIOModuleMeta moduleMeta = new WagoIOModuleMeta(isAnalog, isDiscrete, isInput, isOutput, size,
                                                                       register, address);

                    wagoMeta = moduleMeta;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return wagoMeta;
        }
    }
}