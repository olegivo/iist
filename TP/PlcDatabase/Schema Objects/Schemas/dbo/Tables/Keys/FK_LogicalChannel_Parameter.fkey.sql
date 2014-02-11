ALTER TABLE [dbo].[LogicalChannel]
    ADD CONSTRAINT [FK_LogicalChannel_Parameter] FOREIGN KEY ([ParameterId]) REFERENCES [dbo].[Parameter] ([Id]) ON DELETE NO ACTION ON UPDATE CASCADE;

