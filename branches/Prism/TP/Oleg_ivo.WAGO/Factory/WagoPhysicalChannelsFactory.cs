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
    /// Фабрика физических каналов для ПЛК WAGO
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
        /// Построить физические каналы
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        public PhysicalChannelCollection BuildPhysicalChannels(PLC plc)
        {
            WagoPlc wagoPlc = plc as WagoPlc;
            if (wagoPlc != null)
            {
                //использование настроек построения уровня физических каналов
                var physicalChannelsLevel = plc.FieldBusNode.FieldBusManager.FieldBusLoadOptions.PhysicalChannelsLevel;
                //Загрузка каналов из сохранённой конфигурации
                PhysicalChannelCollection loadedPhysicalChannels = physicalChannelsLevel.LoadSavedConfiguration
                                                                       ? LoadPhysicalChannels(wagoPlc.FieldBusNode)
                                                                       : new PhysicalChannelCollection();

                //Получение каналов из текущей конфигурации
                if (!plc.FieldBusNode.IsOnline)
                    Log.Debug("Узел {0}. Невозможно начать построение физических каналов отключенного узла", plc.FieldBusNode);

                PhysicalChannelCollection currentPhysicalChannels = physicalChannelsLevel.ComputeCurrentConfiguration && plc.FieldBusNode.IsOnline
                                                                        ? GetCurrentPhysicalChannels(wagoPlc)
                                                                        : new PhysicalChannelCollection();
                
                LogChannelsConfiguration(plc, currentPhysicalChannels, loadedPhysicalChannels);

                // если есть каналы текущей конфигурации + загруженные каналы:
                if (currentPhysicalChannels.Count > 0 && loadedPhysicalChannels.Count > 0)
                {
                    //изменяем текущую конфигурацию, учитывая загруженную
                    foreach (var currentPhysicalChannel in currentPhysicalChannels)
                    {
                        //ищем загруженный канал, совпадающий с текущим
                        IEnumerable<PhysicalChannel> foundChannels = loadedPhysicalChannels.Where(
                            loadedChannel =>
                            loadedChannel.EqualsPredicate(currentPhysicalChannel, true, true, true)).ToArray();
                        
                        /*PhysicalChannel find;*/
                        int count = foundChannels.Count();
                        if(count!=1)
                        {
                            Log.Debug(
                                "Поиск дубликатов каналов пока не реализован. Найдено загруженных каналов, похожих на текущий: {0}. Текущий канал - {1}",
                                count, currentPhysicalChannel);
                        }

                        PhysicalChannel find = foundChannels.FirstOrDefault();

                        if (find != null)//переносим данные из загруженного канала в текущий
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
                    //каналы в текущей конфигурации отсутствуют, добавляем их из загруженной
                    currentPhysicalChannels = loadedPhysicalChannels;
                }

                wagoPlc.FieldBusNode.PhysicalChannels = currentPhysicalChannels;
                return wagoPlc.FieldBusNode.PhysicalChannels;
            }

            throw new ArgumentOutOfRangeException("plc", plc, "в параметре должен передаваться ПЛК WAGO");
        }

        /// <summary>
        /// Записать в файл текущую и сохранённую конфигурацию физических каналов
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
                                   : "нет элементов";

            string aggregateLoaded = loadedPhysicalChannels.Count > 0
                                         ? loadedPhysicalChannels.Select(
                                             pc =>
                                             string.Format("{0}\t{1}\t{2}", pc, pc.ReadAddress, pc.WriteAddress)).
                                               Aggregate((s, s2) => s + Environment.NewLine + s2)
                                         : "нет элементов";

            using (StreamWriter stream = new StreamWriter(string.Format(@"d:\{0}.txt", plc), false, Encoding.UTF8))
            {
                stream.WriteLine("currentPhysicalChannels:");
                stream.WriteLine(aggregate);
                stream.WriteLine("loadedPhysicalChannels:");
                stream.WriteLine(aggregateLoaded);
            }
        }

        /// <summary>
        /// Получение каналов из текущей конфигурации
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
                    if (moduleMeta != null)//метаданные модуля
                    {
                        WagoIOModule module = moduleMeta.CreateModule(logicalChannelsFactory);//создание модуля на основе метаданных
                        if (module != null)//создание физического канала на основе модуля
                        {
                            PhysicalChannel physicalChannel = new PhysicalChannel(null, logicalChannelsFactory, wagoPlc.FieldBusNode, module, 0, 0);
                            currentPhysicalChannels.Add(physicalChannel);//добавление ФК к узлу
                        }
                    }
                    else
                    {
                        WagoPlcMeta wagoPlcMeta = wagoMeta as WagoPlcMeta;
                        if (wagoPlcMeta != null)//метаданные ПЛК
                        {
                            wagoPlc.Meta = wagoPlcMeta;
                        }
                    }
                }

                // после построения проверяем уникальность
                CheckUniqueChannels(currentPhysicalChannels);
            }
            else
            {
                Log.Debug("Невозможно получить описание подключенных модулей ввода-вывода");
            }
            return currentPhysicalChannels;
        }

        /// <summary>
        /// Проверить уникальность каналов
        /// </summary>
        /// <param name="physicalChannels"></param>
        private void CheckUniqueChannels(IEnumerable<PhysicalChannel> physicalChannels)
        {
            var channelsList = physicalChannels as IList<PhysicalChannel> ?? physicalChannels.ToList();
            foreach (var physicalChannel in channelsList)
            {
                try
                {
                    //ищем единственный ряд с уникальными параметрами
                    var channels = channelsList.
                        Where(channel1 =>
                              channel1.EqualsPredicate(physicalChannel, true, true, true)
                        );

                    int count = channels.Count();
                    if (count==0)
                    {
                        throw new Exception("Не найдено ни одного канала");
                    }
                    if(count>1)
                    {
                        throw new Exception("Найдено несколько каналов");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Есть дубликаты в физических каналах", ex);
                }
            }
        }

        ///<summary>
        /// Сохранить настроенные физические каналы для физического канала
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        [Obsolete("Здесь вызывается код из PhysicalChannelsDAC, там осуществляется перенос данных из ЛК в датасет с последующим сохранением. Нужно пользоваться DataContext.SubmitChanges")]
        public void SavePhysicalChannels(FieldBusNode fieldBusNode)
        {
            var physicalChannelsDAC = new PhysicalChannelsDAC();
            physicalChannelsDAC.SaveChannels(fieldBusNode);
        }

        /// <summary>
        ///  Загрузить настроенные физические каналы для узла полевой шины
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