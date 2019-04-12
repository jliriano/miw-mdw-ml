
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2019 19:01:46
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
IF OBJECT_ID(N'[dbo].[FK_ReservaAspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservas] DROP CONSTRAINT [FK_ReservaAspNetUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservaHabitacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservas] DROP CONSTRAINT [FK_ReservaHabitacion];
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
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Reservas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservas];
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
    [precio_hr] float  NOT NULL,
    [precio_dia] float  NOT NULL,
    [HotelId] int  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Reservas'
CREATE TABLE [dbo].[Reservas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [check_in] datetime  NOT NULL,
    [check_out] datetime  NOT NULL,
    [precio_hr] float  NOT NULL,
    [precio_dia] float  NOT NULL,
    [precio_total] float  NOT NULL,
    [AspNetUser_Id] nvarchar(128)  NOT NULL,
    [Habitacion_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reservas'
ALTER TABLE [dbo].[Reservas]
ADD CONSTRAINT [PK_Reservas]
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

-- Creating foreign key on [AspNetUser_Id] in table 'Reservas'
ALTER TABLE [dbo].[Reservas]
ADD CONSTRAINT [FK_ReservaAspNetUser]
    FOREIGN KEY ([AspNetUser_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservaAspNetUser'
CREATE INDEX [IX_FK_ReservaAspNetUser]
ON [dbo].[Reservas]
    ([AspNetUser_Id]);
GO

-- Creating foreign key on [Habitacion_Id] in table 'Reservas'
ALTER TABLE [dbo].[Reservas]
ADD CONSTRAINT [FK_ReservaHabitacion]
    FOREIGN KEY ([Habitacion_Id])
    REFERENCES [dbo].[Habitaciones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservaHabitacion'
CREATE INDEX [IX_FK_ReservaHabitacion]
ON [dbo].[Reservas]
    ([Habitacion_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------