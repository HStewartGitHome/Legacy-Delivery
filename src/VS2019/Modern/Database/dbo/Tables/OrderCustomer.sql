CREATE TABLE [dbo].[OrderCustomers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerNum] INT NULL, 
    [CustomerName] NCHAR(50) NULL, 
    [CustomerAddress] NCHAR(50) NULL, 
    [CustomerCity] NVARCHAR(50) NULL, 
    [CustomerState] NCHAR(2) NULL, 
    [CustomerZip] NCHAR(10) NULL, 
    [OrderId] INT NOT NULL, 
    [OrderNum] INT NOT NULL
)

GO

