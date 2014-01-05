﻿CREATE TABLE [Security].[User]
(
  [UserID]    INT           NOT NULL  IDENTITY (1,1),
  [Code]      VARCHAR (10)  NOT NULL,
  [UserName]  VARCHAR (20)  NOT NULL,
  CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
  CONSTRAINT [UK_User_UserCode] UNIQUE ([Code])
)
