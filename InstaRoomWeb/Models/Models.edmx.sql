
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/08/2019 11:15:16
-- Generated from EDMX file: D:\Proyectos Master\Proyectos_MDW\miw-mdw-ml\InstaRoomWeb\Models\Models.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Hoteles];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Hoteles'
CREATE TABLE [dbo].[Hoteles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre_hotel] nvarchar(max)  NOT NULL,
    [nombre_director] nvarchar(max)  NOT NULL,
    [direccion_postal] nvarchar(max)  NOT NULL,
    [imagen_hotel] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Habitaciones'
CREATE TABLE [dbo].[Habitaciones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [tipo_habitacion] nvarchar(max)  NOT NULL,
    [servicios] nvarchar(max)  NOT NULL,
    [precio_hr] nvarchar(max)  NOT NULL,
    [precio_dia] nvarchar(max)  NOT NULL,
    [HotelId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Hoteles'
ALTER TABLE [dbo].[Hoteles]
ADD CONSTRAINT [PK_Hoteles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Habitaciones'
ALTER TABLE [dbo].[Habitaciones]
ADD CONSTRAINT [PK_Habitaciones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [HotelId] in table 'Habitaciones'
ALTER TABLE [dbo].[Habitaciones]
ADD CONSTRAINT [FK_HotelHabitacion]
    FOREIGN KEY ([HotelId])
    REFERENCES [dbo].[Hoteles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HotelHabitacion'
CREATE INDEX [IX_FK_HotelHabitacion]
ON [dbo].[Habitaciones]
    ([HotelId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------