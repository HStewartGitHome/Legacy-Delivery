CREATE PROCEDURE [dbo].[spOrder_Create]
		@OrderNum int,
		@TimeCreated DateTime2,
		@LocationCreated NVARCHAR(50),
		@BeforeTax decimal(18,2),
		@Tax decimal(18,2),
		@Shipping decimal(18,2),
		@Tip decimal(18,2),
		@Total decimal(18,2),
		@IsTestObject bit
AS
BEGIN
   	set nocount on;

	INSERT INTO Orders (OrderNum,TimeCreated,LocationCreated,BeforeTax,Tax,Shipping,Tip,Total,IsTestObject)
	VALUES (@OrderNum,@TimeCreated,@LocationCreated,@BeforeTax,	@Tax,@Shipping,	@Tip,@Total,@IsTestObject)
END
