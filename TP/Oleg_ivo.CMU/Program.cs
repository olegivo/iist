using System;
using System.Windows.Forms;
using Autofac;
using DMS.Common;
using DMS.Common.Messages;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Tools.ExceptionCatcher;
using Oleg_ivo.WAGO.Autofac;
using Oleg_ivo.WAGO.Configuration;

namespace Oleg_ivo.CMU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(args));
            builder.RegisterModule<WagoAutofacModule>();
            var container = builder.Build();
            var form = container.ResolveUnregistered<LowLevelClientForm>();

            var errorSenderWrapper = new ErrorSenderWrapper<ControlManagementUnit>(form.ControlManagementUnit);
            container.Resolve<ExceptionHandler>().AdditionalErrorHandler = errorSenderWrapper.LogError;
            Application.Run(form);
        }
    }
}