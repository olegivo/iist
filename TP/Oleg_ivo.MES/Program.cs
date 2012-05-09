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
            logger = new Logger("������ ������� ������� ������");
            ServiceHost serviceHostLowLevel = new ServiceHost(LowLevelMessageExchangeSystem.Instance);
            serviceHostLowLevel.Open();
            logger.End(2);
#endif

#if HIGH_LEVEL
            logger = new Logger("������ ������� �������� ������");
            ServiceHost serviceHostHighLevel = new ServiceHost(HighLevelMessageExchangeSystem.Instance);
            serviceHostHighLevel.Open();
            logger.End(2);
#endif

            logger = new Logger("��������� ������ ���������� � ����� ������");
            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();
            logger.End(2);

            logger = new Logger("������ ���������������� ���������");
            InternalMessageLogger.Instance.Start();
            logger.End(2);

            Application.Run(new MesForm());

#if LOW_LEVEL
		    logger = new Logger("��������� ������� ������� ������");
            serviceHostLowLevel.Close();
            logger.End(2);
#endif

#if HIGH_LEVEL
            logger = new Logger("��������� ������� �������� ������");
            serviceHostHighLevel.Close();
            logger.End(2);
#endif

            logger = new Logger("��������� ���������������� ���������");
            InternalMessageLogger.Instance.Stop(false);
            logger.End(2);
        }
    }
}