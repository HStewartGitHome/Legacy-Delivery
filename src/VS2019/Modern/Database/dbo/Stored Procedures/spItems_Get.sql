CREATE PROCEDURE [dbo].[spItems_Get]
	
AS
begin
      SELECT  Id, ItemNum, Description, Amount, DefaultQuantity, ImageFileName, IsTestObject, CategoryNum
       FROM dbo.Items
end