    CREATE VIEW [dbo].[OrderItemsView]
	AS SELECT   dbo.Orders.OrderNum, dbo.Items.ItemNum, dbo.Items.Description, dbo.Items.Amount, dbo.Items.IsTestObject, 
                dbo.OrderItems.Quantity, dbo.ItemCategories.CategoryNum, dbo.ItemCategories.CategoryDescription, 
                dbo.Items.ImageFileName
    FROM        dbo.OrderItems INNER JOIN
                         dbo.Orders ON dbo.OrderItems.OrderId = dbo.Orders.Id INNER JOIN
                         dbo.Items ON dbo.OrderItems.ItemNum = dbo.Items.ItemNum INNER JOIN
                         dbo.ItemCategories ON dbo.OrderItems.CategoryNum = dbo.ItemCategories.CategoryNum
	    
