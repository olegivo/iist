using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WAGOConfigurationImporter.Annotations;
using WAGOConfigurationImporter.Plc27DataSetTableAdapters;


namespace WAGOConfigurationImporter
{


    public class ImportProcessor : INotifyPropertyChanged
    {

        private XDocument xConfig;
        public List<ModuleInfo> modulesInfos { get; set; }

        public SignalType? ParceSignalType(string content)
        {
            if (Regex.Match(content, @"(DO|DI)", RegexOptions.IgnoreCase).Success == true)
                return SignalType.Descrete;
            else if (Regex.Match(content, @"(AO|AI)", RegexOptions.IgnoreCase).Success == true)
                return SignalType.Analog;
            return null;
        }

        public IOType? ParceIOType(string content)
        {
            if (Regex.Match(content, @"(DO|AO)", RegexOptions.IgnoreCase).Success == true)
                return IOType.Output;
            else if (Regex.Match(content, @"(DI|AI)", RegexOptions.IgnoreCase).Success == true)
                return IOType.Input;
            return null;
        }



        public void ParceXML(XDocument configFile)
        {
            this.modulesInfos = new List<ModuleInfo>();
            IEnumerable<XElement> moduleElements = from xml2 in configFile.Descendants("module") select xml2;
            foreach (var moduleElement in moduleElements)
            {
                
                this.modulesInfos.Add(new ModuleInfo()
                    {
                        ModuleName = moduleElement.Attribute("module-name").Value.ToString(),
                        ModuleNo = moduleElement.Attribute("article-no").Value.ToString(),
                        PhysicalPosition = Convert.ToInt32(moduleElement.Attribute("physical-pos").Value),
                        ChannelsCount = Convert.ToInt32(moduleElement.Descendants("channel").Count()),
                        SignalType = ParceSignalType(moduleElement.Attribute("module-name").Value),
                        IoType = ParceIOType(moduleElement.Attribute("module-name").Value)
                    })
                ;
            }
            
            OnPropertyChanged("modulesInfos");
        }

        public bool CheckConfigAlreadyExists(string controllerIP)
        {
            return false;
        }

        public DataRow CreateNewFieldBusNodeRow(Plc27DataSet plc27DataSet, string ip)
        {
            DataRow filedBusNodeRow = plc27DataSet.FieldBusNode.NewRow();
            if (Regex.Match(ip, @"(?:\d{1,3}\.){3}\d{1,3}").Success == true)
            {

                filedBusNodeRow[plc27DataSet.FieldBusNode.FieldBusTypeIdColumn] = 3;
                filedBusNodeRow[plc27DataSet.FieldBusNode.AddressPart1Column] = ip;
                filedBusNodeRow[plc27DataSet.FieldBusNode.AddressPart2Column] = 502;
                filedBusNodeRow[plc27DataSet.FieldBusNode.EnabledColumn] = true;

            }
            else
            {
                throw new Exception("IP адрес указан неверно!");
            }
            return filedBusNodeRow;

        }

        //public void PreparePhysicalChnnels(Plc27DataSet plc27DataSet, int fieldNodeId)
        //{
        //    //дискретные входные
        //        DataRow anyRow = plc27DataSet.PhysicalChannel.NewRow();
        //        //anyRow[0] = DBNull.Value;//id
        //        anyRow[plc27DataSet.PhysicalChannel.FieldNodeIdColumn] = fieldNodeId;//filedbusnodeId
        //        anyRow[plc27DataSet.PhysicalChannel.AddressShiftColumn] = 0;//для выходных модулей смещение = 512
        //        anyRow[plc27DataSet.PhysicalChannel.ReadAddressColumn] = 0;//readaddress
        //        anyRow[plc27DataSet.PhysicalChannel.WriteAddressColumn] = 0;//writeaddress
        //        anyRow[plc27DataSet.PhysicalChannel.PhysicalChannelSizeColumn] =
        //        modulesInfos.Where(x => x.IoType == IOType.Input && x.SignalType == SignalType.Descrete).Sum(x=>x.ChannelsCount);
        //        anyRow[plc27DataSet.PhysicalChannel.IsInputColumn] = 1;//isInput
        //        anyRow[plc27DataSet.PhysicalChannel.IsOutputColumn] = 0;//isOutput
        //        anyRow[plc27DataSet.PhysicalChannel.IsAnalogColumn] = 0;//isAnalog
        //        anyRow[plc27DataSet.PhysicalChannel.IsDiscreteColumn] = 1;//isdescrete
        //        anyRow[plc27DataSet.PhysicalChannel.DescriptionColumn] = "new discrete input module - " + fieldNodeId;//description
        //        //anyRow[11] = DBNull.Value;//register
        //        plc27DataSet.PhysicalChannel.Rows.Add(anyRow);

