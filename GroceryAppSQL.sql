-- Create new database called 'GroceriesDb'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'GroceriesDb'
)
CREATE DATABASE GroceriesDb
GO

USE GroceriesDb







-- Create a table called '[Family]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[DBO].[Family]', 'U') IS NOT NULL
DROP TABLE [dbo].Family
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Family]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, -- Primary Key Columnn,
	[Name] NVARCHAR(255) NOT NULL,
	[DateCreated] DATE NOT NULL
)
GO



-- Create a new table called '[User]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[DBO].[User]', 'U') IS NOT NULL
DROP TABLE [dbo].[User]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[User]
(
    [Id] INT IDENTITY NOT NULL PRIMARY KEY, -- Primary Key Columnn
    [FirstName] NVARCHAR(255) NOT NULL,
    [LastName] NVARCHAR(255) NOT NULL,
    [SignUpDate] DATETIME NOT NULL,
	[Uid] NVARCHAR(255) NOT NULL,
	[Email] NVARCHAR(255) NOT NULL,
    [IsActive] BIT NOT NULL,
	[FamilyId] INT NOT NULL
		FOREIGN KEY (FamilyId)
		REFERENCES Family (Id)
)
GO






-- Create a new table called '[GroceryList]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[DBO].[GroceryList]', 'U') IS NOT NULL
DROP TABLE [dbo].[GroceryList]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[GroceryList]
(
    [Id] INT IDENTITY NOT NULL PRIMARY KEY, -- Primary Key Columnn,
    [Name] NVARCHAR(255) NOT NULL,
    [UserId] INT NOT NULL
        FOREIGN KEY (UserId)
        REFERENCES [User] (Id),
    [DateCreated] DATE NOT NULL
)
GO






---- Create a table called '[Recipe]' in schema '[dbo]'
---- Drop the table if it already exists
--IF OBJECT_ID('[DBO].[Recipe]', 'U') IS NOT NULL
--DROP TABLE [dbo].Recipe
--GO
---- Create the table in the specified schema
--CREATE TABLE [dbo].[Recipe]
--(
--    [Id] INT IDENTITY NOT NULL PRIMARY KEY, -- Primary Key Columnn,
--	[Name] NVARCHAR(255) NOT NULL,
--	[Ingredients] NVARCHAR(MAX) NOT NULL,
--	[Instructions] NVARCHAR(MAX) NOT NULL,
--    [UserId] INT NOT NULL,
--        FOREIGN KEY (UserId)
--        REFERENCES [User] (Id),
--    [CookIt] BIT NOT NULL
--)
--GO






-- Create a table called '[GroceryStore]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[DBO].[GroceryStore]', 'U') IS NOT NULL
DROP TABLE [dbo].GroceryStore
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[GroceryStore]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, -- Primary Key Columnn,
	[Name] NVARCHAR(255) NOT NULL
)
GO


-- Create a new table called '[Item]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Item]', 'U') IS NOT NULL
DROP TABLE [dbo].[Item]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].Item
(
    [Id] INT IDENTITY NOT NULL PRIMARY KEY, -- Primary Key column
    [Name] NVARCHAR(255) NOT NULL,
	[GroceryListId] INT NOT NULL
        FOREIGN KEY (GroceryListId)
        REFERENCES GroceryList (Id),
	[GroceryStoreId] INT NOT NULL
		FOREIGN KEY (GroceryStoreId)
		REFERENCES GroceryStore (Id)
);
GO

-- SEED DATA

USE GroceriesDb
GO


INSERT INTO Family (Name, DateCreated)
	VALUES ('Pantana', '2019-12-03')

INSERT INTO [User] (FirstName, LastName, IsActive, FamilyId, [Uid], Email, SignUpDate)
	VALUES ('Josh', 'Pantana', 1, 1, 'lsfjsf8s', 'josh@josh.com', '1900-01-01 00:00:00')

INSERT INTO GroceryList (Name, UserId, DateCreated)
	VALUES ('Our Groceries', 1, '2019-12-03')

INSERT INTO GroceryStore (Name)
	VALUES ('Kroger')

INSERT INTO Item (Name, GroceryListId, GroceryStoreId)
	VALUES ('Milk', 1, 1)

SELECT * from Item
SELECT * from [User]
SELECT * from GroceryStore
SELECT * from GroceryList
SELECT * from Family


