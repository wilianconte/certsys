CREATE DATABASE CertSys
GO

USE CertSys
GO


CREATE TABLE [dbo].[Configuration] 
(
    [Id]	INT NOT NULL IDENTITY,
    [MaxVao]	FLOAT(53) NULL,
    [MaxBaseReforcada]	FLOAT(53) NULL,
    [MinTotal] FLOAT(53) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);