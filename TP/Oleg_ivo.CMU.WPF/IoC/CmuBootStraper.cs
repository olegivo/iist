using System.ComponentModel;
using Autofac;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.CMU.WPF.ViewModels;
using Oleg_ivo.WAGO.Configuration;

namespace Oleg_ivo.CMU.WPF.IoC
{
    public class CmuBootStraper : AutofacBootstrapperBase<MainWindow, WagoCommandLineOptions, CmuPrismModule, CmuAutofacModule>
    {
        public CmuBootStraper(string[] args) : base(args)
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if(e.Cancel) return;

            var viewModel = Container.Resolve<MainViewModel>();
            if(viewModel.CanUnRegister)
                viewModel.Unregister();
        }
    }
}