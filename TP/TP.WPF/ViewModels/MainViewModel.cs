using System;
using System.Diagnostics;
using System.Windows.Input;
using DMS.Common;
using DMS.Common.Messages;
using GalaSoft.MvvmLight.Command;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.HighLevelClient;
using Oleg_ivo.Tools.ExceptionCatcher;
using TP.WPF.IoC;
using TP.WPF.Properties;
using TP.WPF.Views;
using UICommon.WPF.LogBinding;


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

        public MainViewModel(ExceptionHandler exceptionHandler, ErrorSenderWrapper<ClientProvider> errorSenderWrapper)
        {
            Enforce.ArgumentNotNull(exceptionHandler, "exceptionHandler").AdditionalErrorHandler =
                errorSenderWrapper.LogError;
            LogTarget = LogManager.Configuration.FindTargetByName("uiLog") as ObservableLogTarget;

            channelController.AutoSubscribeChannels = true;
            channelController.LogicalChannelMappings = Settings.Default.LogicalChannelMappings;
            channelController.GetRegName = GetRegName;
            channelController.InitProvider();
            channelController.CanRegister = true;

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

            SubscribeAndInitViewModels();
        }

        public ObservableLogTarget LogTarget { get; private set; }

        [Dependency(Required = true)]
        public TpCommandLineOptions CommandLineOptions { get; set; }

        public void OnLoad()
        {
            if(CommandLineOptions.AutoRegister)
                channelController.Register();
        }

        private string GetRegName()
        {
            return Settings.Default.DefaultRegName;//TODO: 2 view?
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
            if (!indicatorViewModel.CurrentValueDouble.HasValue) indicatorViewModel.CurrentValue = 0;
            indicatorViewModel.CurrentValueDouble += (indicatorViewModel.CurrentValueDouble > 50 ? -70 : 5);
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
                channelController.HasReadChannel += (sender, e) => viewModel.OnReadChannel(ConvertMessageToLocalChannel(e.Message));
                channelController.ChannelStateChanged += (sender, e) => viewModel.OnChannelStateChanged(ConvertMessageToLocalChannel(e.Message));
                channelController.ChannelRegistered += (sender, e) => viewModel.OnChannelRegistered(ConvertMessageToLocalChannel(e.Message));
                channelController.ChannelUnRegistered += (sender, e) => viewModel.OnChannelUnRegistered(ConvertMessageToLocalChannel(e.Message));
                channelController.ChannelSubscribed += (sender, e) => viewModel.OnChannelIsActiveChanged(channelController.GetLocalChannelId(Convert.ToInt32(e.UserState)), true);
                channelController.ChannelUnSubscribed += (sender, e) => viewModel.OnChannelIsActiveChanged(channelController.GetLocalChannelId(Convert.ToInt32(e.UserState)), false);
                channelController.UnregisterCompleted += (sender, e) => viewModel.OnUnregistered();
                viewModel.IndicatorViewModels = models;
            }
        }

        private ChannelRegistrationMessage ConvertMessageToLocalChannel(ChannelRegistrationMessage originalMessage)
        {
            //TODO:если кроме LogicalChannelId и Value ничего не используется, вместо генерации нового сообщения можно передавать только эти 2 параметра
            var message = (ChannelRegistrationMessage) originalMessage.Clone();
            message.LogicalChannelId = channelController.GetLocalChannelId(originalMessage.LogicalChannelId);
            return message;
        }

        /// <summary>
        /// Создать сообщение, содержащее локальный Id канала на основе реального LogicalChannelId
        /// </summary>
        /// <param name="originalMessage"></param>
        /// <returns></returns>
        private InternalLogicalChannelDataMessage ConvertMessageToLocalChannel(
            InternalLogicalChannelDataMessage originalMessage)
        {
            //TODO:если кроме LogicalChannelId и Value ничего не используется, вместо генерации нового сообщения можно передавать только эти 2 параметра
            var message = (InternalLogicalChannelDataMessage) originalMessage.Clone();
            message.LogicalChannelId = channelController.GetLocalChannelId(originalMessage.LogicalChannelId);
            return message;
        }

        /// <summary>
        /// Создать сообщение, содержащее локальный Id канала на основе реального LogicalChannelId
        /// </summary>
        /// <param name="originalMessage"></param>
        /// <returns></returns>
        private InternalLogicalChannelStateMessage ConvertMessageToLocalChannel(
            InternalLogicalChannelStateMessage originalMessage)
        {
            //TODO:если кроме LogicalChannelId и Value ничего не используется, вместо генерации нового сообщения можно передавать только эти 2 параметра
            var message = (InternalLogicalChannelStateMessage) originalMessage.Clone();
            message.LogicalChannelId = channelController.GetLocalChannelId(originalMessage.LogicalChannelId);
            return message;
        }

        private void OnRegister()
        {
            channelController.Register();
        }

        private void OnUnregister()
        {
            channelController.Unregister();
        }

        /// <summary>
        /// This method closes the application window.
        /// </summary>
        private void OnCloseApp()
        {
            if (!channelController.CanRegister)
            {
                channelController.Unregister();
                channelController.Dispose();
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

        private readonly ChannelController channelController = new ChannelController();


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
            var localChannelId = channelController.GetLocalChannelId(message.LogicalChannelId);
            if (IndicatorViewModels.ContainsKey(localChannelId))
            {
                indicatorViewModel = IndicatorViewModels[localChannelId];
            }
            else
            {
                indicatorViewModel = new IndicatorViewModel();
                IndicatorViewModels.Add(localChannelId, indicatorViewModel);
            }
            indicatorViewModel.Init(message);
        }

        //TODO:изменить механизм из событий во что-нибудь другое
        private void FinishCleaning_SendControlMessage(object sender, SendControlMessageEventArgs e)
        {
            channelController.WriteChannel(e.ChannelId, e.Value);
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