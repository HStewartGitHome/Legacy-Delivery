CREATE PROCEDURE [dbo].[spOrder_CreateCust]
		@OrderNum int,
		@TimeCreated DateTime2,
		@LocationCreated NVARCHAR(50),
		@BeforeTax decimal(18,2),
		@Tax decimal(18,2),
		@Shipping decimal(18,2),
		@Tip decimal(18,2),
		@Total decimal(18,2),
		@IsTestObject bit,
		@CustomerNum int,
		@CustomerName NVARCHAR(50),
		@CustomerAddress NVARCHAR(50),
		@CustomerCity NVARCHAR(50),
		@CustomerState NVARCHAR(50),
		@CustomerZip NVARCHAR(50),
		@Id int OUTPUT
AS
BEGIN
   	set nocount on;
	declare @OrderId int
	

	INSERT INTO Orders (OrderNum,TimeCreated,LocationCreated,BeforeTax,Tax,Shipping,Tip,Total,IsTestObject)
	VALUES (@OrderNum,@TimeCreated,@LocationCreated,@BeforeTax,	@Tax,@Shipping,	@Tip,@Total,@IsTestObject)

	
	SET @OrderId = @@IDENTITY
	SET @Id = @@IDENTITY
	if (@CustomerNum > 0 )
	BEGIN
		INSERT INTO OrderCustomers (CustomerNum, CustomerName, CustomerAddress, CustomerCity, CustomerState, CustomerZip, OrderNum, OrderId )
		VALUES (@CustomerNum, @CustomerName, @CustomerAddress, @CustomerCity, @CustomerState, @CustomerZip, @OrderNum, @OrderID )
	END
END
