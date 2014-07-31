using System.ComponentModel;
using Autofac;
using Oleg_ivo.Base.Autofac.Modules;
using TP.WPF.ViewModels;
using TP.WPF.Views;

namespace TP.WPF.IoC
{
    public class TpBootStraper : AutofacBootstrapperBase<MainShell, TpCommandLineOptions, TpPrismModule, TpAutofacModule>
    {
        public TpBootStraper(string[] args) : base(args)
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (e.Cancel) return;

            var viewModel = Container.Resolve<MainViewModel>();
            if (viewModel.CanUnregister)
                viewModel.Unregister();
        }
    }
}