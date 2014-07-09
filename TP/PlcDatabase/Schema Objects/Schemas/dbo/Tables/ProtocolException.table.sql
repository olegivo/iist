CREATE TABLE [dbo].[ProtocolException] (
    [ProtocolEventId] BIGINT         NOT NULL,
    [Message]         NVARCHAR (MAX) NOT NULL,
    [StackTrace]      NVARCHAR (MAX) NOT NULL
);

