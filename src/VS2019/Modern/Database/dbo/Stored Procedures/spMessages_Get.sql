CREATE PROCEDURE [dbo].[spMessages_Get]
	
AS
begin
      SELECT Id,ToName, FromName, MessageText, TimeCreated , LocationCreated, IsTestObject
      FROM dbo.Messages
end