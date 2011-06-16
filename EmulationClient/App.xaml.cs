using System;
using System.Threading;
using System.Windows;
using EmulationClient.Emulation;

namespace EmulationClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            GasConcentration gasConcentration = new GasConcentration();
            Temperature temperature = new Temperature();
            double concentrationValue;
            double temperatureValue;

            temperature.Refresh();
            temperatureValue = temperature.OutputValue;

            gasConcentration.Temperature = temperatureValue;
            gasConcentration.Refresh();
            concentrationValue = gasConcentration.OutputValue;
            Console.WriteLine("outputValue = {0}", concentrationValue);

            Thread.Sleep(2000);
            gasConcentration.Refresh();
            concentrationValue = gasConcentration.OutputValue;
            Console.WriteLine("outputValue = {0}", concentrationValue);

            Thread.Sleep(2000);
            gasConcentration.Refresh();
            concentrationValue = gasConcentration.OutputValue;
            Console.WriteLine("outputValue = {0}", concentrationValue);
        }
    }
}
