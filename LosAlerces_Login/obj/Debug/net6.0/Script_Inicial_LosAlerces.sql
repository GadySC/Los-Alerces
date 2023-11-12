IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categorias] (
    [ID_Categoria] int NOT NULL IDENTITY,
    [Nombre_Categoria] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([ID_Categoria])
);
GO

CREATE TABLE [Clientes] (
    [ID_Cliente] int NOT NULL IDENTITY,
    [Nombre_Empresa] nvarchar(255) NOT NULL,
    [Direccion] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([ID_Cliente])
);
GO

CREATE TABLE [Personal] (
    [ID_Personal] int NOT NULL IDENTITY,
    [Nombre] nvarchar(255) NOT NULL,
    [Apellido] nvarchar(255) NOT NULL,
    [Cargo] nvarchar(255) NOT NULL,
    [Salario] DECIMAL(10,2) NOT NULL,
    [Email] nvarchar(255) NOT NULL,
    [Direccion] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Personal] PRIMARY KEY ([ID_Personal])
);
GO

CREATE TABLE [Productos] (
    [ID_Productos] int NOT NULL IDENTITY,
    [Nombre_Producto] nvarchar(255) NOT NULL,
    [Descripcion] nvarchar(255) NOT NULL,
    [Precio] DECIMAL(10,2) NOT NULL,
    [Stock] int NOT NULL,
    CONSTRAINT [PK_Productos] PRIMARY KEY ([ID_Productos])
);
GO

CREATE TABLE [Roles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuarios] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Contactos] (
    [ID_Contactos] int NOT NULL IDENTITY,
    [ID_Cliente] int NOT NULL,
    [Nombre] nvarchar(255) NOT NULL,
    [Apellido] nvarchar(255) NOT NULL,
    [Email] nvarchar(255) NOT NULL,
    [Telefono] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Contactos] PRIMARY KEY ([ID_Contactos]),
    CONSTRAINT [FK_Contactos_Clientes_ID_Cliente] FOREIGN KEY ([ID_Cliente]) REFERENCES [Clientes] ([ID_Cliente]) ON DELETE CASCADE
);
GO

CREATE TABLE [Cotizaciones] (
    [ID_Cotizacion] int NOT NULL IDENTITY,
    [ID_Cliente] int NOT NULL,
    [ID_Producto] int NOT NULL,
    [ID_Personal] int NOT NULL,
    [Fecha_cotizacion] DATE NOT NULL,
    [Cantidad] int NOT NULL,
    [Precio_Unitario] DECIMAL(10,2) NOT NULL,
    [Total] DECIMAL(10,2) NOT NULL,
    CONSTRAINT [PK_Cotizaciones] PRIMARY KEY ([ID_Cotizacion]),
    CONSTRAINT [FK_Cotizaciones_Clientes_ID_Cliente] FOREIGN KEY ([ID_Cliente]) REFERENCES [Clientes] ([ID_Cliente]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cotizaciones_Personal_ID_Personal] FOREIGN KEY ([ID_Personal]) REFERENCES [Personal] ([ID_Personal]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cotizaciones_Productos_ID_Producto] FOREIGN KEY ([ID_Producto]) REFERENCES [Productos] ([ID_Productos]) ON DELETE CASCADE
);
GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Contactos_ID_Cliente] ON [Contactos] ([ID_Cliente]);
GO

CREATE INDEX [IX_Cotizaciones_ID_Cliente] ON [Cotizaciones] ([ID_Cliente]);
GO

CREATE INDEX [IX_Cotizaciones_ID_Personal] ON [Cotizaciones] ([ID_Personal]);
GO

CREATE INDEX [IX_Cotizaciones_ID_Producto] ON [Cotizaciones] ([ID_Producto]);
GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
GO

CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Usuarios] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Usuarios] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231112021400_InitialCreate', N'6.0.24');
GO

COMMIT;
GO

