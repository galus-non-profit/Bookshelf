CREATE DATABASE [Bookshelf] COLLATE Polish_CI_AS
GO

USE [Bookshelf]

CREATE TABLE [dbo].[Book]
(
    Id INT IDENTITY(1, 1),
    BookId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(4000) NULL,
    Authors NVARCHAR(4000) NULL,
    Publisher NVARCHAR(4000) NULL,
    Isbn VARCHAR(13) NULL,
    CONSTRAINT PK_Book_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT AK_Book_BookId UNIQUE(BookId)   
)