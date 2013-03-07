CREATE TABLE [dbo].[ProtocolData]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[LogicalChannelId] [int] NOT NULL,
[TimeStamp] [datetime] NOT NULL,
[QueueTimeStamp] [datetime] NOT NULL,
[DataValue] [decimal] (18, 10) NOT NULL
) ON [PRIMARY]


