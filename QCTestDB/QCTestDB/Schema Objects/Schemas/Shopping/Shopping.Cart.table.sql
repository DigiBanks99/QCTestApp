CREATE TABLE [Shopping].[Cart]
(
  [CartID]        INT             NOT NULL  IDENTITY (1, 1),
  [UserID]        INT             NOT NULL,
  [Code]          VARCHAR (10)    NOT NULL,
  [DateCreated]  SMALLDATETIME   NOT NULL,
  [CartTotal]     DECIMAL (18, 2) NOT NULL,
  [ItemCount]     INT             NOT NULL,
  [Status]        VARCHAR (1)     NOT NULL  CONSTRAINT [DF_Cart_Status] DEFAULT ('N'),  
    
  CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([CartID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [FK_Cart_User] FOREIGN KEY ([UserID]) REFERENCES [Security].[User] ([UserID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [UK_Cart_CartCode] UNIQUE ([Code])
)
