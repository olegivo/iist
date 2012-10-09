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

            SubscribeViewModels();
        }

        /// <summary>
        /// Подписка моделей представлений на события, связанные с каналами
        /// </summary>
        private void SubscribeViewModels()
        {
            var viewModels = new ViewModelBase[]
                {
                    FinishCleaning,
                    DrumTypeFurnace,
                    CyclonAndScrubber,
                    ReheatChamber,
                    AllHeatExchanger,
                    SummaryTable
                };
            foreach (var viewModelBase in viewModels)
            {
                var viewModel = viewModelBase;
                channelController1.HasReadChannel += (sender, e) => viewModel.OnReadChannel(e.Message);
                channelController1.ChannelRegistered += (sender, e) => viewModel.OnChannelRegistered(e.Message);
                channelController1.ChannelUnRegistered += (sender, e) => viewModel.OnChannelUnRegistered(e.Message);
                channelController1.ChannelSubscribed += (sender, e) => viewModel.OnChannelIsActiveChanged(Convert.ToInt32(e.UserState), true);
                channelController1.ChannelUnSubscribed += (sender, e) => viewModel.OnChannelIsActiveChanged(Convert.ToInt32(e.UserState), false);
                channelController1.UnregisterCompleted += (sender, e) => viewModel.OnUnregistered();
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

        void channelController1_HasReadChannel(object sender, DataEventArgs e)
        {
            //TODO:перенести в реализации BaseViewModel всё, что ниже:
            var channelId = e.Message.LogicalChannelId;

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