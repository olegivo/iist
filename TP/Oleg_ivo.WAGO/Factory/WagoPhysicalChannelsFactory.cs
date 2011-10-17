using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public class WagoPhysicalChannelsFactory : IPhysicalChannelsFactory
    {
        #region Singleton

        private static WagoPhysicalChannelsFactory _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static WagoPhysicalChannelsFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WagoPhysicalChannelsFactory();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WagoPhysicalChannelsFactory" />.
        /// </summary>
        private WagoPhysicalChannelsFactory()
        {
        }

        #endregion

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
                    Console.WriteLine("Узел {0}. Невозможно начать построение физических каналов отключенного узла", plc.FieldBusNode);

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
                            Console.WriteLine(
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

            using (StreamWriter stream = new StreamWriter(string.Format(@"c:\{0}.txt", plc), false, Encoding.UTF8))
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
                        WagoIOModule module = moduleMeta.CreateModule();//создание модуля на основе метаданных
                        if (module != null)//создание физического канала на основе модуля
                        {
                            PhysicalChannel physicalChannel = new PhysicalChannel(wagoPlc.FieldBusNode, module, 0, 0);
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
                Console.WriteLine("Невозможно получить описание подключенных модулей ввода-вывода");
            }
            return currentPhysicalChannels;
        }

        /// <summary>
        /// Проверить уникальность каналов
        /// </summary>
        /// <param name="physicalChannels"></param>
        private void CheckUniqueChannels(IEnumerable<PhysicalChannel> physicalChannels)
        {
            foreach (var physicalChannel in physicalChannels)
            {
                try
                {
                    //ищем единственный ряд с уникальными параметрами
                    IEnumerable<PhysicalChannel> channels = physicalChannels.
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
        public void SavePhysicalChannels(FieldBusNode fieldBusNode)
        {
            PhysicalChannelsDAC physicalChannelsDAC = new PhysicalChannelsDAC();
            physicalChannelsDAC.SaveChannels(fieldBusNode);
        }

        ///<summary>
        /// Загрузить настроенные физические каналы для узла полевой шины
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        public PhysicalChannelCollection LoadPhysicalChannels(FieldBusNode fieldBusNode)
        {
            PhysicalChannelsDAC physicalChannelsDAC = new PhysicalChannelsDAC();
            return physicalChannelsDAC.GetChannels(fieldBusNode);
        }
    }
}