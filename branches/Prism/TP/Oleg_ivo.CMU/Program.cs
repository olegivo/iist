using System;
using System.Windows.Forms;
using Autofac;
using DMS.Common.Messages;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.PrismExtensions.Autofac.DependencyInjection;
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

            var errors = new Errors(form.ControlManagementUnit);
            container.Resolve<ExceptionHandler>().AdditionalErrorHandler = errors.LogError;
            Application.Run(form);
        }
    }

    internal class Errors
    {
        private readonly Func<ControlManagementUnit> controlManagementUnitProvider;
        private ControlManagementUnit controlManagementUnit;

        private ControlManagementUnit ControlManagementUnit
        {
            get
            {
                return controlManagementUnit ?? (controlManagementUnit = controlManagementUnitProvider());
            }
        }
        public Errors(ControlManagementUnit controlManagementUnit)
        {
            this.controlManagementUnit = controlManagementUnit;
        }

        public Errors(Func<ControlManagementUnit> controlManagementUnitProvider)
        {
            this.controlManagementUnitProvider = controlManagementUnitProvider;
        }

        internal void LogError(object sender, ExtendedThreadExceptionEventArgs e)
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
                throw ex;
            }
        }

        void Proxy_SendErrorCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
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
    }
}