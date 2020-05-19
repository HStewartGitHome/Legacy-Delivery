CREATE PROCEDURE [dbo].[spItemCategory_CreateOrUpdate]
	@CategoryNum int,
	@CategoryDescription NVARCHAR(50)
AS
BEGIN
   	set nocount on;


	IF EXISTS ( SELECT CategoryNum FROM ItemCategories WHERE CategoryNum = @CategoryNum ) 
		UPDATE ItemCategories SET CategoryDescription = @CategoryDescription
		WHERE CategoryNum = @CategoryNum 
	ELSE
		INSERT INTO ItemCategories (CategoryNum, CategoryDescription )
		VALUES( @CategoryNum, @CategoryDescription )
END 
