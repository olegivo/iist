﻿ALTER TABLE [dbo].[LogicalChannelClient]
    ADD CONSTRAINT [UK_LogicalChannelClient] UNIQUE NONCLUSTERED ([ClientId] ASC, [LogicalChannnelId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

