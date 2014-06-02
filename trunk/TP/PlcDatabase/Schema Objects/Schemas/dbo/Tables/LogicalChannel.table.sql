CREATE TABLE [dbo].[LogicalChannel] (
    [Id]                    INT            NOT NULL,
    [ParameterId]           INT            NULL,
    [PhysicalChannelId]     INT            NULL,
    [Size]                  INT            NULL,
    [AddressShift]          INT            NULL,
    [Description]           NVARCHAR (MAX) NULL,
    [StateLogicalChannelId] INT            NULL
);



