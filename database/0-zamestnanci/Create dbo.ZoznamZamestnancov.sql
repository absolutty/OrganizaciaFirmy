USE [OrganizaciaFirmy]
GO

/****** Object: Table [dbo].[ZoznamZamestnancov] Script Date: 17. 6. 2021 16:33:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ZoznamZamestnancov] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Titul]        NVARCHAR (MAX) NULL,
    [Meno]         NVARCHAR (MAX) NOT NULL,
    [Priezvisko]   NVARCHAR (MAX) NOT NULL,
    [Telefon]      NVARCHAR (MAX) NOT NULL,
    [Email]        NVARCHAR (MAX) NULL,
    [jeRiaditelom] BIT            NOT NULL,
    [DiviziaId]    INT            NULL,
    [ProjektId]    INT            NULL,
    [OddelenieID]  INT            NULL
);


