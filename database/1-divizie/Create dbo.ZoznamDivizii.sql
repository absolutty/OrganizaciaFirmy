USE [OrganizaciaFirmy]
GO

/****** Object: Table [dbo].[ZoznamDivizii] Script Date: 17. 6. 2021 16:33:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ZoznamDivizii] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Nazov]             NVARCHAR (MAX) NULL,
    [Popis]             NVARCHAR (MAX) NULL,
    [IdVeducehoDivizie] INT            NOT NULL
);


