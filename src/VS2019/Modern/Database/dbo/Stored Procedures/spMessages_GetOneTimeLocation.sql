CREATE PROCEDURE [dbo].[spMessages_GetTimeLocation]
      @TimeCreated datetime2, @LocationCreated NVARCHAR(50)
AS
begin
      SELECT Id ToName, FromName, MessageText, TimeCreated ,LocationCreated, IsTestObject
      FROM dbo.Messages
      WHERE TimeCreated > @TimeCreated  and LocationCreated != '@LocationCreated';
end