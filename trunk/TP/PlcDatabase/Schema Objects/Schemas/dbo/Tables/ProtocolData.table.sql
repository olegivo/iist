CREATE TABLE [dbo].[ProtocolData] (
    [Id]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [ClientId]         INT              NOT NULL,
    [LogicalChannelId] INT              NOT NULL,
    [TimeStamp]        DATETIME         NOT NULL,
    [QueueTimeStamp]   DATETIME         NOT NULL,
    [DataType]         INT              NOT NULL,
    [AnalogValue]      DECIMAL (18, 10) NULL,
    [DiscreteValue]    BIT              NULL
);



