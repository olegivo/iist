CREATE TABLE [dbo].[ProtocolData] (
    [Id]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [LogicalChannelId] INT              NOT NULL,
    [TimeStamp]        DATETIME         NOT NULL,
    [QueueTimeStamp]   DATETIME         NOT NULL,
    [DataValue]        DECIMAL (18, 10) NOT NULL
);