        //    //дискретные выходные
        //        DataRow anyRow1 = plc27DataSet.PhysicalChannel.NewRow();
        //        anyRow1[plc27DataSet.PhysicalChannel.FieldNodeIdColumn] = fieldNodeId;
        //        anyRow1[plc27DataSet.PhysicalChannel.ReadAddressColumn] = 0;
        //        anyRow1[plc27DataSet.PhysicalChannel.WriteAddressColumn] = 0;
        //        anyRow1[plc27DataSet.PhysicalChannel.AddressShiftColumn] = 512;
        //        anyRow1[plc27DataSet.PhysicalChannel.PhysicalChannelSizeColumn] =
        //        modulesInfos.Where(x => x.IoType == IOType.Output && x.SignalType == SignalType.Descrete).Sum(x => x.ChannelsCount);
        //        anyRow1[plc27DataSet.PhysicalChannel.IsInputColumn] = 0;
        //        anyRow1[plc27DataSet.PhysicalChannel.IsOutputColumn] = 1;
        //        anyRow1[plc27DataSet.PhysicalChannel.IsAnalogColumn] = 0;
        //        anyRow1[plc27DataSet.PhysicalChannel.IsDiscreteColumn] = 1;
        //        anyRow1[plc27DataSet.PhysicalChannel.DescriptionColumn] = "new discrete output module " + fieldNodeId;
        //        plc27DataSet.PhysicalChannel.Rows.Add(anyRow1);

        //    //аналогоые входные
        //        DataRow anyRow2 = plc27DataSet.PhysicalChannel.NewRow();
        //        anyRow2[plc27DataSet.PhysicalChannel.FieldNodeIdColumn] = fieldNodeId;
        //        anyRow2[plc27DataSet.PhysicalChannel.ReadAddressColumn] = 0;
        //        anyRow2[plc27DataSet.PhysicalChannel.WriteAddressColumn] = 0;
        //        anyRow2[plc27DataSet.PhysicalChannel.AddressShiftColumn] = 0;
        //        anyRow2[plc27DataSet.PhysicalChannel.PhysicalChannelSizeColumn] =
        //        modulesInfos.Where(x => x.IoType == IOType.Input && x.SignalType == SignalType.Analog).Sum(x => x.ChannelsCount*16);
        //        anyRow2[plc27DataSet.PhysicalChannel.IsInputColumn] = 1;
        //        anyRow2[plc27DataSet.PhysicalChannel.IsOutputColumn] = 0;
        //        anyRow2[plc27DataSet.PhysicalChannel.IsAnalogColumn] = 1;
        //        anyRow2[plc27DataSet.PhysicalChannel.IsDiscreteColumn] = 0;
        //        anyRow2[plc27DataSet.PhysicalChannel.DescriptionColumn] = "new analog input module " + fieldNodeId;
        //        plc27DataSet.PhysicalChannel.Rows.Add(anyRow2);

        //    //аналоговые выходные
        //        DataRow anyRow3 = plc27DataSet.PhysicalChannel.NewRow();
        //        anyRow3[plc27DataSet.PhysicalChannel.FieldNodeIdColumn] = fieldNodeId;
        //        anyRow3[plc27DataSet.PhysicalChannel.ReadAddressColumn] = 0;
        //        anyRow3[plc27DataSet.PhysicalChannel.WriteAddressColumn] = 0;
        //        anyRow3[plc27DataSet.PhysicalChannel.AddressShiftColumn] = 512;
        //        anyRow3[plc27DataSet.PhysicalChannel.PhysicalChannelSizeColumn] =
        //        modulesInfos.Where(x => x.IoType == IOType.Output && x.SignalType == SignalType.Analog).Sum(x => x.ChannelsCount * 16);
        //        anyRow3[plc27DataSet.PhysicalChannel.IsInputColumn] = 0;
        //        anyRow3[plc27DataSet.PhysicalChannel.IsOutputColumn] = 1;
        //        anyRow3[plc27DataSet.PhysicalChannel.IsAnalogColumn] = 1;
        //        anyRow3[plc27DataSet.PhysicalChannel.IsDiscreteColumn] = 0;
        //        anyRow3[plc27DataSet.PhysicalChannel.DescriptionColumn] = "new analog output module " + fieldNodeId;
        //        plc27DataSet.PhysicalChannel.Rows.Add(anyRow3);

