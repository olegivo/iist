ALTER TABLE [dbo].[LogicalChannelClient]
    ADD CONSTRAINT [FK_LogicalChannelClient_LogicalChannel] FOREIGN KEY ([LogicalChannnelId]) REFERENCES [dbo].[LogicalChannel] ([Id]) ON DELETE NO ACTION ON UPDATE CASCADE;

