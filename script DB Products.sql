


--Create database TestProducts


use TestProducts


CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NameProduct VARCHAR(100) NOT NULL UNIQUE,
    DescriptionProduct NVARCHAR(500),
    Price DECIMAL(18,2) NOT NULL CHECK (Price > 0),
    DiscountPrice DECIMAL(18,2) NULL CHECK (DiscountPrice IS NULL OR DiscountPrice > 0),
    ImageUrl NVARCHAR(255)
);


-- SP

CREATE PROCEDURE SP_GetAllProducts
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, NameProduct, DescriptionProduct, Price, DiscountPrice, ImageUrl FROM Products;
END
