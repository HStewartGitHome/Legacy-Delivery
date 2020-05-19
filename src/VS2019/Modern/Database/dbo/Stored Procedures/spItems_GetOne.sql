CREATE PROCEDURE [dbo].[spItems_GetOne]
      @Id int
AS
begin
      SELECT Id, ItemNum, Description, Amount, DefaultQuantity, ImageFileName, IsTestObject, CategoryNum
      FROM dbo.Items
      WHERE Id = @Id;
end