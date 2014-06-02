ALTER TABLE [dbo].[FieldBusNode]
    ADD CONSTRAINT [FK_FieldBusNode_FieldBusType] FOREIGN KEY ([FieldBusTypeId]) REFERENCES [dbo].[FieldBusType] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

