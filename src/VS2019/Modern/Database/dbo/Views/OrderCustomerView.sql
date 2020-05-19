CREATE VIEW [dbo].[OrderCustomerView]
	AS SELECT dbo.Orders.OrderNum, dbo.Orders.TimeCreated, dbo.Orders.LocationCreated,  dbo.Orders.BeforeTax, 
              dbo.Orders.Tax, dbo.Orders.Shipping, dbo.Orders.Tip, dbo.Orders.Total, dbo.Orders.IsTestObject, 
              dbo.OrderCustomers.CustomerNum, dbo.OrderCustomers.CustomerName, dbo.OrderCustomers.CustomerAddress, 
              dbo.OrderCustomers.CustomerCity, dbo.OrderCustomers.CustomerState, dbo.OrderCustomers.CustomerZip
    FROM      dbo.Orders INNER JOIN
              dbo.OrderCustomers ON dbo.Orders.OrderNum = dbo.OrderCustomers.OrderNum
