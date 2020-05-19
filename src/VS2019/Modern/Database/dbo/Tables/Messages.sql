CREATE TABLE [dbo].[Messages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ToName] NCHAR(50) NULL, 
    [FromName] NCHAR(50) NULL, 
    [MessageText] NCHAR(255) NULL, 
    [TimeCreated] DATETIME2 NULL, 
    [LocationCreated] NCHAR(50) NULL, 
    [IsTestObject] BIT NULL
)
