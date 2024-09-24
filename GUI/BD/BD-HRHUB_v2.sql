USE [master]
GO
/****** Object:  Database [HrHub]    Script Date: 24/9/2024 19:24:59 ******/
CREATE DATABASE [HrHub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HrHub', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HrHub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HrHub_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HrHub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HrHub] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HrHub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HrHub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HrHub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HrHub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HrHub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HrHub] SET ARITHABORT OFF 
GO
ALTER DATABASE [HrHub] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HrHub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HrHub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HrHub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HrHub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HrHub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HrHub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HrHub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HrHub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HrHub] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HrHub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HrHub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HrHub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HrHub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HrHub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HrHub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HrHub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HrHub] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HrHub] SET  MULTI_USER 
GO
ALTER DATABASE [HrHub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HrHub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HrHub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HrHub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HrHub] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HrHub] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HrHub] SET QUERY_STORE = OFF
GO
USE [HrHub]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Area] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[TipoUsuario] [nvarchar](20) NOT NULL,
	[Descripcion] [nvarchar](200) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Criticidad] [nvarchar](50) NOT NULL,
	[DVH] [int] NOT NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DigitoVerificador]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DigitoVerificador](
	[IdTabla] [int] IDENTITY(1,1) NOT NULL,
	[NombreTabla] [nvarchar](50) NOT NULL,
	[DV] [int] NOT NULL,
 CONSTRAINT [PK_DigitoVerificador] PRIMARY KEY CLUSTERED 
(
	[IdTabla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FamiliaPatente]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamiliaPatente](
	[PadreId] [int] NOT NULL,
	[HijoId] [int] NOT NULL,
	[DVH] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Permiso] [nvarchar](100) NULL,
	[DVH] [int] NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puesto]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puesto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Puesto] [nvarchar](50) NOT NULL,
	[DVH] [int] NOT NULL,
 CONSTRAINT [PK_Puesto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Contraseña] [nvarchar](100) NOT NULL,
	[IdPuesto] [int] NOT NULL,
	[IdArea] [int] NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Genero] [nvarchar](50) NOT NULL,
	[FechaIngreso] [date] NOT NULL,
	[Estado] [int] NOT NULL,
	[DVH] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_v2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioPermiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioPermiso](
	[Id_UsuarioPermiso] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[PatenteId] [int] NOT NULL,
	[DVH] [int] NULL,
 CONSTRAINT [PK_UsuarioPermiso] PRIMARY KEY CLUSTERED 
(
	[Id_UsuarioPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FamiliaPatente] ADD  DEFAULT (NULL) FOR [DVH]
GO
ALTER TABLE [dbo].[Permiso] ADD  DEFAULT ((0)) FOR [DVH]
GO
ALTER TABLE [dbo].[Puesto] ADD  DEFAULT ((0)) FOR [DVH]
GO
/****** Object:  StoredProcedure [dbo].[sp_d_familiaPatente]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_d_familiaPatente]
    @parPadreId int, 
    @parHijoId int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM FamiliaPatente WHERE PadreId = @parPadreId and HijoId = @parHijoId
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_d_usuarioPermiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_d_usuarioPermiso]
    @parUsuarioId int
AS
BEGIN
    SET NOCOUNT ON

    DELETE FROM UsuarioPermiso WHERE UsuarioId = @parUsuarioId
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_i_bitacora]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_i_bitacora]
    @Email NVARCHAR(100),
    @TipoUsuario NVARCHAR(20),
    @Descripcion NVARCHAR(255),
    @Fecha DATETIME,
    @Criticidad NVARCHAR(10),
    @DVH INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Bitacora (Email, TipoUsuario, Descripcion, Fecha, Criticidad, DVH)
    VALUES (@Email, @TipoUsuario, @Descripcion, @Fecha, @Criticidad, @DVH);

    SELECT SCOPE_IDENTITY() AS Id
END

GO
/****** Object:  StoredProcedure [dbo].[sp_i_familiaPatente]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_i_familiaPatente]
    @parPadreId int, 
    @parHijoId int
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO FamiliaPatente (PadreId, HijoId) VALUES (@parPadreId, @parHijoId)
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_i_permiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_i_permiso]
    @parNombre nvarchar(100), 
    @parPermiso nvarchar(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Permiso (Nombre, Permiso)
    OUTPUT inserted.Id
    VALUES (@parNombre, @parPermiso);
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_i_usuario]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_i_usuario]
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @Email NVARCHAR(100),
    @Contraseña NVARCHAR(255),
    @IdPuesto INT,
	@IdArea INT,
	@FechaNacimiento datetime,
	@Genero NVARCHAR(30),
	@FechaIngreso datetime,
	@Estado int
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO Usuario (Nombre, Apellido, Email, Contraseña, IdPuesto, IdArea, FechaNacimiento, Genero, FechaIngreso, Estado, DVH)
    VALUES (@Nombre, @Apellido, @Email, @Contraseña, @IdPuesto, @IdArea, @FechaNacimiento, @Genero, @FechaIngreso, @Estado, 0);

    SELECT SCOPE_IDENTITY() AS Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_i_usuarioPermiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_i_usuarioPermiso]
    @parUsuarioId int,
	@parPatenteId int,
	@parDVH int
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO UsuarioPermiso (UsuarioId, PatenteId, DVH) VALUES (@parUsuarioId, @parPatenteId, @parDVH)
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerPermisosNoAsignados]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerPermisosNoAsignados]
    @FamiliaId INT
AS
BEGIN
    SELECT 
        p.Id, 
        p.Nombre, 
        p.Permiso
    FROM 
        Permiso p
    WHERE 
        p.Permiso IS NOT NULL
        AND p.Id NOT IN (
            SELECT fp.HijoId 
            FROM FamiliaPatente fp 
            WHERE fp.PadreId = @FamiliaId
        )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RealizarBackup]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RealizarBackup]
    @ruta NVARCHAR(255),
    @nombre NVARCHAR(255)
AS
BEGIN
    DECLARE @backupCommand NVARCHAR(MAX);
    
    SET @backupCommand = 'BACKUP DATABASE [HrHub] TO DISK = ''' + @ruta + @nombre + '.bak'' WITH FORMAT, MEDIANAME = ''SQLServerBackups'', NAME = ''Full Backup of HrHub''';
    
    EXEC sp_executesql @backupCommand;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_area]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_area]
AS
BEGIN
	SELECT * FROM Area ORDER BY 1 ASC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_bitacora]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_bitacora]
AS
BEGIN
	
	SELECT * FROM Bitacora ORDER BY 1 DESC

END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_familia_validacion]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_familia_validacion]
    @familiaId int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.Id, p.Nombre 
	FROM FamiliaPatente fm 
		INNER JOIN Permiso p ON p.Id = fm.PadreId 
		INNER JOIN Permiso p2 ON p2.Id = fm.HijoId 
	WHERE fm.HijoId = @familiaId
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_familiaPatente]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_familiaPatente]
    @idFamilia int
AS
BEGIN
    SET NOCOUNT ON;

    WITH RECURSIVO AS 
    (
        SELECT fp.PadreId, fp.HijoId 
        FROM FamiliaPatente fp 
        WHERE fp.PadreId = @idFamilia
        UNION ALL 
        SELECT fp2.PadreId, fp2.HijoId 
        FROM FamiliaPatente fp2 
        INNER JOIN RECURSIVO r ON r.HijoId = fp2.PadreId
    )
    SELECT r.PadreId, r.HijoId, p.Id as PermisoId, p.Nombre, p.Permiso 
    FROM RECURSIVO r 
    INNER JOIN Permiso p ON r.HijoId = p.Id;

    -- Si estás seguro de que no hay ciclos, puedes usar la siguiente opción
    -- OPTION (MAXRECURSION 0);
    
END

GO
/****** Object:  StoredProcedure [dbo].[sp_s_permiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_permiso]
AS
BEGIN
    SET NOCOUNT ON

    SELECT * FROM Permiso WHERE Permiso IS NULL
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_familiaPatente]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_permiso_familiaPatente]
    @idFamilia int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id as PermisoId, Nombre, Permiso, PadreId, HijoId 
	FROM Permiso p 
	INNER JOIN FamiliaPatente fm on fm.HijoId = p.Id 
	WHERE PadreId = @idFamilia ORDER BY Permiso DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_notNull]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_permiso_notNull]
AS
BEGIN
    SET NOCOUNT ON

    SELECT * FROM Permiso WHERE Permiso IS NOT NULL
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_usuarioPermiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_permiso_usuarioPermiso]
    @idUsuario int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
		Id as PermisoId, 
		Nombre,	
		Permiso 
	FROM Permiso p 
	INNER JOIN UsuarioPermiso up on up.PatenteId = p.Id 
	WHERE up.UsuarioId = @idUsuario
	ORDER BY Permiso DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_puesto]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_puesto]
AS
BEGIN
    SELECT * FROM Puesto
END

GO
/****** Object:  StoredProcedure [dbo].[sp_s_UsuarioEmail]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_UsuarioEmail]
@Email nvarchar(50)
AS
BEGIN
	SELECT * FROM Usuario 
	WHERE Email = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_usuarioPermiso_permiso]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_usuarioPermiso_permiso]
    @idUsuario int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.* FROM UsuarioPermiso up INNER JOIN Permiso p on p.Id = up.PatenteId WHERE UsuarioId = @idUsuario
END

GO
/****** Object:  StoredProcedure [dbo].[sp_s_usuarios]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_usuarios]
AS
BEGIN
    SELECT * FROM Usuario order by 1 asc
END

GO
/****** Object:  StoredProcedure [dbo].[sp_s_validarUsuarioContraseña]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_validarUsuarioContraseña]
@email nvarchar(100),
@contraseña nvarchar(100)
AS
BEGIN
    SELECT * FROM Usuario WHERE Email = @email and Contraseña = @contraseña
END

GO
/****** Object:  StoredProcedure [dbo].[sp_u_DesbloquearUsuario]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_u_DesbloquearUsuario]
@Email nvarchar(50)
AS
BEGIN
	UPDATE Usuario 
	SET Estado = 0
	WHERE Email = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[sp_u_EstadoUsuarioBloqueo]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_u_EstadoUsuarioBloqueo]
@Email nvarchar(50)
AS
BEGIN
	UPDATE Usuario 
	SET Estado = Estado + 1 
	WHERE Email = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[sp_u_usuarioContraseña]    Script Date: 24/9/2024 19:24:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_u_usuarioContraseña]
@email nvarchar(50),
@contraseña nvarchar(100)
AS
BEGIN
    UPDATE Usuario
    SET Contraseña = @contraseña
    WHERE Email = @email

    SELECT Id FROM Usuario WHERE Email = @email
END

GO
USE [master]
GO
ALTER DATABASE [HrHub] SET  READ_WRITE 
GO
