CREATE TABLE [Shopping].[Wishlist]
(
  [WishlistID]    INT           NOT NULL IDENTITY (1, 1),
  [UserID]        INT           NOT NULL,  
  [Code]          VARCHAR (10)  NOT NULL,
  [DateCreated]   SMALLDATETIME NOT NULL, 
  [WishlistName]  VARCHAR (30)  NOT NULL, 
  [CategoryID]    INT           NULL, 

  CONSTRAINT [PK_Wishlist] PRIMARY KEY CLUSTERED ([WishlistID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [FK_Wishlist_User] FOREIGN KEY ([UserID]) REFERENCES [Security].[User] ([UserID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_Wishlist_Category] FOREIGN KEY ([CategoryID]) REFERENCES [Shopping].[Category] ([CategoryID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [UK_Whislist_WishlistCode] UNIQUE ([Code])
)
