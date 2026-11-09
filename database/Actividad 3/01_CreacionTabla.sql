IF DB_ID(N'HPA_Parcial3_BCrypt') IS NULL
BEGIN
    CREATE DATABASE HPA_Parcial3_BCrypt;
END;
GO

USE HPA_Parcial3_BCrypt;
GO

IF OBJECT_ID(N'dbo.Usuarios', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuarios
    (
        IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
        NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
        NombreCompleto NVARCHAR(100) NOT NULL,
        HashContrasena VARCHAR(60) NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.IntentosAcceso', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.IntentosAcceso
    (
        IdIntento INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NULL,
        UsuarioIngresado NVARCHAR(50) NOT NULL,
        Exitoso BIT NOT NULL,
        Fecha DATETIME2(0) NOT NULL
            CONSTRAINT DF_IntentosAcceso_Fecha DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_IntentosAcceso_Usuarios
            FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuarios(IdUsuario)
    );
END;
GO