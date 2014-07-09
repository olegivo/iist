CREATE TABLE [dbo].[ProtocolEvent] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventTypeId]      SMALLINT       NOT NULL,
    [ClientId]         INT            NOT NULL,
    [LogicalChannelId] INT            NULL,
    [TimeStamp]        DATETIME       NOT NULL,
    [QueueTimeStamp]   DATETIME       NOT NULL,
    [Message]          NVARCHAR (MAX) NULL,
    [Data]             NVARCHAR (50)  NULL
);



