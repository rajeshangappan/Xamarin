CREATE TABLE [dbo].[Product]
(
	Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID() PRIMARY KEY, 
    [ProdName] VARCHAR(50) NOT NULL, 
    [Cost] FLOAT NULL, 
    [MaxLimit] INT NULL, 
    [Version] BIGINT NULL, 
    [AvailableQty] INT NULL, 
    [UpdatedAt] DATETIME NULL, 
    [IsDeleted] BIT NULL
)
