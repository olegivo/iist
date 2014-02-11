ALTER TABLE [dbo].[WagoMeta]
    ADD CONSTRAINT [FK_WagoMeta_FieldBusType] FOREIGN KEY ([FieldBusTypeId]) REFERENCES [dbo].[FieldBusType] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

