namespace Oleg_ivo.HighLevelClient.LabViewAdapter
{
    ///<summary>
    ///
    ///</summary>
    public enum LabViewServerState
    {
        TF_WHO_AM_I = 0x0001,
        TF_WHO_AM_I_REPLY = 0x0002,
        TF_NEW_DATA = 0x0003,
        TF_NEW_CONTROL = 0x0004,
        TF_HEARTBEAT = 0x0005,
        TF_SERVER_ERROR = 0x0006,
        TF_WANT_CONFIG = 0x0007,
        TF_CONFIG_PART = 0x0008,
        TF_NEW_CFG = 0x0009,
        TF_CFG_ACCEPTED = 0x000A
    }
}