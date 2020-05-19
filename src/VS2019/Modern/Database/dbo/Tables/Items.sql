CREATE TABLE [dbo].[Items]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemNum] INT NOT NULL, 
    [Description] NCHAR(50) NULL, 
    [Amount] DECIMAL(18, 2) NULL, 
    [IsTestObject] BIT NULL, 
    [DefaultQuantity] INT NULL, 
    [ImageFileName] NVARCHAR(50) NULL, 
    [CategoryNum] INT NOT NULL 
)

GO

CREATE INDEX [IX_Items_ItemNum] ON [dbo].[Items] ([ItemNum])

GO

CREATE INDEX [IX_Items_CategoryNum] ON [dbo].[Items] ([CategoryNum])