        //    //добавляем только в том случае если конфигурация 
        //    //if(plc27DataSet.PhysicalChannel.FirstOrDefault(x => x.AddressShift == 0 && x.PhysicalChannelSize == 16 && x.IsInput == true && x.IsAnalog == true)==null)
        //    //    plc27DataSet.PhysicalChannel.Rows.Add(anyRow);
        //    //else
        //    //{
        //    //    MessageBox.Show("Таблица физических каналов уже содержит аналогичную запись!");
        //    //}
        //    //return anyRow;

        //}

        public List<int> InsertPhysicalChannels(PhysicalChannelTableAdapter physicalChannelTableAdapter, int fieldNodeId)
        {
            var gids = new List<int>();

            var DI = physicalChannelTableAdapter.Insert(
                fieldNodeId,
                0,
                0,
                0,
                modulesInfos.Where(x => x.IoType == IOType.Input && x.SignalType == SignalType.Descrete)
                            .Sum(x => x.ChannelsCount),
                true,
                false,
                false,
                true,
                "new discrete input module - " + fieldNodeId,
                null
                );
            gids.Add(DI);
            var DO = physicalChannelTableAdapter.Insert(
                fieldNodeId,
                0,
                0,
                512,
                modulesInfos.Where(x => x.IoType == IOType.Output && x.SignalType == SignalType.Descrete).Sum(x => x.ChannelsCount),
                false,
                true,
                false,
                true,
                "new discrete output module - " + fieldNodeId,
                null
                );
            gids.Add(DO);
            var AI = physicalChannelTableAdapter.Insert(
                fieldNodeId,
                0,
                0,
                0,
                modulesInfos.Where(x => x.IoType == IOType.Input && x.SignalType == SignalType.Analog).Sum(x => x.ChannelsCount * 16),
                true,
                false,
                true,
                false,
                "new analog input module - " + fieldNodeId,
                null
                );
            gids.Add(AI);
            var AO = physicalChannelTableAdapter.Insert(
                fieldNodeId,
                0,
                0,
                512,
                modulesInfos.Where(x => x.IoType == IOType.Output && x.SignalType == SignalType.Analog).Sum(x => x.ChannelsCount * 16),
                false,
                true,
                true,
                false,
                "new analog output module - " + fieldNodeId,
                null
                );
            gids.Add(AO);
            return gids;
        }

        public int InsertLogicalChannels(LogicalChannelTableAdapter logicalChannelTableAdapter, List<int> gids, int insertedNodeId)
        {
            var insertedRowCounter = new int();
            //ставим лайк имени переменной ;)
            if (gids.Count < 4)
            {
                throw new ArgumentException("Невохможно добавить логические каналы, т.к. не все физические каналы были созданы!");
            }


            var adios = new[]
                {
                    new
                        {
                            mod = modulesInfos.Where(x => x.IoType == IOType.Input && x.SignalType == SignalType.Descrete).ToList(),
                            chid = gids[0]
                        },

                    new
                        {
                            mod = modulesInfos.Where(x => x.IoType == IOType.Input && x.SignalType == SignalType.Analog).ToList(),
                            chid = gids[2]
                        },
                    new
                        {
                            mod = modulesInfos.Where(x => x.IoType == IOType.Output && x.SignalType == SignalType.Descrete).ToList(),
                            chid = gids[1]
                        },
                    new
                        {
                            mod = modulesInfos.Where(x => x.IoType == IOType.Output && x.SignalType == SignalType.Analog).ToList(),
                            chid = gids[3]
                        },
                }.ToList();
            
            foreach (var adio in adios)/*=4*/
                {
                    for (int i = 0; i < adio.mod.Sum(x=>x.ChannelsCount); i++)/*=8*/
                    {
                        insertedRowCounter += logicalChannelTableAdapter.Insert(
                            //{0} - тип модуля; {1} кол.каналов; {2} no.канала;
                            Convert.ToInt32(String.Format("{0}{1}{2}", adios.IndexOf(adio) + 1, insertedNodeId%10, (i + 1))),
                            null,
                            adio.chid,
                            null,
                            null,
                            (adios.IndexOf(adio) % 2 == 1) ? 16 : 1, //аналоговые 16 битные; дискретные только 1 бит на канал
                            i,
                            500,
                            (adio.mod.Sum(x => x.ChannelsCount)).ToString() + "ch " + adio.mod[0].SignalType + adio.mod[0].IoType,//adio.mod[i].ToString(),
                            null,
                            null,
                            null,
                            null,
                            null

                            );

                    }
                    
                }
            return insertedRowCounter;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
