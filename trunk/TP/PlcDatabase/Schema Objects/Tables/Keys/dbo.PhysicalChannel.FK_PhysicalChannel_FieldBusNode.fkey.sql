ALTER TABLE [dbo].[PhysicalChannel] ADD
CONSTRAINT [FK_PhysicalChannel_FieldBusNode] FOREIGN KEY ([FieldNodeId]) REFERENCES [dbo].[FieldBusNode] ([Id])


