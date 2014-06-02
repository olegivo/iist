ALTER TABLE [dbo].[EventType]
    ADD CONSTRAINT [FK_EventType_EventLevel] FOREIGN KEY ([EventLevelId]) REFERENCES [dbo].[EventLevel] ([EventLevelId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

