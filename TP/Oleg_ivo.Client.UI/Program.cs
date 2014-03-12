using System;
using System.Windows.Forms;
using Autofac;
using DMS.Common;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace Oleg_ivo.HighLevelClient.UI
{
    static class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Info("Регистрация компонентов");
            var builder = new ContainerBuilder();
            //builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(args));
            builder.RegisterModule<BaseAutofacModule>();
            //builder.RegisterModule<WagoAutofacModule>();
            var container = builder.Build();
            var form = container.ResolveUnregistered<HighLevelClientForm>();

            var errorSenderWrapper = new ErrorSenderWrapper<ClientProvider>(form.Provider);
            container.Resolve<ExceptionHandler>().AdditionalErrorHandler = errorSenderWrapper.LogError;
            Application.Run(form);
        }
    }

}