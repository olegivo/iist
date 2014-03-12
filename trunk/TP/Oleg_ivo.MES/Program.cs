#define HIGH_LEVEL
#define LOW_LEVEL

using System;
using System.ServiceModel;
using System.Windows.Forms;
using Autofac;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.Base.Communication;
using Oleg_ivo.MES.High;
using Oleg_ivo.MES.Logging;
using Oleg_ivo.MES.Low;
using Oleg_ivo.Tools.ExceptionCatcher;
using Logger = Oleg_ivo.Tools.Logger;

namespace Oleg_ivo.MES
{
    static class Program
    {
        private static readonly NLog.Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Info("Регистрация компонентов");
            var builder = new ContainerBuilder();
            //builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(args));
            builder.RegisterModule<BaseAutofacModule>();
            //builder.RegisterModule<WagoAutofacModule>();
            builder.RegisterType<InternalMessageLogger>().SingleInstance();
            builder.RegisterType<LowLevelMessageExchangeSystem>().SingleInstance();
            builder.RegisterType<HighLevelMessageExchangeSystem>().SingleInstance();
            var container = builder.Build();

            Logger logger;

            var lowLevelMessageExchangeSystem = container.Resolve<LowLevelMessageExchangeSystem>();
            var highLevelMessageExchangeSystem = container.Resolve<HighLevelMessageExchangeSystem>();
            //взаимная подписка событий:
            lowLevelMessageExchangeSystem.NotifySubscribeEvents(highLevelMessageExchangeSystem);
            highLevelMessageExchangeSystem.NotifySubscribeEvents(lowLevelMessageExchangeSystem);

#if LOW_LEVEL
            logger = new Logger("Запуск сервиса нижнего уровня");
            ServiceHost serviceHostLowLevel = new ServiceHost(lowLevelMessageExchangeSystem);
            serviceHostLowLevel.Open();
            logger.End(2);
#endif

#if HIGH_LEVEL
            logger = new Logger("Запуск сервиса верхнего уровня");
            ServiceHost serviceHostHighLevel = new ServiceHost(highLevelMessageExchangeSystem);
            serviceHostHighLevel.Open();
            logger.End(2);
#endif

            /*logger = new Logger("Установка строки соединения с базой данных");
            DbConnectionProvider.Instance.SetupConnectionStringFromConfigurationFile();
            logger.End(2);*/

            logger = new Logger("Запуск протоколирования сообщений");
            var internalMessageLogger = container.Resolve<InternalMessageLogger>();
            internalMessageLogger.Start();
            logger.End(2);

            Application.Run(container.ResolveUnregistered<MesForm>());

#if LOW_LEVEL
		    logger = new Logger("Остановка сервиса нижнего уровня");
            serviceHostLowLevel.SafeClose();
            logger.End(2);
#endif

#if HIGH_LEVEL
            logger = new Logger("Остановка сервиса верхнего уровня");
            serviceHostHighLevel.SafeClose();
            logger.End(2);
#endif

            logger = new Logger("Остановка протоколирования сообщений");
            internalMessageLogger.Stop(false);
            logger.End(2);
        }
    }
}