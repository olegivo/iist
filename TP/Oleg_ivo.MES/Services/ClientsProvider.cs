using System.Linq;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Plc.Entities;

namespace Oleg_ivo.MES.Services
{
    public class ClientsProvider
    {
        private readonly PlcDataContext dataContext;

        public ClientsProvider(PlcDataContext dataContext)
        {
            this.dataContext = Enforce.ArgumentNotNull(dataContext, "dataContext");
        }

        public int? GetClientId(string clientName)
        {
            return
                dataContext.Clients
                    .Where(client => client.ClientName == clientName)
                    .Select(client => (int?)client.ClientId)
                    .SingleOrDefault();
        }
    }
}