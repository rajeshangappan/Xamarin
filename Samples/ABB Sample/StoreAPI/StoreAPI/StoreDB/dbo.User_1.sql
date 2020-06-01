CREATE TABLE [dbo].[User]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(), 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Password] CHAR(60) NOT NULL, 
    [Address] NCHAR(10) NULL, 
    [LoggedIn] BIT NOT NULL, 
    [Role] VARCHAR(30) NOT NULL, 
    [EmailAddress] NVARCHAR(30) NULL
)
