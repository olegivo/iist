ALTER TABLE [dbo].[IoModule] ADD
CONSTRAINT [FK_IoModule_PhysicalChannel] FOREIGN KEY ([PhisylcaIChanneld]) REFERENCES [dbo].[PhysicalChannel] ([Id])


