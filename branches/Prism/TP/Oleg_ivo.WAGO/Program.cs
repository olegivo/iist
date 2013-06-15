using System;
using System.Windows.Forms;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;
using Oleg_ivo.WAGO.Configuration;

namespace Oleg_ivo.WAGO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Включить обработку исключений
#pragma warning disable 168
            ExceptionHandler exceptionHandler = new ExceptionHandler();
#pragma warning restore 168
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TestSettings();
            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();
            
            //DbConnectionProvider.Instance.DefaultConnectionString =
            //    @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\WORK\Oleg_ivo\Oleg_ivo.WAGO\Oleg_ivo.WAGO\test.mdb;Persist Security Info=True";

            Form form;
            //form = new Ports.frmFieldBus();
            form = new Forms.MDIParentMain();
            Application.Run(form);
        }

        private static void TestSettings()
        {
            ConfigurationManager.Instance.LoadConfig("");

        }
    }
}