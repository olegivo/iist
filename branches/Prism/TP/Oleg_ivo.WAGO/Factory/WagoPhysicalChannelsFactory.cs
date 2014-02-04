using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NLog;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.WAGO.Devices;
using Oleg_ivo.WAGO.Meta;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    /// ������� ���������� ������� ��� ��� WAGO
    ///</summary>
    public class WagoPhysicalChannelsFactory : FactoryBase, IPhysicalChannelsFactory
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly ILogicalChannelsFactory logicalChannelsFactory;

        public WagoPhysicalChannelsFactory(ILogicalChannelsFactory logicalChannelsFactory)
        {
            this.logicalChannelsFactory = logicalChannelsFactory;
        }

        ///<summary>
        /// ��������� ���������� ������
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        public PhysicalChannelCollection BuildPhysicalChannels(PLC plc)
        {
            WagoPlc wagoPlc = plc as WagoPlc;
            if (wagoPlc != null)
            {
                //������������� �������� ���������� ������ ���������� �������
                var physicalChannelsLevel = plc.FieldBusNode.FieldBusManager.FieldBusLoadOptions.PhysicalChannelsLevel;
                //�������� ������� �� ���������� ������������
                PhysicalChannelCollection loadedPhysicalChannels = physicalChannelsLevel.LoadSavedConfiguration
                                                                       ? LoadPhysicalChannels(wagoPlc.FieldBusNode)
                                                                       : new PhysicalChannelCollection();

                //��������� ������� �� ������� ������������
                if (!plc.FieldBusNode.IsOnline)
                    Log.Debug("���� {0}. ���������� ������ ���������� ���������� ������� ������������ ����", plc.FieldBusNode);

                PhysicalChannelCollection currentPhysicalChannels = physicalChannelsLevel.ComputeCurrentConfiguration && plc.FieldBusNode.IsOnline
                                                                        ? GetCurrentPhysicalChannels(wagoPlc)
                                                                        : new PhysicalChannelCollection();
                
                LogChannelsConfiguration(plc, currentPhysicalChannels, loadedPhysicalChannels);

                // ���� ���� ������ ������� ������������ + ����������� ������:
                if (currentPhysicalChannels.Count > 0 && loadedPhysicalChannels.Count > 0)
                {
                    //�������� ������� ������������, �������� �����������
                    foreach (var currentPhysicalChannel in currentPhysicalChannels)
                    {
                        //���� ����������� �����, ����������� � �������
                        IEnumerable<PhysicalChannel> foundChannels = loadedPhysicalChannels.Where(
                            loadedChannel =>
                            loadedChannel.EqualsPredicate(currentPhysicalChannel, true, true, true)).ToArray();
                        
                        /*PhysicalChannel find;*/
                        int count = foundChannels.Count();
                        if(count!=1)
                        {
                            Log.Debug(
                                "����� ���������� ������� ���� �� ����������. ������� ����������� �������, ������� �� �������: {0}. ������� ����� - {1}",
                                count, currentPhysicalChannel);
                        }

                        PhysicalChannel find = foundChannels.FirstOrDefault();

                        if (find != null)//��������� ������ �� ������������ ������ � �������
                        {
                            currentPhysicalChannel.Id = find.Id;
                            //currentPhysicalChannel.Id = find.Id;
                            //currentPhysicalChannel.ReadAddress = find.ReadAddress;
                            //currentPhysicalChannel.WriteAddress = find.WriteAddress;
                        }
                    }
                }
                else if(loadedPhysicalChannels.Count > 0)
                {
                    //������ � ������� ������������ �����������, ��������� �� �� �����������
                    currentPhysicalChannels = loadedPhysicalChannels;
                }

                wagoPlc.FieldBusNode.PhysicalChannels = currentPhysicalChannels;
                return wagoPlc.FieldBusNode.PhysicalChannels;
            }

            throw new ArgumentOutOfRangeException("plc", plc, "� ��������� ������ ������������ ��� WAGO");
        }

        /// <summary>
        /// �������� � ���� ������� � ���������� ������������ ���������� �������
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="currentPhysicalChannels"></param>
        /// <param name="loadedPhysicalChannels"></param>
        private static void LogChannelsConfiguration(PLC plc, PhysicalChannelCollection currentPhysicalChannels, PhysicalChannelCollection loadedPhysicalChannels)
        {
            string aggregate = currentPhysicalChannels.Count > 0
                                   ? currentPhysicalChannels.Select(
                                       pc => string.Format("{0}\t{1}\t{2}", pc, pc.ReadAddress, pc.WriteAddress)).
                                         Aggregate((s, s2) => s + Environment.NewLine + s2)
                                   : "��� ���������";

            string aggregateLoaded = loadedPhysicalChannels.Count > 0
                                         ? loadedPhysicalChannels.Select(
                                             pc =>
                                             string.Format("{0}\t{1}\t{2}", pc, pc.ReadAddress, pc.WriteAddress)).
                                               Aggregate((s, s2) => s + Environment.NewLine + s2)
                                         : "��� ���������";

            using (StreamWriter stream = new StreamWriter(string.Format(@"d:\{0}.txt", plc), false, Encoding.UTF8))
            {
                stream.WriteLine("currentPhysicalChannels:");
                stream.WriteLine(aggregate);
                stream.WriteLine("loadedPhysicalChannels:");
                stream.WriteLine(aggregateLoaded);
            }
        }

        /// <summary>
        /// ��������� ������� �� ������� ������������
        /// </summary>
        /// <param name="wagoPlc"></param>
        /// <returns></returns>
        private PhysicalChannelCollection GetCurrentPhysicalChannels(WagoPlc wagoPlc)
        {
            PhysicalChannelCollection currentPhysicalChannels = new PhysicalChannelCollection();

            IEnumerable<WagoMeta> wagoMetas = wagoPlc.GetModulesMeta();
            if (wagoMetas != null)
            {
                foreach (WagoMeta wagoMeta in wagoMetas)
                {
                    WagoIOModuleMeta moduleMeta = wagoMeta as WagoIOModuleMeta;
                    if (moduleMeta != null)//���������� ������
                    {
                        WagoIOModule module = moduleMeta.CreateModule(logicalChannelsFactory);//�������� ������ �� ������ ����������
                        if (module != null)//�������� ����������� ������ �� ������ ������
                        {
                            PhysicalChannel physicalChannel = new PhysicalChannel(null, logicalChannelsFactory, wagoPlc.FieldBusNode, module, 0, 0);
                            currentPhysicalChannels.Add(physicalChannel);//���������� �� � ����
                        }
                    }
                    else
                    {
                        WagoPlcMeta wagoPlcMeta = wagoMeta as WagoPlcMeta;
                        if (wagoPlcMeta != null)//���������� ���
                        {
                            wagoPlc.Meta = wagoPlcMeta;
                        }
                    }
                }

                // ����� ���������� ��������� ������������
                CheckUniqueChannels(currentPhysicalChannels);
            }
            else
            {
                Log.Debug("���������� �������� �������� ������������ ������� �����-������");
            }
            return currentPhysicalChannels;
        }

        /// <summary>
        /// ��������� ������������ �������
        /// </summary>
        /// <param name="physicalChannels"></param>
        private void CheckUniqueChannels(IEnumerable<PhysicalChannel> physicalChannels)
        {
            var channelsList = physicalChannels as IList<PhysicalChannel> ?? physicalChannels.ToList();
            foreach (var physicalChannel in channelsList)
            {
                try
                {
                    //���� ������������ ��� � ����������� �����������
                    var channels = channelsList.
                        Where(channel1 =>
                              channel1.EqualsPredicate(physicalChannel, true, true, true)
                        );

                    int count = channels.Count();
                    if (count==0)
                    {
                        throw new Exception("�� ������� �� ������ ������");
                    }
                    if(count>1)
                    {
                        throw new Exception("������� ��������� �������");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("���� ��������� � ���������� �������", ex);
                }
            }
        }

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        [Obsolete("����� ���������� ��� �� PhysicalChannelsDAC, ��� �������������� ������� ������ �� �� � ������� � ����������� �����������. ����� ������������ DataContext.SubmitChanges")]
        public void SavePhysicalChannels(FieldBusNode fieldBusNode)
        {
            var physicalChannelsDAC = new PhysicalChannelsDAC();
            physicalChannelsDAC.SaveChannels(fieldBusNode);
        }

        /// <summary>
        ///  ��������� ����������� ���������� ������ ��� ���� ������� ����
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <returns></returns>
        public PhysicalChannelCollection LoadPhysicalChannels(FieldBusNode fieldBusNode)
        {
            var channels = new PhysicalChannelCollection();

            channels.AddRange(from Plc.Entities.PhysicalChannel entity in DataContext.PhysicalChannels.Where(pc => pc.FieldNodeId == fieldBusNode.Id)
                              select CreateChannelFromData(entity, fieldBusNode));

            return channels;

            //PhysicalChannelsDAC physicalChannelsDAC = new PhysicalChannelsDAC{LogicalChannelsFactory = logicalChannelsFactory};
            //return physicalChannelsDAC.GetChannels(fieldBusNode);
        }

        private PhysicalChannel CreateChannelFromData(Plc.Entities.PhysicalChannel entity, FieldBusNode fieldBusNode)
        {
            var ioModule = new WagoIOModule(logicalChannelsFactory)
            {
                Meta =
                    new WagoIOModuleMeta(entity.IsAnalog, entity.IsDiscrete, entity.IsInput, entity.IsOutput,
                                         (ushort)entity.PhysicalChannelSize.Value, 0, entity.AddressShift.Value)
            };

            var channel = new PhysicalChannel(entity, logicalChannelsFactory, fieldBusNode, ioModule, (ushort) entity.AddressShift, (ushort) entity.PhysicalChannelSize)
            {
                Id = entity.Id,
                WriteAddress = entity.WriteAddress.Value,
                ReadAddress = entity.ReadAddress.Value
            };
            return channel;
        }

    }
}