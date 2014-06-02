CREATE TABLE [dbo].[Parameter] (
    [Id]                INT              NOT NULL,
    [MeasurementUnitId] INT              NOT NULL,
    [Name]              VARCHAR (50)     NULL,
    [Description]       VARCHAR (200)    NULL,
    [DirectPolynom]     NVARCHAR (MAX)   NULL,
    [ReversePolynom]    NVARCHAR (MAX)   NULL,
    [PollPeriod]        DECIMAL (18, 6)  NULL,
    [SensivityDelta]    DECIMAL (18, 10) NULL,
    [MinValue]          DECIMAL (18, 10) NULL,
    [MaxValue]          DECIMAL (18, 10) NULL,
    [MinNormalValue]    DECIMAL (18, 10) NULL,
    [MaxNormalValue]    DECIMAL (18, 10) NULL
);



