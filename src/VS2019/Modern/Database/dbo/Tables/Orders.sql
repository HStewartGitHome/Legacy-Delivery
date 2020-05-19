CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TimeCreated] DATETIME2 NULL, 
    [LocationCreated] NVARCHAR(50) NULL, 
    [BeforeTax] DECIMAL(18, 2) NULL, 
    [Tax] DECIMAL(18, 2) NULL, 
    [Shipping] DECIMAL(18, 2) NULL, 
    [Tip] DECIMAL(18, 2) NULL, 
    [Total] DECIMAL(18, 2) NULL, 
    [IsTestObject] BIT NULL, 
    [OrderNum] INT NOT NULL
)

GO

CREATE INDEX [IX_Orders_OrderNum] ON [dbo].[Orders] ([OrderNum])

GO

CREATE TRIGGER [dbo].[Trigger_OrdersDelete]
    ON [dbo].[Orders]
    FOR DELETE
    AS
    BEGIN
        SET NoCount ON

        DELETE FROM OrderItems WHERE OrderNum = OrderItems.OrderNum 
        DELETE FROM OrderCustomers WHERE OrderNum = OrderCustomers.OrderNum
    END