using System.Linq;
using Autofac;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.MES.Services
{
    public class ClientsProvider
    {
        private PlcDataContext dataContext;
        private readonly IComponentContext context;

        public ClientsProvider(IComponentContext context)
        {
            this.context = Enforce.ArgumentNotNull(context, "");
        }

        private PlcDataContext DataContext
        {
            get
            {
                lock (context)
                {
                    return dataContext ??
                           (dataContext =
                            context.Resolve<PlcDataContext>(new TypedParameter(typeof(string),
                                                                               context.Resolve<DbConnectionProvider>().DefaultConnectionString)));                    
                }
            }
        }

        public int? GetClientId(string clientName)
        {
            lock (DataContext)
            {
                return
                    DataContext.Clients
                               .Where(client => client.ClientName == clientName)
                               .Select(client => (int?)client.ClientId)
                               .SingleOrDefault();
                
            }
        }
    }
}