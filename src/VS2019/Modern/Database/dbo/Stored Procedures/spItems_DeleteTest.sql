CREATE PROCEDURE [dbo].[spItems_DeleteTest]
	
AS
begin
	set nocount on;

	delete from dbo.Items
	where IsTestObject = 1
end
