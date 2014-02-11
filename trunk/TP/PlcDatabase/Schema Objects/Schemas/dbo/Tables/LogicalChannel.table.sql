CREATE TABLE [dbo].[LogicalChannel] (
    [Id]                INT              NOT NULL,
    [ParameterId]       INT              NULL,
    [PhysicalChannelId] INT              NULL,
    [DirectPolynom]     NVARCHAR (MAX)   NULL,
    [ReversePolynom]    NVARCHAR (MAX)   NULL,
    [Size]              INT              NULL,
    [AddressShift]      INT              NULL,
    [PollPeriod]        DECIMAL (18, 6)  NULL,
    [Description]       NVARCHAR (MAX)   NULL,
    [SensivityDelta]    DECIMAL (18, 10) NULL,
    [MinValue]          DECIMAL (18, 10) NULL,
    [MaxValue]          DECIMAL (18, 10) NULL,
    [MinNormalValue]    DECIMAL (18, 10) NULL,
    [MaxNormalValue]    DECIMAL (18, 10) NULL
);

