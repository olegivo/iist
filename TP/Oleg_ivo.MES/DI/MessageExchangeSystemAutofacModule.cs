using Autofac;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.MES.Services;
using Oleg_ivo.Plc.Entities;

namespace Oleg_ivo.MES.DI
{
    public class MessageExchangeSystemAutofacModule : BaseAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PlcDataContext>().SingleInstance();//TODO: регистрация конструктора в контексте
            builder.RegisterType<ClientsProvider>().SingleInstance();
        }
    }
}
