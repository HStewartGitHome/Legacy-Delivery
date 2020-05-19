CREATE PROCEDURE [dbo].[spOrders_GetOne]
      @Id int
AS
BEGIN
	SELECT Id, OrderNum, LocationCreated, BeforeTax, Tax, Shipping, Tax, Tip, Total, TimeCreated, IsTestObject 
		FROM dbo.Orders
    WHERE Id = @Id;
END
