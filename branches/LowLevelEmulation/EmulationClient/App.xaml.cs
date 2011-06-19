using System;
using System.Threading;
using System.Windows;
using DMS.Common.Messages;
using EmulationClient.Emulation;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace EmulationClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ControlManagementUnitEmulation _controlManagementUnit;

        public App()
        {
            Init();

            GasConcentration gasConcentration = new GasConcentration();
            Temperature temperature = new Temperature();
            double concentrationValue;
            double temperatureValue;

            temperature.Refresh();
            temperatureValue = temperature.GetOutputValue();

            gasConcentration.Temperature = temperatureValue;
            gasConcentration.Refresh();
            concentrationValue = gasConcentration.GetOutputValue();
            Console.WriteLine("outputValue = {0}", concentrationValue);

            Thread.Sleep(2000);
            gasConcentration.Refresh();
            concentrationValue = gasConcentration.GetOutputValue();
            Console.WriteLine("outputValue = {0}", concentrationValue);

            Thread.Sleep(2000);
            gasConcentration.Refresh();
            concentrationValue = gasConcentration.GetOutputValue();
            Console.WriteLine("outputValue = {0}", concentrationValue);
        }

        private void Init()
        {
#pragma warning disable 168
            ExceptionHandler exceptionHandler = new ExceptionHandler(LogError);
#pragma warning restore 168

            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();
            ControlManagementUnit.Register();
            
        }

        private static void LogError(object sender, ExtendedThreadExceptionEventArgs e)
        {
            ControlManagementUnitEmulation.Proxy.SendErrorCompleted += Proxy_SendErrorCompleted;
            try
            {
                ControlManagementUnitEmulation.Proxy.SendErrorAsync(new InternalErrorMessage(e.Exception), e);
                if (e.Exception is ArgumentOutOfRangeException)
                    e.ShowError = false;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private ControlManagementUnitEmulation ControlManagementUnit
        {
            get
            {
                if (_controlManagementUnit == null)
                {
                    _controlManagementUnit = new ControlManagementUnitEmulation();
                    _controlManagementUnit.GetRegName = new GetRegNameDelegate(() => "EmulationClient");
                    _controlManagementUnit.InitChannels();
                    //_ControlManagementUnit.GetDistributedMeasurementInformationSystem =
                    //    GetDistributedMeasurementInformationSystem;
                    //_ControlManagementUnit.BuildSystemConfiguration();
                }
                return _controlManagementUnit;
            }
        }

    }
}
