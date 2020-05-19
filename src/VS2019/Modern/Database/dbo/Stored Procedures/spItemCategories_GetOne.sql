CREATE PROCEDURE [dbo].[spItemCategories_GetOne]
	@Id int
AS
begin
      SELECT  Id, CategoryNum, CategoryDescription
       FROM dbo.ItemCategories
       WHERE Id = @Id
end
