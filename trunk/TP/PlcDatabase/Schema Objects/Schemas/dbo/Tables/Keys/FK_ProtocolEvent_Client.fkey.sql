ALTER TABLE [dbo].[ProtocolEvent]
    ADD CONSTRAINT [FK_ProtocolEvent_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId]) ON DELETE CASCADE ON UPDATE CASCADE;

