CREATE TABLE [dbo].[FieldBusNode] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [FieldBusId]     INT            NOT NULL,
    [FieldBusTypeId] INT            NULL,
    [AddressPart1]   NVARCHAR (255) NULL,
    [AddressPart2]   INT            NULL,
    [Enabled]        BIT            NOT NULL
);

