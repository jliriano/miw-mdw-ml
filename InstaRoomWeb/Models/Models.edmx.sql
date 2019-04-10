
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/10/2019 18:51:28
-- Generated from EDMX file: C:\ProyectoReservasH\miw-mdw-ml\InstaRoomWeb\Models\Models.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_HotelHabitacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Habitaciones] DROP CONSTRAINT [FK_HotelHabitacion];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Hoteles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hoteles];
GO
IF OBJECT_ID(N'[dbo].[Habitaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Habitaciones];
GO

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
    [precio_hr] bigint  NOT NULL,
    [precio_dia] bigint  NOT NULL,
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