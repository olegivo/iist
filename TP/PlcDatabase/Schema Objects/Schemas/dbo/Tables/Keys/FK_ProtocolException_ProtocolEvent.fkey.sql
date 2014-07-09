ALTER TABLE [dbo].[ProtocolException]
    ADD CONSTRAINT [FK_ProtocolException_ProtocolEvent] FOREIGN KEY ([ProtocolEventId]) REFERENCES [dbo].[ProtocolEvent] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE;

