CREATE PROCEDURE [dbo].[spItemCategorie_Get]
AS
begin
      SELECT  Id, CategoryNum, CategoryDescription
       FROM dbo.ItemCategories
end
