ALTER TABLE [dbo].[FieldBusNode]
    ADD CONSTRAINT [DF_FieldBusNode_Enabled] DEFAULT ((1)) FOR [Enabled];

