CREATE TABLE [dbo].[WagoMeta]
(
[Id] [int] NOT NULL,
[IsPLC] [bit] NOT NULL,
[FieldBusTypeId] [int] NULL,
[IsDescrete] [bit] NOT NULL,
[IsAnalog] [bit] NOT NULL,
[IsInput] [bit] NOT NULL,
[IsOutput] [bit] NOT NULL,
[Size] [int] NULL
) ON [PRIMARY]


