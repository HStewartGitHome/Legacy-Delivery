CREATE PROCEDURE [dbo].[spItems_GetByCategoryNum]
      @CategoryNum int
AS
begin
      SELECT Id, ItemNum, Description, Amount, DefaultQuantity, ImageFileName, IsTestObject, CategoryNum
      FROM dbo.Items
      WHERE CategoryNum = @CategoryNum
end
