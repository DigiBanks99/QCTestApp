CREATE TABLE [Shopping].[WishlistItem]
(
  [WishlistItemID]  INT           NOT NULL  IDENTITY (1,1), 
  [WishlistID]      INT           NOT NULL, 
  [ItemID]          INT           NOT NULL, 
  [DateAdded]       SMALLDATETIME NOT NULL,

  CONSTRAINT [PK_WishlistItems] PRIMARY KEY CLUSTERED ([WishlistItemID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [FK_WishlistItems_Wishlist] FOREIGN KEY ([WishlistID]) REFERENCES [Shopping].[Wishlist] ([WishlistID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_WishlistItems_Item] FOREIGN KEY ([ItemID]) REFERENCES [Shopping].[Item] ([ItemID]) ON DELETE NO ACTION ON UPDATE NO ACTION
)
