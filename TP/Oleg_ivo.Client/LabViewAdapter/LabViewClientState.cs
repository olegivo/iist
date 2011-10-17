namespace Oleg_ivo.HighLevelClient.LabViewAdapter
{
    ///<summary>
    ///
    ///</summary>
    public enum LabViewClientState
    {
        STATE_START = 0,
        STATE_SEND_WAI = 1,
        STATE_RECEIVE_WAI_REPLY = 2,
        STATE_LOGICAL_ERROR = 3,
        STATE_CONNECT_ERROR = 4,
        STATE_CLOSE = 5,
        STATE_STOP = 6,
        STATE_WAIT_FOR_DATA = 7,
        STATE_READ_PACKET = 8,
        STATE_SEND_CFG_ACCEPTED = 9,
        STATE_SEND_NEW_CONTROL = 10,
        STATE_READ_NEW_DATA = 11,
        STATE_READ_SERVER_ERROR = 12,
        STATE_SEND_WANT_CFG = 13,
        STATE_READ_CFG_PART = 14
    }
}
