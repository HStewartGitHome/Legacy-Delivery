CREATE PROCEDURE [dbo].[spItems_Delete]
	@Id int
AS
begin
	set nocount on;

	delete from dbo.Items
	where Id = @Id;
end
