USE [OrganizaciaFirmy]
GO

/****** Object: Table [dbo].[ZoznamProjektov] Script Date: 17. 6. 2021 16:33:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ZoznamProjektov] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Nazov]              NVARCHAR (MAX) NOT NULL,
    [Zameranie]          NVARCHAR (MAX) NULL,
    [IdVeducehoProjektu] INT            NULL,
    [IdDivizie]          INT            NULL
);


