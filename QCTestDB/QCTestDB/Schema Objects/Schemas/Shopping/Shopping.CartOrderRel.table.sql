﻿CREATE TABLE [Shopping].[CartOrderRel]
(
  [CartOrderRelID]  INT           NOT NULL IDENTITY (1, 1),
  [CartID]          INT           NOT NULL, 
  [OrderID]         INT           NOT NULL, 
  [Processed]       BIT           NOT NULL,
  CONSTRAINT [PK_CartOrderRel] PRIMARY KEY CLUSTERED ([CartOrderRelID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [FK_CartOrderRel_Cart] FOREIGN KEY ([CartID]) REFERENCES [Shopping].[Cart] ([CartID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_CartOrderRel_Order] FOREIGN KEY ([OrderID]) REFERENCES [Shopping].[Order] ([OrderID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [UK_CartOrderRel] UNIQUE ([CartID],[OrderID])
)
