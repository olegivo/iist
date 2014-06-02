CREATE TABLE [dbo].[ProtocolEvent] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventTypeId]    SMALLINT       NOT NULL,
    [TimeStamp]      DATETIME       NOT NULL,
    [QueueTimeStamp] DATETIME       NOT NULL,
    [Message]        NVARCHAR (MAX) NOT NULL
);

