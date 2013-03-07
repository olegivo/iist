CREATE TABLE [dbo].[PhysicalChannel]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[FieldNodeId] [int] NULL,
[AddressShift] [smallint] NULL,
[ReadAddress] [int] NULL,
[WriteAddress] [int] NULL,
[PhysicalChannelSize] [int] NULL,
[IsInput] [bit] NOT NULL,
[IsOutput] [bit] NOT NULL,
[IsAnalog] [bit] NOT NULL,
[IsDiscrete] [bit] NOT NULL,
[Description] [nvarchar] (255) COLLATE Cyrillic_General_CI_AS NULL,
[Register] [int] NULL
) ON [PRIMARY]


