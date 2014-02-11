CREATE TABLE [dbo].[LogicalChannel]
(
[Id] [int] NOT NULL,
[PhysicalChannelId] [int] NULL,
[DirectPolynom] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NULL,
[ReversePolynom] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NULL,
[Size] [int] NULL,
[AddressShift] [int] NULL,
[PollPeriod] [decimal] (18, 6) NULL,
[Description] [nvarchar] (max) COLLATE Cyrillic_General_CI_AS NULL,
[SensivityDelta] [decimal] (18, 10) NULL,
[MinValue] [decimal] (18, 10) NULL,
[MaxValue] [decimal] (18, 10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


