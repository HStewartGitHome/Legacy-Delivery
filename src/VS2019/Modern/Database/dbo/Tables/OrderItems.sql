CREATE TABLE [dbo].[OrderItems]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Quantity] INT NULL DEFAULT 1, 
    [CategoryNum] INT NOT NULL, 
    [OrderId] INT NOT NULL,
    [ItemNum] INT NOT NULL, 
    [OrderNum] INT NULL 
)

GO

CREATE INDEX [IX_OrderItems_CategoryNum] ON [dbo].[OrderItems] ([CategoryNum])

GO

CREATE INDEX [IX_OrderItems_ItemNum] ON [dbo].[OrderItems] ([ItemNum])

GO

CREATE INDEX [IX_OrderItems_OrderNum] ON [dbo].[OrderItems] ([OrderNum])
