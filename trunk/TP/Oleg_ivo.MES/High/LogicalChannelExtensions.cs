using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.MES.High
{
    public static class LogicalChannelExtensions
    {
         public static IRegisteredChannel GetRegisteredChannel(this LogicalChannel channel)
         {
             return new RegisteredLogicalChannelExtended(channel.Id, DataMode.Unknown, LogicalChannelState.Break
/*TODO: DataMode.Unknown*/)
                 {
                     Description = channel.Description,
                     MinValue = channel.MinValue,
                     MaxValue = channel.MaxValue,
                     MinNormalValue = channel.MinNormalValue,
                     MaxNormalValue = channel.MaxNormalValue
                 };
         }
    }
}