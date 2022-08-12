CREATE TABLE [dbo].[Keluhan] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Keluhanmu] NVARCHAR (MAX) NULL,
    [Solusimu]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Keluhan] PRIMARY KEY CLUSTERED ([Id] ASC)
);

