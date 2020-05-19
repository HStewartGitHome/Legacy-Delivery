CREATE PROCEDURE [dbo].[spMessages_DeleteBeforeDate]
	@Time DateTime2 
AS
BEGIN
	DELETE FROM Messages WHERE TimeCreated < @Time
END
