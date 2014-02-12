ALTER TABLE [dbo].[FieldBus] ADD
CONSTRAINT [FK_FieldBus_FieldBusType] FOREIGN KEY ([FieldBusTypeId]) REFERENCES [dbo].[FieldBusType] ([Id])


