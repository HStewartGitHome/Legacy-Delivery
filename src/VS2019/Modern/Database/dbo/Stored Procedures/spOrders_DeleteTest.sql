CREATE PROCEDURE [dbo].[spOrders_DeleteTest]
	
AS
begin
	set nocount on;

	delete from dbo.Orders
	where IsTestObject = 1
end
