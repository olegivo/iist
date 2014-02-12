CREATE TABLE [dbo].[FieldBusNode]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[FieldBusTypeId] [int] NULL,
[AddressPart1] [nvarchar] (255) COLLATE Cyrillic_General_CI_AS NULL,
[AddressPart2] [int] NULL,
[Enabled] [bit] NOT NULL
) ON [PRIMARY]


