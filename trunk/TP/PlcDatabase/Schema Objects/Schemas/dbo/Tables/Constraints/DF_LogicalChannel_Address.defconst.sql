ALTER TABLE [dbo].[LogicalChannel]
    ADD CONSTRAINT [DF_LogicalChannel_Address] DEFAULT ((0)) FOR [AddressShift];

