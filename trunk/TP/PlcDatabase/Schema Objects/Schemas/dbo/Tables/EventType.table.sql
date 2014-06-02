CREATE TABLE [dbo].[EventType] (
    [EventTypeId]  SMALLINT       IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (255) NULL,
    [EventLevelId] SMALLINT       NOT NULL
);

