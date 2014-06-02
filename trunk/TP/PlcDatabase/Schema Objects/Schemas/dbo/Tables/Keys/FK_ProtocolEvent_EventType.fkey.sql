ALTER TABLE [dbo].[ProtocolEvent]
    ADD CONSTRAINT [FK_ProtocolEvent_EventType] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventType] ([EventTypeId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

