SELECT Orders.ID,
(SELECT Products.name From Products WHERE Orders.ProductID = ID) AS ProductName
From Orders
WHERE CustomerID = (SELECT ID from Customers WHERE Customers.Firstname = 'Tina' AND Customers.Lastname = 'Smith');