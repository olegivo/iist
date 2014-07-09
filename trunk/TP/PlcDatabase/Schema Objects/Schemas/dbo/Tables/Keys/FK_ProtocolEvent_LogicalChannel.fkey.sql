ALTER TABLE [dbo].[ProtocolEvent]
    ADD CONSTRAINT [FK_ProtocolEvent_LogicalChannel] FOREIGN KEY ([LogicalChannelId]) REFERENCES [dbo].[LogicalChannel] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE;

