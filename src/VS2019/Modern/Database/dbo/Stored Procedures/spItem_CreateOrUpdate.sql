CREATE PROCEDURE [dbo].[spItem_CreateOrUpdate]
	@ItemNum int,
	@Description NVARCHAR(50),
    @Amount decimal(18,2),
	@CategoryNum int,
	@DefaultQuantity int,
	@ImageFileName NVARCHAR(50),
    @IsTestObject bit
AS
BEGIN
   	set nocount on;


	IF EXISTS ( SELECT ItemNum FROM Items WHERE ItemNum = @ItemNum ) 
		UPDATE Items SET Description = @Description, Amount = @Amount, CategoryNum = @CategoryNum,
		DefaultQuantity = @DefaultQuantity, ImageFileName = @ImageFileName, IsTestObject = @IsTestObject
		WHERE ItemNum = @ItemNum 
	ELSE
		INSERT INTO Items (ItemNum, Description, Amount, CategoryNum, DefaultQuantity, ImageFileName, IsTestObject )
		VALUES( @ItemNum, @Description, @Amount, @CategoryNum, @DefaultQuantity, @ImageFileName, @IsTestObject )
END 
