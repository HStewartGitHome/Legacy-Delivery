CREATE PROCEDURE [dbo].[spOrders_MaxOrderNum]
	@MaxOrderNum int OUTPUT,
	@Count int OUTPUT
AS
BEGIN
	
	SELECT @Count = COUNT(OrderNum) FROM Orders
	SELECT @MaxOrderNum = MAX(OrderNum) FROM Orders
END
