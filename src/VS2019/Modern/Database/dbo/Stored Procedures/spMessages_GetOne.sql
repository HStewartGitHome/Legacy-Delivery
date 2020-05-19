CREATE PROCEDURE [dbo].[spMessages_GetOne]
      @Id int
AS
begin
      SELECT Id,ToName, FromName, MessageText, TimeCreated , LocationCreated, IsTestObject
      FROM dbo.Messages
      WHERE Id = @Id;
end