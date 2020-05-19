CREATE PROCEDURE [dbo].[spOrders_Get]
AS
BEGIN
	SELECT Id, OrderNum, LocationCreated, BeforeTax, Tax, Shipping, Tax, Tip, Total, TimeCreated, IsTestObject 
		FROM dbo.Orders
END
