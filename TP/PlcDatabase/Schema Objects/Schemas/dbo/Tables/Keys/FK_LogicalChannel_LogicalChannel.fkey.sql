ALTER TABLE [dbo].[LogicalChannel]
    ADD CONSTRAINT [FK_LogicalChannel_LogicalChannel] FOREIGN KEY ([StateLogicalChannelId]) REFERENCES [dbo].[LogicalChannel] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

