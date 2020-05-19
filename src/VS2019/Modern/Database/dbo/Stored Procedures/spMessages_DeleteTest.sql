CREATE PROCEDURE [dbo].[spMessages_DeleteTest]
	
AS
begin
	set nocount on;

	delete from dbo.Messages
	where IsTestObject = 1
end
