-- Create new database called 'GroceriesDb'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'GroceriesDb2'
)
CREATE DATABASE GroceriesDb2
GO








USE GroceriesDb2
-- Create a table called '[Family]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[DBO].[Family]', 'U') IS NOT NULL
DROP TABLE [dbo].Family
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Family]
(
	[Id] NVARCHAR(68) NOT NULL PRIMARY KEY, -- Primary Key Columnn,
	[Name] NVARCHAR(255) NOT NULL,
	[DateCreated] DATE NOT NULL,
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
	[PhotoURL] NVARCHAR(1000),
    [IsActive] BIT NOT NULL,
	[FamilyId] NVARCHAR(68) NOT NULL
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
	[FamilyId] NVARCHAR(68) NOT NULL
        FOREIGN KEY (FamilyId)
        REFERENCES [Family] (Id),
    [DateCreated] DATE NOT NULL
)
GO








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
	[Category] NVARCHAR(255),
	[GroceryListId] INT NOT NULL
        FOREIGN KEY (GroceryListId)
        REFERENCES GroceryList (Id),
	[GroceryStoreId] INT NOT NULL
		FOREIGN KEY (GroceryStoreId)
		REFERENCES GroceryStore (Id)
);
GO



-- Create a table called '[Invite]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[DBO].[Message]', 'U') IS NOT NULL
DROP TABLE [dbo].Invitation
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Invitation]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, -- Primary Key Columnn,
	[FamilyId] NVARCHAR(68) NOT NULL
        FOREIGN KEY (FamilyId)
        REFERENCES Family (Id),
	[FromId] INT NOT NULL
		FOREIGN KEY (FromId)
		REFERENCES [User] (Id),
	[ToId] INT NOT NULL
		FOREIGN KEY (ToId)
		REFERENCES [User] (Id),
	[DateCreated] DATE NOT NULL
)
GO


-- SEED DATA

USE GroceriesDb2
GO


INSERT INTO Family (Name, DateCreated)
	VALUES ('Pantana', '2019-12-03')

INSERT INTO [User] (FirstName, LastName, IsActive, FamilyId, [Uid], Email, SignUpDate)
	VALUES ('Josh', 'Pantana', 1, 1, 'lsfjsf8s', 'josh@josh.com', '1900-01-01 00:00:00')

INSERT INTO GroceryList (Name, FamilyId, DateCreated)
	VALUES ('Our Groceries', 1, '2019-12-03')

INSERT INTO GroceryStore (Name)
	VALUES ('Default Grocery')

INSERT INTO Item (Name, GroceryListId, GroceryStoreId)
	VALUES ('Milk', 1, 1)



DELETE FROM [User] 
DELETE FROM Family 
DELETE FROM Item
DELETE FROM GroceryList
