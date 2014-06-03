ALTER TABLE [dbo].[LogicalChannelClient]
    ADD CONSTRAINT [FK_LogicalChannelClient_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId]) ON DELETE NO ACTION ON UPDATE CASCADE;

