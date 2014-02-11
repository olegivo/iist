CREATE TABLE [dbo].[WagoMeta] (
    [Id]             INT NOT NULL,
    [IsPLC]          BIT NOT NULL,
    [FieldBusTypeId] INT NULL,
    [IsDescrete]     BIT NOT NULL,
    [IsAnalog]       BIT NOT NULL,
    [IsInput]        BIT NOT NULL,
    [IsOutput]       BIT NOT NULL,
    [Size]           INT NULL
);

