CREATE VIEW [dbo].[ItemsView]																								
	AS SELECT dbo.Items.ItemNum, dbo.Items.Description, dbo.Items.Amount, dbo.Items.IsTestObject, dbo.Items.DefaultQuantity, 
			  dbo.Items.ImageFileName, dbo.ItemCategories.CategoryDescription, dbo.ItemCategories.CategoryNum
	FROM      dbo.ItemCategories INNER JOIN
              dbo.Items ON dbo.ItemCategories.Id = dbo.Items.Id
