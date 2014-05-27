using System.IO;
using System.Reflection;

namespace TP.WPF.Properties
{
    internal sealed partial class Settings
    {
        private LogicalChannelMappings logicalChannelMappings;

        public LogicalChannelMappings LogicalChannelMappings
        {
            get
            {
                return logicalChannelMappings ?? (logicalChannelMappings = CreateChannelsMappings(ChannelMappingsFile));
            }
        }

        private static LogicalChannelMappings CreateChannelsMappings(string channelMappingsFile)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(location);
            var path = Path.Combine(directory, channelMappingsFile);
            return File.Exists(path) ? File.ReadAllText(path).Deserialize<LogicalChannelMappings>() : null;
        }

    }
}