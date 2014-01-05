CREATE TABLE [Shopping].[Order]
(
  [OrderID]         INT             NOT NULL  IDENTITY (1, 1),
  [ItemID]          INT             NOT NULL,
  [Quantity]        INT             NOT NULL,
  [PricePerUnit]    DECIMAL (18, 2) NOT NULL,
  [Total]           DECIMAL (18, 2) NOT NULL,
  [Status]          VARCHAR (1)     NOT NULL CONSTRAINT [DF_Order_Status] DEFAULT ('N'),
  [OrderDate]       SMALLDATETIME   NOT NULL,
  [DispatchDate]    SMALLDATETIME   NULL,
  [CompletionDate]  SMALLDATETIME   NULL,

  CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [FK_Order_Item] FOREIGN KEY ([ItemID]) REFERENCES [Shopping].[Item]  ([ItemID]) ON DELETE NO ACTION ON UPDATE NO ACTION
)
