using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using DMS.Common;
using DMS.Common.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.CMU.WPF.Properties;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Tools.ExceptionCatcher;
using Oleg_ivo.WAGO.Configuration;
using UICommon.WPF.LogBinding;

namespace Oleg_ivo.CMU.WPF.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ControlManagementUnit controlManagementUnit, ExceptionHandler exceptionHandler, ErrorSenderWrapper<ControlManagementUnit> errorSenderWrapper)
        {
            ControlManagementUnit = Enforce.ArgumentNotNull(controlManagementUnit, "controlManagementUnit");
            ControlManagementUnit.GetRegName = () => RegName;
            ControlManagementUnit.RegisterCompleted += ControlManagementUnit_RegisterCompleted;
            ControlManagementUnit.UnregisterCompleted += ControlManagementUnit_UnregisterCompleted;

            Enforce.ArgumentNotNull(exceptionHandler, "exceptionHandler").AdditionalErrorHandler =
                errorSenderWrapper.LogError;

            dispatcher = Dispatcher.CurrentDispatcher;

            LogTarget = LogManager.Configuration.FindTargetByName("uiLog") as ObservableLogTarget;
            UnregisteredChannels = new ObservableCollection<LogicalChannelViewModel>();
            RegisteredChannels = new ObservableCollection<LogicalChannelViewModel>();
            SelectedUnregisteredChannels = new ObservableCollection<LogicalChannelViewModel>();
            SelectedRegisteredChannels = new ObservableCollection<LogicalChannelViewModel>();
            initTask = Task.Factory.StartNew(() =>
            {
                ControlManagementUnit.BuildSystemConfiguration();
                CanRegister = true;
            });
        }

        #region EventHandlers
        void ControlManagementUnit_RegisterCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Registering = false;
            CanRegister = e.Error != null;

            if (CanRegisterChannels && AutoRegisterAllChannels)
                RegisterAllChannels();
        }

        void ControlManagementUnit_UnregisterCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Registering = false;
            CanRegister = e.Error == null;
        }

        #endregion

        #region Fields
        private ICommand commandRegister;
        private ICommand commandUnregister;
        private ICommand commandRegisterChannel;
        private ICommand commandUnregisterChannel;
        private ICommand commandRegisterSelectedChannels;
        private ICommand commandUnregisterSelectedChannels;
        private ICommand commandRegisterAllChannels;
        private ICommand commandUnregisterAllChannels;
        private bool isRegistered;
        private bool canRegister;
        private bool registering;
        private ObservableCollection<LogicalChannelViewModel> registeredChannels;
        private ObservableCollection<LogicalChannelViewModel> unregisteredChannels;
        private ObservableCollection<LogicalChannelViewModel> selectedUnregisteredChannels;
        private ObservableCollection<LogicalChannelViewModel> selectedRegisteredChannels;

        #endregion

        #region Properties
        [Dependency(Required = true)]
        public IDistributedMeasurementInformationSystem DMIS { get; set; }

        [Dependency(Required = true)]
        public WagoCommandLineOptions CommandLineOptions { get; set; }

        //[Dependency(Required = true)]
        public ControlManagementUnit ControlManagementUnit { get; set; }

        public ObservableLogTarget LogTarget { get; private set; }

        private string regName;
        private readonly Task initTask;
        private Dispatcher dispatcher;

        public string RegName
        {
            get { return regName; }
            set
            {
                if (regName == value) return;
                regName = value;
                RaisePropertyChanged(() => RegName);
            }
        }

        public bool AutoRegister
        {
            get { return CommandLineOptions.AutoRegister; }
        }

        public bool AutoRegisterAllChannels
        {
            get { return CommandLineOptions.AutoRegisterAllChannels; }
        }

        public bool IsRegistered
        {
            get { return isRegistered; }
            set
            {
                isRegistered = value;
                RaisePropertyChanged(() => IsRegistered);
            }
        }

        public bool CanRegister
        {
            get { return !Registering && canRegister; }
            set
            {
                if (canRegister == value) return;
                canRegister = value;
                RaisePropertyChanged(() => CanRegister);
                RaisePropertyChanged(() => CanUnRegister);
            }
        }

        public bool CanUnRegister
        {
            get { return !Registering && !CanRegister; }
        }

        public bool CanRegisterSelectedChannels
        {
            get { return CanUnRegister && SelectedUnregisteredChannels.Any(); }
        }

        public bool CanUnregisterSelectedChannels
        {
            get { return CanUnRegister && SelectedRegisteredChannels.Any(); }
        }

        public bool CanRegisterChannels
        {
            get { return CanUnRegister && UnregisteredChannels.Any(); }
        }

        public bool CanUnregisterChannels
        {
            get { return CanUnRegister && RegisteredChannels.Any(); }
        }

        public bool Registering
        {
            get { return registering; }
            set
            {
                if (registering == value) return;
                registering = value;
                RaisePropertyChanged(() => Registering);
                RaisePropertyChanged(() => CanRegister);
                RaisePropertyChanged(() => CanUnRegister);
            }
        }

        public ObservableCollection<LogicalChannelViewModel> UnregisteredChannels
        {
            get { return unregisteredChannels; }
            set
            {
                if (unregisteredChannels == value) return;
                unregisteredChannels = value;
                RaisePropertyChanged(() => UnregisteredChannels);
            }
        }

        public ObservableCollection<LogicalChannelViewModel> RegisteredChannels
        {
            get { return registeredChannels; }
            set
            {
                if (registeredChannels == value) return;
                registeredChannels = value;
                RaisePropertyChanged(() => RegisteredChannels);
            }
        }

        public ObservableCollection<LogicalChannelViewModel> SelectedUnregisteredChannels
        {
            get { return selectedUnregisteredChannels; }
            set
            {
                if (selectedUnregisteredChannels == value) return;
                selectedUnregisteredChannels = value;
                RaisePropertyChanged(() => SelectedUnregisteredChannels);
            }
        }

        public ObservableCollection<LogicalChannelViewModel> SelectedRegisteredChannels
        {
            get { return selectedRegisteredChannels; }
            set
            {
                if (selectedRegisteredChannels == value) return;
                selectedRegisteredChannels = value;
                RaisePropertyChanged(() => SelectedRegisteredChannels);
            }
        }

        #endregion

        #region Commands
        public ICommand CommandRegister
        {
            get
            {
                return commandRegister ??
                       (commandRegister = new RelayCommand(Register, () => CanRegister));
            }
        }

        public ICommand CommandUnregister
        {
            get
            {
                return commandUnregister ??
                       (commandUnregister = new RelayCommand(Unregister, () => CanUnRegister));
            }
        }

        public ICommand CommandRegisterChannel
        {
            get
            {
                return commandRegisterChannel ??
                       (commandRegisterChannel = new RelayCommand<LogicalChannelViewModel>(RegisterChannel));
            }
        }

        public ICommand CommandUnregisterChannel
        {
            get
            {
                return commandUnregisterChannel ??
                       (commandUnregisterChannel = new RelayCommand<LogicalChannelViewModel>(UnregisterChannel));
            }
        }

        public ICommand CommandRegisterSelectedChannels
        {
            get
            {
                return commandRegisterSelectedChannels ??
                       (commandRegisterSelectedChannels =
                           new RelayCommand(RegisterSelectedChannels, () => CanRegisterSelectedChannels));
            }
        }

        public ICommand CommandUnregisterSelectedChannels
        {
            get
            {
                return commandUnregisterSelectedChannels ??
                       (commandUnregisterSelectedChannels =
                           new RelayCommand(UnregisterSelectedChannels, () => CanUnregisterSelectedChannels));
            }
        }

        public ICommand CommandRegisterAllChannels
        {
            get
            {
                return commandRegisterAllChannels ??
                       (commandRegisterAllChannels = new RelayCommand(RegisterAllChannels, () => CanRegisterChannels));
            }
        }

        public ICommand CommandUnregisterAllChannels
        {
            get
            {
                return commandUnregisterAllChannels ??
                       (commandUnregisterAllChannels =
                           new RelayCommand(UnregisterAllChannels, () => CanUnregisterChannels));
            }
        }

        #endregion

        #region Methods
        private void Register()
        {
            ControlManagementUnit.RegisterAsync();
            Registering = true;
        }

        private void Unregister()
        {
            UnregisterAllChannels();

            ControlManagementUnit.Unregister();
            Registering = true;
        }

        private void RegisterChannel(LogicalChannelViewModel logicalChannelViewModel)
        {
            MoveChannel(logicalChannelViewModel, RegistrationMode.Register);
        }

        private void UnregisterChannel(LogicalChannelViewModel logicalChannelViewModel)
        {
            MoveChannel(logicalChannelViewModel, RegistrationMode.Unregister);
        }

        private void MoveChannel(LogicalChannelViewModel logicalChannelViewModel, RegistrationMode registrationMode)
        {
            var registrationMessage = logicalChannelViewModel.GetRegistrationMessage(RegName, registrationMode);
            //регистрация каналов в MES, если это не канал состояния
            var channel = logicalChannelViewModel.LogicalChannel;
            if (!channel.IsStateChannel)
            {
                LogicalChannel stateChannel = null;
                if (channel.Entity.StateLogicalChannelId.HasValue)
                {
                    stateChannel = ControlManagementUnit.GetAvailableLogicalChannels(true)
                        .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(channel.Entity.StateLogicalChannelId.Value));
                }

                switch (registrationMessage.RegistrationMode)
                {
                    case RegistrationMode.Register:
                        ControlManagementUnit.RegisterChannel(registrationMessage);
                        UnregisteredChannels.Remove(logicalChannelViewModel);
                        RegisteredChannels.Add(logicalChannelViewModel);
                        logicalChannelViewModel.IsRegistered = true;
                        if (TryAddPoll(channel) && stateChannel != null)
                            TryAddPoll(stateChannel);
                        break;
                    case RegistrationMode.Unregister:
                        ControlManagementUnit.UnregisterChannel(registrationMessage);
                        RegisteredChannels.Remove(logicalChannelViewModel);
                        UnregisteredChannels.Add(logicalChannelViewModel);
                        logicalChannelViewModel.IsRegistered = false;
                        if (TryRemovePoll(channel) && stateChannel != null)
                            TryRemovePoll(stateChannel);
                        break;
                }
            }
        }

        private void RegisterSelectedChannels()
        {
            foreach (var channel in SelectedUnregisteredChannels.ToList())
                RegisterChannel(channel);
        }

        private void UnregisterSelectedChannels()
        {
            foreach (var channel in SelectedRegisteredChannels.ToList())
                UnregisterChannel(channel);
        }

        private void RegisterAllChannels()
        {
            foreach (var channel in UnregisteredChannels.ToList())
                RegisterChannel(channel);
        }

        private void UnregisterAllChannels()
        {
            foreach (var channel in RegisteredChannels.ToList())
                UnregisterChannel(channel);
        }

        private bool TryAddPoll(LogicalChannel logicalChannel)
        {
            var success = ControlManagementUnit.TryAddPoll(logicalChannel);
            Log.Info(string.Format("Добавление расписания для [{0}]:\n{1}удачно", logicalChannel, success ? "" : "не"));
            return success;
        }

        private bool TryRemovePoll(LogicalChannel logicalChannel)
        {
            var success = ControlManagementUnit.TryRemovePoll(logicalChannel);
            Log.Info(string.Format("Удаление расписания для [{0}]:\n{1}удачно", logicalChannel, success ? "" : "не"));
            return success;
        }

        #endregion

        public void OnLoad()
        {
            initTask.ContinueWith(task =>
            {
                dispatcher.Invoke(new Action(() =>
                {
                    UnregisteredChannels =
                        new ObservableCollection<LogicalChannelViewModel>(
                            ControlManagementUnit.GetAvailableLogicalChannels()
                                .Select(channel => new LogicalChannelViewModel(channel)));
                    RegName = Settings.Default.DefaultRegName;
                    if (AutoRegister)
                        Register();
                }));
            });
        }
    }
}