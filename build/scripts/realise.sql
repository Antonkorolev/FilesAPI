IF NOT EXISTS(SELECT [Database_id]
              FROM sys.databases
              WHERE [Name] = 'FileDb')
    BEGIN
        CREATE DATABASE [FileDb]
    END
GO

USE [FileDb]
GO

IF OBJECT_ID(N'dbo.FileInfo', N'U') IS NULL
    BEGIN
        CREATE TABLE [dbo].[FileInfo]
        (
            [FileInfoId] INT          NOT NULL PRIMARY KEY IDENTITY,
            [Code]       NVARCHAR(100) NOT NULL,
            [Name]       NVARCHAR(50) NOT NULL
        )
    END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UQ_FileInfo_Code')
    CREATE UNIQUE INDEX [UQ_FileInfo_Code] ON dbo.FileInfo(Code);
GO

IF OBJECT_ID(N'dbo.FileChangeHistory', N'U') IS NULL
    BEGIN
        CREATE TABLE [dbo].[FileChangeHistory]
        (
            [FileChangeHistoryId] INT          NOT NULL PRIMARY KEY IDENTITY,
            [FileInfoId]          INT          NOT NULL,
            [Created]             DATETIME     NULL,
            [CreatedBy]           NVARCHAR(50) NULL,
            [Modified]            DATETIME     NULL,
            [ModifiedBy]          NVARCHAR(50) NULL,
            FOREIGN KEY (FileInfoId) REFERENCES [FileInfo] (FileInfoId)
        )
    END

GO