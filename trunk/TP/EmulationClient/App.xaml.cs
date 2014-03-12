using System;
using System.Windows;
using Autofac;
using DMS.Common.Messages;
using EmulationClient.Emulation;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;
using Oleg_ivo.WAGO.Autofac;
using Oleg_ivo.WAGO.Configuration;

namespace EmulationClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Test()
        {
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

            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(e.Args));
            builder.RegisterType<ControlManagementUnitEmulation>().SingleInstance();
            builder.RegisterModule<WagoAutofacModule>();

            var container = builder.Build();

            Emulator = container.ResolveUnregistered<Emulator>();
            //ControlManagementUnit = container.ResolveUnregistered<ControlManagementUnitEmulation>();
            ControlManagementUnit.GetRegName = GetRegName;


#pragma warning disable 168
            var exceptionHandler = new ExceptionHandler(LogError);
#pragma warning restore 168

            //container.Resolve<DbConnectionProvider>().SetupConnectionStringFromConfigurationFile();
            //TODO:Bootstrapper instead of StartupUri="Window1.xaml"
            //MyOwnBootStraper bootstrapper = new MyOwnBootStraper();
            //bootstrapper.Run();
            //ControlManagementUnit.Register();

            Test();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            ControlManagementUnit.Dispose();
            base.OnExit(e);
        }

        private void LogError(object sender, ExtendedThreadExceptionEventArgs e)
        {
            ControlManagementUnit.SendErrorCompleted += ControlManagementUnit_SendErrorCompleted;
            try
            {
                //TODO: заполнить RegNameFrom
                ControlManagementUnit.SendErrorAsync(e);
                if (e.Exception is ArgumentOutOfRangeException)
                    e.ShowError = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void ControlManagementUnit_SendErrorCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            /*
             * TODO: если не удалось передать ошибку службе обмена сообщениями, выбрасывать ошибку здесь?
                        ControlManagementUnit.SendErrorCompleted -= ControlManagementUnit_SendErrorCompleted;
                        if(e.Error!=null)
                        {
                            ExtendedThreadExceptionEventArgs args = e.UserState as ExtendedThreadExceptionEventArgs;
                        }
            */
        }

        internal ControlManagementUnitEmulation ControlManagementUnit
        {
            get { return Emulator.ControlManagementUnit; }
        }

        internal Emulator Emulator { get; private set; }
    }
}
