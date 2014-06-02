ALTER TABLE [dbo].[ProtocolData]
    ADD CONSTRAINT [FK_ProtocolData_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

