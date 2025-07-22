CREATE DATABASE CINE_DB_FIRST;
GO
USE CINE_DB_FIRST;
GO

--CREAR LA TABLA DE PELICULA
CREATE TABLE Pelicula (
    Id_Pelicula INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(200) NOT NULL,
    Duracion INT NOT NULL, -- en minutos
    Imagen NVARCHAR(MAX) NULL, -- Columna para la URL o Base64 de la imagen
    Activo BIT NOT NULL DEFAULT 1 -- Para borrado lógico
);

-- Tabla Sala_Cine
CREATE TABLE Sala_Cine (
    Id_Sala INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Estado INT, -- Podría ser 1: Activa, 2: Mantenimiento, etc.
    Tipo NVARCHAR(50) NOT NULL, -- 'General' o 'VIP'
    Activo BIT NOT NULL DEFAULT 1 -- Para borrado lógico
);

-- Tabla de Unión: Pelicula_SalaCine
CREATE TABLE Pelicula_SalaCine (
    Id_Pelicula_Sala INT PRIMARY KEY IDENTITY(1,1),
    Id_Pelicula INT NOT NULL,
    Id_Sala_Cine INT NOT NULL,
    Fecha_Publicacion DATE NOT NULL,
    Fecha_Fin DATE NOT NULL,
    CONSTRAINT FK_Pelicula_SalaCine_Pelicula FOREIGN KEY (Id_Pelicula) REFERENCES Pelicula(Id_Pelicula),
    CONSTRAINT FK_Pelicula_SalaCine_SalaCine FOREIGN KEY (Id_Sala_Cine) REFERENCES Sala_Cine(Id_Sala)
);

GO
-- Stored Procedure para verificar la disponibilidad de la sala
CREATE OR ALTER PROCEDURE sp_VerificarDisponibilidadSala
    @NombreSala NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ConteoPeliculas INT;
    DECLARE @Resultado NVARCHAR(200);
    DECLARE @IdSala INT;

    -- Buscar el ID de la sala y asegurarse de que esté activa
    SELECT @IdSala = Id_Sala FROM Sala_Cine WHERE Nombre = @NombreSala AND Activo = 1;

    IF @IdSala IS NULL
    BEGIN
        -- Si la sala no existe o no está activa, devolvemos un mensaje de error
        SELECT 'Sala no encontrada o inactiva' AS Mensaje;
        RETURN;
    END

    -- Contar cuántas películas están activas en esa sala para el día de hoy
    SELECT @ConteoPeliculas = COUNT(*)
    FROM Pelicula_SalaCine psc
    JOIN Pelicula p ON psc.Id_Pelicula = p.Id_Pelicula
    WHERE psc.Id_Sala_Cine = @IdSala
      AND p.Activo = 1
      AND GETDATE() BETWEEN psc.Fecha_Publicacion AND psc.Fecha_Fin;

    -- Lógica de negocio
    IF @ConteoPeliculas < 3
        SET @Resultado = 'Sala disponible';
    ELSE IF @ConteoPeliculas BETWEEN 3 AND 5
        SET @Resultado = 'Sala con ' + CAST(@ConteoPeliculas AS NVARCHAR(10)) + ' películas asignadas';
    ELSE
        SET @Resultado = 'Sala no disponible';

    -- Devolver el resultado
    SELECT @Resultado AS Mensaje;
END
GO