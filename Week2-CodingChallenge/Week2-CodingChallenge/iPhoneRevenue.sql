SELECT Orders.ID,
(SELECT Products.Price From Products WHERE Orders.ProductID = ID) AS Price
From Orders
WHERE ProductID = (SELECT ID from Products WHERE Products.name = 'iPhone');