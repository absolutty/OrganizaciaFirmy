USE [OrganizaciaFirmy]
GO

/****** Object: Table [dbo].[ZoznamOddeleni] Script Date: 17. 6. 2021 16:33:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ZoznamOddeleni] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Nazov]               NVARCHAR (MAX) NOT NULL,
    [IdVeducehoOddelenia] INT            NOT NULL,
    [IdProjektu]          INT            NOT NULL
);


