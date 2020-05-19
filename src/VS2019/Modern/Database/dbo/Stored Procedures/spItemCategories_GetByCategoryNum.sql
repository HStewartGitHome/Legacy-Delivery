CREATE PROCEDURE [dbo].[spItemCategories_GetByCategoryNum]
	@CategoryNum int
AS
begin
      SELECT  Id, CategoryNum, CategoryDescription
       FROM dbo.ItemCategories
       WHERE CategoryNum = @CategoryNum
end
