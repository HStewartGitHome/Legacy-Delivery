CREATE PROCEDURE [dbo].[spItemCategories_Delete]
	@Id int
AS
begin
	set nocount on;

	delete from dbo.ItemCategories
	where Id = @Id;
end
