CREATE PROCEDURE [dbo].[spOrders_DeleteBeforeDate]
	@Time DateTime2 
AS
BEGIN
	DELETE FROM Orders WHERE TimeCreated < @Time
END

