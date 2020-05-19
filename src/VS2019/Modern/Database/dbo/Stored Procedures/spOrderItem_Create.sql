CREATE PROCEDURE [dbo].[spOrderItem_Create]
	@ItemNum int, 
	@Quantity int,
	@CategoryNum int,
	@OrderNum int,
	@OrderId int,
	@Id int OUTPUT
AS
BEGIN
	set nocount on;

	INSERT INTO OrderItems (ItemNum,Quantity,CategoryNum,OrderNum,OrderId)
	VALUES (@ItemNum,@Quantity,@CategoryNum,@OrderNum,@OrderId)
	SET @Id = @@IDENTITY
END
