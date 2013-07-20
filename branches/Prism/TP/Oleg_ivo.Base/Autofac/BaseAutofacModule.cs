using System;
using System.Windows.Forms;
using Autofac;
using Oleg_ivo.PrismExtensions.Autofac.DependencyInjection;

namespace Oleg_ivo.Base.Autofac
{
    public class BaseAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<PropertyInjectionModule>();
            builder.RegisterModule<ConfigurationActionsModule>();
            // ��� ������������� �� ��������, ���������� �� IServiceProvider
            builder.RegisterAdapter((ILifetimeScope s) => (IServiceProvider)s).InstancePerLifetimeScope();
            // ��������� �������� ��� �������������
            System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            builder.RegisterInstance(System.Threading.SynchronizationContext.Current);
        }
    }
}