CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryId] INT NULL, 
    [Name] NVARCHAR(30) NOT NULL, 
    [Cost] DECIMAL NOT NULL, 
    [About] NVARCHAR(MAX) NULL 
	FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE
)
