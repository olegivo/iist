using System.Data;
using Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    public abstract class FactoryBase
    {
        private PlcDataContext dataContext;

        [Dependency]
        public IComponentContext Context { get; set; }

        protected PlcDataContext DataContext
        {
            get 
            {
                return dataContext ??
                       (dataContext =
                           Context.Resolve<PlcDataContext>(new TypedParameter(typeof (string),
                               Context.Resolve<DbConnectionProvider>().DefaultConnectionString)));
            }
        }
    }
}