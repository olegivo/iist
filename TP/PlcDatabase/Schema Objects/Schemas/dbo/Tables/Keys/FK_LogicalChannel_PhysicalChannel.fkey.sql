﻿ALTER TABLE [dbo].[LogicalChannel]
    ADD CONSTRAINT [FK_LogicalChannel_PhysicalChannel] FOREIGN KEY ([PhysicalChannelId]) REFERENCES [dbo].[PhysicalChannel] ([Id]) ON DELETE NO ACTION ON UPDATE CASCADE;

