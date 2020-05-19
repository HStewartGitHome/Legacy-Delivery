CREATE PROCEDURE [dbo].[spMessage_Create]
    @ToName NVARCHAR(50), 
    @FromName NVARCHAR(50), 
    @MessageText NVARCHAR(255), 
    @TimeCreated DATETIME2, 
    @LocationCreated NVARCHAR(50), 
    @IsTestObject BIT
AS
BEGIN
    INSERT INTO Messages (ToName, FromName, MessageText, TimeCreated, LocationCreated, IsTestObject)
    VALUES (@ToName, @FromName, @MessageText, @TimeCreated, @LocationCreated, @IsTestObject)
END

