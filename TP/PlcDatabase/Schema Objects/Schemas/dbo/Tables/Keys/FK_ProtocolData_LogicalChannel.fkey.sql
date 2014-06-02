ALTER TABLE [dbo].[ProtocolData]
    ADD CONSTRAINT [FK_ProtocolData_LogicalChannel] FOREIGN KEY ([LogicalChannelId]) REFERENCES [dbo].[LogicalChannel] ([Id]) ON DELETE NO ACTION ON UPDATE CASCADE;

