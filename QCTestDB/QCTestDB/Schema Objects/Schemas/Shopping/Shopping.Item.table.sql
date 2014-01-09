CREATE TABLE [Shopping].[Item]
(
  [ItemID]            INT             NOT NULL  IDENTITY (1, 1),
  [Code]              VARCHAR (16)    NOT NULL,
  [ItemName]          VARCHAR (50)    NOT NULL,
  [ShortDescription]  VARCHAR (100)   NOT NULL,
  [Description]       VARCHAR (MAX)   NOT NULL,
  [Price]             DECIMAL (18, 2) NOT NULL,
  [CategoryID]        INT             NOT NULL,

  CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([ItemID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [UK_Item_ItemCode] UNIQUE ([Code])
)
