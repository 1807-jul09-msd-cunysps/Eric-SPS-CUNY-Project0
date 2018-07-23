INSERT INTO Products(ID, name, Price)
VALUES (1, 'iPhone', 200),
	(2, 'iPad', 270),
	(3, 'Mac', 780);

INSERT INTO Customers(ID, Firstname, Lastname)
VALUES (1, 'Tina', 'Smith'),
	(2, 'Tina', 'John'),
	(3, 'John', 'Tina');


INSERT INTO Orders(ID,ProductID,CustomerID)
VALUES (1,
(SELECT ID from Products WHERE Products.name = 'iPhone'),
(SELECT ID from Customers WHERE Customers.Firstname = 'Tina' AND Customers.Lastname = 'Smith')),
(2,
(SELECT ID from Products WHERE Products.name = 'iPad'),
(SELECT ID from Customers WHERE Customers.Firstname = 'Tina' AND Customers.Lastname = 'Smith')),
(3,
(SELECT ID from Products WHERE Products.name = 'Mac'),
(SELECT ID from Customers WHERE Customers.Firstname = 'Tina' AND Customers.Lastname = 'Smith'));

