ALTER TABLE [dbo].[Parameter]
    ADD CONSTRAINT [FK_Parameter_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [dbo].[MeasurementUnit] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

