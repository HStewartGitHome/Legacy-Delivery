CREATE TABLE [dbo].[ItemCategories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryDescription] NCHAR(50) NULL, 
    [CategoryNum] INT NOT NULL, 
)

GO

CREATE INDEX [IX_ItemCategories_CategoryNum] ON [dbo].[ItemCategories] ([CategoryNum])
