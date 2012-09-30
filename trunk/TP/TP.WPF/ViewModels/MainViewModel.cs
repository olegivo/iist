using System;
using System.Windows.Input;
using DMS.Common.Events;
using JulMar.Windows.Interfaces;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            CloseAppCommand = new DelegatingCommand(OnCloseApp);
            DisplayAboutCommand = new DelegatingCommand(OnShowAbout);

            RegisterCommand = new DelegatingCommand(OnRegister);
            UnregisterCommand = new DelegatingCommand(OnUnregister);

            FinishCleaning = new FinishCleaningViewModel();
            FinishCleaning.SendControlMessage += FinishCleaning_SendControlMessage;

            DrumTypeFurnace = new DrumTypeFurnaceViewModel();
            CyclonAndScrubber = new CycloneAndScrubberViewModel();
            ReheatChamber = new ReheatChamberViewModel();
            AllHeatExchanger = new AllHeatExchangerViewModel();
            SummaryTable = new SummaryTableViewModel();

            channelController1.AutoSubscribeChannels = true;
            //channelController1.CanRegisterChanged += new System.EventHandler(this.channelController1_CanRegisterChanged);
            channelController1.InitProvider("HighLevelClient");
            channelController1.NeedProtocol += channelController1_NeedProtocol;
            channelController1.HasReadChannel += channelController1_HasReadChannel;
            channelController1.CanRegister = true;
        }

        private void OnRegister()
        {
            channelController1.Register();
        }

        private void OnUnregister()
        {
            channelController1.Unregister();
        }

        /// <summary>
        /// This method closes the application window.
        /// </summary>
        private void OnCloseApp()
        {
            if (!channelController1.CanRegister)
                channelController1.Unregister();
            // Ask the view to close.
            RaiseCloseRequest();
        }

        /// <summary>
        /// This method displays the About Box.
        /// </summary>
        private void OnShowAbout()
        {
            // Get the message visualizer service from the service resolver.
            // All services can be replaced, so make sure to check if we have something
            // registered.
            IMessageVisualizer messageVisualizer = Resolve<IMessageVisualizer>();
            if (messageVisualizer != null)
            {
                // Show a message box.
                messageVisualizer.Show("Технологический процесс", "Две вещи действительно бесконечны: вселенная и  человеческая глупость.", MessageButtons.OK);
            }
        }

        public ChannelController channelController1 = new ChannelController();
        private string messages;

        public FinishCleaningViewModel FinishCleaning { get; private set; }
        public DrumTypeFurnaceViewModel DrumTypeFurnace { get; private set; }
        public CycloneAndScrubberViewModel CyclonAndScrubber { get; private set; }
        public AllHeatExchangerViewModel AllHeatExchanger { get; private set; }
        public ReheatChamberViewModel ReheatChamber { get; private set; }
        public SummaryTableViewModel SummaryTable { get; private set; }


        void channelController1_NeedProtocol(object sender, EventArgs e)
        {
            Protocol(sender);
        }

        private void Protocol(object sender)
        {

            if (sender is double || sender is string)
            {
                var s = string.Format("{0}\t{1}{2}", DateTime.Now, sender, Environment.NewLine);
                Messages = (Messages ?? string.Empty) + s;
            }
        }

        public string Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                OnPropertyChanged("Messages");
            }
        }

        /*
        /// <summary>
        /// Запись протокола с обработкой события, произошедшего в другом потоке
        /// </summary>
        /// <param name="info">TextBox</param>
        /// <param name="s">string</param>
        private delegate void StDelegate(TextBox info, string s);
        private void SetText(TextBox info, string s)
        {
            /*TODO: Binding
            if (Dispatcher.Thread != Thread.CurrentThread)
            {
                StDelegate ddd = SetText;
                Dispatcher.Invoke(ddd, new object[] { info, s });
            }
            else
            {
                info.AppendText(s);
            }
            #1#
        }
        */

        void channelController1_HasReadChannel(object sender, DataEventArgs e)
        {
            float value = Convert.ToSingle(e.Message.Value);
            int channelId = e.Message.LogicalChannelId;
            SummaryTable.Add(channelId,value);
            switch (channelId)
            {
                case 1:
                    DrumTypeFurnace.Temperature_TC1 = value;
                    break; //TП1	температура в циклонной вихревой топке
                case 2:
                    DrumTypeFurnace.Temperature_TC2 = value;
                    break; //TП2	температура в загрузочной системе
                case 3:
                    AllHeatExchanger.Temperature_TP3 = value;
                    break; //TП3	температура в камере дожигания
                case 4:
                    AllHeatExchanger.Temperature_TR4 = value;
                    break; //TР4	температура в теплообменнике ТО1
                case 5:
                    AllHeatExchanger.Temperature_TR5 = value;
                    break; //TР5	температура в теплообменнике ТО2
                case 6:
                    FinishCleaning.Temperature_TC6 = value;
                    break; //TС6	температура перед рукавным фильтром
                case 7:
                    FinishCleaning.Temperature_TC7 = value;
                    break; //TС7	температура перед дымососом
                case 8:
                    DrumTypeFurnace.Temperature_TC8 = value;
                    break; //TС8	температура воды в системе охлаждения
                //BUG: канал не реализован
                //    case 9:
                //    break; //Р	разрежение в камере дожигания
                case 10:
                    CyclonAndScrubber.PhLevel_CF1 = value;
                    break; //рН1	уровень рН в СФ1
                case 11:
                    CyclonAndScrubber.PhLevel_CF2 = value;
                    break; //рН2	уровень рН в СФ2
                case 12:
                    DrumTypeFurnace.Speed_S = value;
                    break; //S	скорость вращения печи
                case 13:
                    DrumTypeFurnace.Level_DU9 = value;
                    //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-9	уровень отходов в бункере
                case 14:
                    //BUG: В 14й канале должна быть ЛИБО температура, ЛИБО уровень! (проверить)
                    //-??-ReheatChamber.Temperature = value * 100;
                    //this.ReheatChamber.Temperature=value*100;
                    ReheatChamber.Level_DU11 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-11	уровень в РТ
                case 15:
                    ReheatChamber.Level_DU1 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-1	уровень в НЕ
                case 16:
                    ReheatChamber.Level_DU4 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-4	уровень в РЕ
                case 17:
                    CyclonAndScrubber.Level_DU10 = value;
                    //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-10	уровень в СБ
                case 18:
                    AllHeatExchanger.GasConcentration_O2 = value;
                    break; //Г-О2	концентрация газа О2
                case 19:
                    AllHeatExchanger.GasConcentration_CO = value;
                    break; //Г-СО	концентрация газа СО
                case 20:
                    FinishCleaning.GasConcentration_O2 = value;
                    break; //Г-О2	концентрация газа О2
                case 21:
                    FinishCleaning.GasConcentration_CO = value;
                    break; //Г-СО	концентрация газа СО
                case 22:
                    FinishCleaning.GasConcentration_SO2 = value;
                    break; //Г-SО2	концентрация газа SО2
                case 23:
                    FinishCleaning.GasConcentration_NO = value;
                    break; //Г-NО	концентрация газа NО
                case 24:
                    FinishCleaning.GasConcentration_NO2 = value;
                    break; //Г-NО2	концентрация газа NО2
            }
        }

        private void FinishCleaning_SendControlMessage(object sender, SendControlMessageEventArgs e)
        {
            channelController1.WriteChannel(e.ChannelId, e.Value);
        }

        /// <summary>
        /// Command to end the application
        /// </summary>
        public ICommand CloseAppCommand { get; private set; }

        /// <summary>
        /// Command to display the About Box.
        /// </summary>
        public ICommand DisplayAboutCommand { get; private set; }

        public ICommand RegisterCommand { get; private set; }
        public ICommand UnregisterCommand { get; private set; }
    }
}