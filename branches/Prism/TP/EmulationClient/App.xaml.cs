using System;
using System.Windows;
using Autofac;
using DMS.Common.Messages;
using EmulationClient.Emulation;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;
using Oleg_ivo.WAGO.Autofac;

namespace EmulationClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(args));
            builder.RegisterModule<WagoAutofacModule>();

            var container = builder.Build();

            ControlManagementUnit = container.ResolveUnregistered<ControlManagementUnitEmulation>();
            ControlManagementUnit.GetRegName = GetRegName;
            Emulator = container.ResolveUnregistered<Emulator>();
            Emulator.ControlManagementUnit = ControlManagementUnit;

            //GasConcentration gasConcentration = new GasConcentration();
            //Temperature temperature = new Temperature();
            //double concentrationValue;
            //double temperatureValue;

            //temperature.Refresh();
            //temperatureValue = temperature.GetOutputValue();

            //gasConcentration.Temperature = temperatureValue;
            //gasConcentration.Refresh();
            //concentrationValue = gasConcentration.GetOutputValue();
            //Log.Debug("outputValue = {0}", concentrationValue);

            //Thread.Sleep(2000);
            //gasConcentration.Refresh();
            //concentrationValue = gasConcentration.GetOutputValue();
            //Log.Debug("outputValue = {0}", concentrationValue);

            //Thread.Sleep(2000);
            //gasConcentration.Refresh();
            //concentrationValue = gasConcentration.GetOutputValue();
            //Log.Debug("outputValue = {0}", concentrationValue);
        }

        private string GetRegName()
        {
            return "EmulationClient";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Init();
            //TODO:Bootstrapper
            //MyOwnBootStraper bootstrapper = new MyOwnBootStraper();
            //bootstrapper.Run();
            //ControlManagementUnit.Register();
        }

        /*
                /// <summary>
                /// 
                /// </summary>
                /// <param name="e"></param>
                protected override void OnExit(ExitEventArgs e)
                {
                    //ControlManagementUnit.Unregister();
                    base.OnExit(e);
                }
        */

        private void Init()
        {
#pragma warning disable 168
            ExceptionHandler exceptionHandler = new ExceptionHandler(LogError);
#pragma warning restore 168

            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();

        }

        private void LogError(object sender, ExtendedThreadExceptionEventArgs e)
        {
            ControlManagementUnit.Proxy.SendErrorCompleted += Proxy_SendErrorCompleted;
            try
            {
                //TODO: заполнить RegNameFrom
                ControlManagementUnit.Proxy.SendErrorAsync(new InternalErrorMessage(null, null, e.Exception), e);
                if (e.Exception is ArgumentOutOfRangeException)
                    e.ShowError = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void Proxy_SendErrorCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            /*
             * TODO: если не удалось передать ошибку службе обмена сообщениями, выбрасывать ошибку здесь?
                        Proxy.SendErrorCompleted -= Proxy_SendErrorCompleted;
                        if(e.Error!=null)
                        {
                            ExtendedThreadExceptionEventArgs args = e.UserState as ExtendedThreadExceptionEventArgs;
                        }
            */
        }

        internal ControlManagementUnitEmulation ControlManagementUnit { get; private set; }

        internal Emulator Emulator { get; private set; }
    }
}
