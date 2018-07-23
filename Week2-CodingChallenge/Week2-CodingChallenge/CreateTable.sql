CREATE TABLE Products(
  ID INT NOT NULL PRIMARY KEY,
  name varchar(255), 
  Price DECIMAL(10,2));

CREATE TABLE Customers(
  ID INT NOT NULL PRIMARY KEY,
  Firstname varchar(255),
  Lastname varchar(255),
  CardNumber INT);
CREATE TABLE Orders(
  ID INT,
  ProductID INT,
  CustomerID INT,
  FOREIGN KEY (ProductID) REFERENCES Products(ID),
  FOREIGN KEY (CustomerID) REFERENCES Customers(ID));
  