CREATE TABLE [dbo].[PhysicalChannel] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [FieldNodeId]         INT            NULL,
    [AddressShift]        SMALLINT       NULL,
    [ReadAddress]         INT            NULL,
    [WriteAddress]        INT            NULL,
    [PhysicalChannelSize] INT            NULL,
    [IsInput]             BIT            NOT NULL,
    [IsOutput]            BIT            NOT NULL,
    [IsAnalog]            BIT            NOT NULL,
    [IsDiscrete]          BIT            NOT NULL,
    [Description]         NVARCHAR (255) NULL,
    [Register]            INT            NULL
);

