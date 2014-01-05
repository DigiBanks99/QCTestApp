﻿CREATE TABLE [Shopping].[ItemPicture]
(
  [ItemPictureID]   INT   NOT NULL  IDENTITY (1, 1),
  [ItemID]      INT   NOT NULL,
  [Picture]     IMAGE NOT NULL,

  CONSTRAINT [PK_ItemPictureID] PRIMARY KEY CLUSTERED ([ItemPictureID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [FK_ItemPicture_Item] FOREIGN KEY ([ItemID]) REFERENCES [Shopping].[Item] ([ItemID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [UK_ItemPicture_Item] UNIQUE ([ItemID])
)
