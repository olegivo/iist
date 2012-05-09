#define HIGH_LEVEL
#define LOW_LEVEL

using System;
using System.ServiceModel;
using System.Windows.Forms;
using Oleg_ivo.MES.High;
using Oleg_ivo.MES.Logging;
using Oleg_ivo.MES.Low;
using Oleg_ivo.Tools;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace Oleg_ivo.MES
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
#pragma warning disable 168
            ExceptionHandler exceptionHandler = new ExceptionHandler();
#pragma warning restore 168
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Logger logger;

#if LOW_LEVEL
            logger = new Logger("Запуск сервиса нижнего уровня");
            ServiceHost serviceHostLowLevel = new ServiceHost(LowLevelMessageExchangeSystem.Instance);
            serviceHostLowLevel.Open();
            logger.End(2);
#endif

#if HIGH_LEVEL
            logger = new Logger("Запуск сервиса верхнего уровня");
            ServiceHost serviceHostHighLevel = new ServiceHost(HighLevelMessageExchangeSystem.Instance);
            serviceHostHighLevel.Open();
            logger.End(2);
#endif

            logger = new Logger("Установка строки соединения с базой данных");
            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();
            logger.End(2);

            logger = new Logger("Запуск протоколирования сообщений");
            InternalMessageLogger.Instance.Start();
            logger.End(2);

            Application.Run(new MesForm());

#if LOW_LEVEL
		    logger = new Logger("Остановка сервиса нижнего уровня");
            serviceHostLowLevel.Close();
            logger.End(2);
#endif

#if HIGH_LEVEL
            logger = new Logger("Остановка сервиса верхнего уровня");
            serviceHostHighLevel.Close();
            logger.End(2);
#endif

            logger = new Logger("Остановка протоколирования сообщений");
            InternalMessageLogger.Instance.Stop(false);
            logger.End(2);
        }
    }
}