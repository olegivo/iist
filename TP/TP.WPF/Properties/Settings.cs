using System.Collections.Generic;
using System.Linq;

namespace TP.WPF.Properties
{
    internal sealed partial class Settings
    {
        private List<int> allowedChannelsIds;

        public List<int> AllowedChannelsIds
        {
            get
            {
                return allowedChannelsIds ??
                       (allowedChannelsIds = AllowedChannels
                           .Cast<string>()
                           .Select(s =>
                           {
                               int i;
                               var b = int.TryParse(s, out i);
                               return new {i, b};
                           })
                           .Where(item => item.b)
                           .Select(item => item.i)
                           .ToList());
            }
        }
    }
}