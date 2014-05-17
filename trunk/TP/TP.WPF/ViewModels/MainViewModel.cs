using System;
using System.Diagnostics;
using System.Windows.Input;
using DMS.Common.Messages;
using GalaSoft.MvvmLight.Command;
using TP.WPF.Views;


namespace TP.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public FinishCleaningViewModel FinishCleaning { get; private set; }
        public DrumTypeFurnaceViewModel DrumTypeFurnace { get; private set; }
        public CycloneAndScrubberViewModel CycloneAndScrubber { get; private set; }
        public AllHeatExchangerViewModel HeatExchanger { get; private set; }
        public ReheatChamberViewModel ReheatChamber { get; private set; }
        public SummaryTableViewModel SummaryTable { get; private set; }
        public ChartTabViewModel ChartTab { get; private set; }

        public MainViewModel()
        {
            TestCommand = new RelayCommand(OnTest);
            CloseAppCommand = new RelayCommand(OnCloseApp);
            DisplayAboutCommand = new RelayCommand(OnShowAbout);

            GoForUpdatesCommand = new RelayCommand(OnGetUpdates);

            RegisterCommand = new RelayCommand(OnRegister);
            UnregisterCommand = new RelayCommand(OnUnregister);

            FinishCleaning = new FinishCleaningViewModel();
            FinishCleaning.SendControlMessage += FinishCleaning_SendControlMessage;

            DrumTypeFurnace = new DrumTypeFurnaceViewModel();
            CycloneAndScrubber = new CycloneAndScrubberViewModel();
            ReheatChamber = new ReheatChamberViewModel();
            HeatExchanger = new AllHeatExchangerViewModel();
            SummaryTable = new SummaryTableViewModel();
            ChartTab = new ChartTabViewModel();

            channelController1.AutoSubscribeChannels = true;
            channelController1.InitProvider();
            channelController1.GetRegName = GetRegName;
            channelController1.NeedProtocol += channelController1_NeedProtocol;
            channelController1.CanRegister = true;

            SubscribeAndInitViewModels();
        }

        private string GetRegName()
        {
            return "HighLevelClient";//TODO: 2 viewmodel & view
        }

        private void OnGetUpdates()
        {
            Process.Start("https://code.google.com/p/iist/source/checkout");
        }

        private void OnTest()
        {
            const int channelId = 6;
            OnChannelRegistered(new ChannelRegistrationMessage("", "", RegistrationMode.Register, DataMode.Read, channelId)
            {
                MinValue = 0,
                MinNormalValue = 20,
                MaxNormalValue = 40,
                MaxValue = 50,
                Description = "Т6"
            });

            var indicatorViewModel = IndicatorViewModels[channelId];
            if (!indicatorViewModel.CurrentValue.HasValue) indicatorViewModel.CurrentValue = 0;
            indicatorViewModel.CurrentValue += (indicatorViewModel.CurrentValue > 50 ? -70 : 5);
        }

        /// <summary>
        /// Подписка моделей представлений на события, связанные с каналами
        /// </summary>
        private void SubscribeAndInitViewModels()
        {
            var viewModels = new ViewModelBase[]
                {
                    this,
                    FinishCleaning,
                    DrumTypeFurnace,
                    CycloneAndScrubber,
                    ReheatChamber,
                    HeatExchanger,
                    SummaryTable,
                    ChartTab
                };

            var models = new ObservableDictionary<int, IndicatorViewModel>();
            foreach (var viewModelBase in viewModels)
            {
                var viewModel = viewModelBase;
                channelController1.HasReadChannel += (sender, e) => viewModel.OnReadChannel(e.Message);
                channelController1.ChannelRegistered += (sender, e) => viewModel.OnChannelRegistered(e.Message);
                channelController1.ChannelUnRegistered += (sender, e) => viewModel.OnChannelUnRegistered(e.Message);
                channelController1.ChannelSubscribed += (sender, e) => viewModel.OnChannelIsActiveChanged(Convert.ToInt32(e.UserState), true);
                channelController1.ChannelUnSubscribed += (sender, e) => viewModel.OnChannelIsActiveChanged(Convert.ToInt32(e.UserState), false);
                channelController1.UnregisterCompleted += (sender, e) => viewModel.OnUnregistered();
                viewModel.IndicatorViewModels = models;
            }
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
            {
                channelController1.Unregister();
                channelController1.Dispose();
            }
            // Ask the view to close.
            //TODO:RaiseCloseRequest();
        }

        /// <summary>
        /// This method displays the About Box.
        /// </summary>
        private void OnShowAbout()
        {
            // Get the message visualizer service from the service resolver.
            // All services can be replaced, so make sure to check if we have something
            // registered.
            /*IMessageVisualizer messageVisualizer = Resolve<IMessageVisualizer>();
            if (messageVisualizer != null)
            {
                // Show a message box.
                messageVisualizer.Show("Технологический процесс", "Две вещи действительно бесконечны: вселенная и  человеческая глупость.", MessageButtons.OK);
            }*/
            new TPAboutBox().ShowDialog();

        }

        public ChannelController channelController1 = new ChannelController();
        private string messages;


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
                RaisePropertyChanged("Messages");
            }
        }

        /// <summary>
        /// После чтения канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnReadChannel(InternalLogicalChannelDataMessage message)
        {
            base.OnReadChannel(message);
            //TODO:перенести в другие реализации BaseViewModel всё, что ниже:
            var channelId = message.LogicalChannelId;

            switch (channelId)
            {
                //BUG: канал не реализован
                //    case 9:
                //    break; //Р	разрежение в камере дожигания
                case 14:
                    //BUG: В 14й канале должна быть ЛИБО температура, ЛИБО уровень! (проверить)
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-11	уровень в РТ
                case 15:
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-1	уровень в НЕ
                case 16:
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-4	уровень в РЕ
                case 17:
                    //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-10	уровень в СБ
            }
        }

        /// <summary>
        /// После регистрации канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnChannelRegistered(ChannelRegistrationMessage message)
        {
            base.OnChannelRegistered(message);
            IndicatorViewModel indicatorViewModel;
            var channelId = message.LogicalChannelId;
            if (IndicatorViewModels.ContainsKey(channelId))
            {
                indicatorViewModel = IndicatorViewModels[channelId];
            }
            else
            {
                indicatorViewModel = new IndicatorViewModel();
                IndicatorViewModels.Add(channelId, indicatorViewModel);
            }
            indicatorViewModel.Init(message);
        }

        //TODO:изменить механизм из событий во что-нибудь другое
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
        public ICommand TestCommand { get; private set; }
        public ICommand GoForUpdatesCommand { get; private set; }

        
    }
}