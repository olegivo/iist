namespace DMS.Common.Messages
{
    public enum EventType : short
    {
        Error = 1,
        ClientRegistration = 2,
        ChannelRegistration = 3,
        ChannelSubscription = 4,
        LogicalChannelState = 5
    }
}