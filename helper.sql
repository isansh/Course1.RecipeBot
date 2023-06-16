USE RecipeDB;
CREATE TABLE Products
(
    Id INT IDENTITY PRIMARY KEY,
    ProductName NVARCHAR(30) NOT NULL,
    Manufacturer NVARCHAR(20) NOT NULL,
    ProductCount INT DEFAULT 0,
    Price MONEY NOT NULL
)
INSERT Products VALUES ('iPhone 7', 'Apple', 5, 52000)
INSERT INTO Products 
VALUES
('iPhone 6', 'Apple', 3, 36000),
('Galaxy S8', 'Samsung', 2, 46000),
('Galaxy S8 Plus', 'Samsung', 1, 56000)

SELECT ProductName, Price FROM Products
DELETE Products
WHERE Id=2
DELETE Products FROM
(SELECT TOP 2 * FROM Products
WHERE Manufacturer='Apple') AS Selected
WHERE Products.Id = Selected.Id

SELECT ProductName, ProductCount * Price AS TotalSum
FROM Products
ORDER BY TotalSum
SELECT ProductName
FROM Products

SELECT ProductName, Price, Manufacturer
FROM Products
ORDER BY Manufacturer, ProductName

SELECT * FROM Products
WHERE Manufacturer = 'Samsung'

SELECT * FROM Products
WHERE Price * ProductCount > 200000

SELECT * FROM Products
WHERE Manufacturer <> 'Samsung'

SELECT * FROM Products
WHERE Price BETWEEN 20000 AND 40000

	
SELECT * FROM Products
WHERE ProductName LIKE 'iPhone [6-8]%'

UPDATE Products
SET Price = Price + 5000