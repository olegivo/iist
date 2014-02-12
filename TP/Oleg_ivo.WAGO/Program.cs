using System;
using System.Windows.Forms;
using Autofac;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.WAGO.Autofac;
using Oleg_ivo.WAGO.Configuration;
using Oleg_ivo.WAGO.Forms;

namespace Oleg_ivo.WAGO
{
    static class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Log.Info("����������� �����������");
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(args));
            builder.RegisterModule<WagoAutofacModule>();
            var container = builder.Build();

            //TODO:������ Instance ������������ ��������
            //���-��� ��� ������������ Instance, ��� ����� ����������������:
            var connectionProvider = container.Resolve<DbConnectionProvider>();

            Log.Info("������ ������� �����");
            Application.Run(container.ResolveUnregistered<MDIParentMain>());
        }
    }
}