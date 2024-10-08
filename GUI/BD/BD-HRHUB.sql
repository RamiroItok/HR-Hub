USE [master]
GO
/****** Object:  Database [HrHub]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  Table [dbo].[Bitacora]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  Table [dbo].[DigitoVerificador]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  Table [dbo].[FamiliaPatente]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  Table [dbo].[Permiso]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  Table [dbo].[Puesto]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/9/2024 00:52:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Contraseña] [varchar](200) NULL,
	[IdPuesto] [int] NULL,
	[Area] [varchar](50) NULL,
	[FechaIngreso] [datetime] NULL,
	[DVH] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioPermiso]    Script Date: 10/9/2024 00:52:48 ******/
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
SET IDENTITY_INSERT [dbo].[Bitacora] ON 

INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-08-24T19:18:09.127' AS DateTime), N'MEDIA', 56779)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T18:43:12.463' AS DateTime), N'MEDIA', 55719)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T18:54:00.477' AS DateTime), N'MEDIA', 55696)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T19:08:51.320' AS DateTime), N'MEDIA', 55795)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (5, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T19:14:19.997' AS DateTime), N'MEDIA', 55824)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (6, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T19:23:16.893' AS DateTime), N'MEDIA', 55772)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (7, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T19:23:37.240' AS DateTime), N'MEDIA', 55821)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (8, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T19:24:49.677' AS DateTime), N'MEDIA', 55885)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (9, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T19:27:30.230' AS DateTime), N'MEDIA', 55758)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (10, N'micaelahiga@gmail.com', N'Lider', N'Inicio de sesion', CAST(N'2024-09-02T19:28:11.057' AS DateTime), N'MEDIA', 46272)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (11, N'ramiro.itokazu@gmail.com', N'4', N'Inicio de sesion', CAST(N'2024-09-02T19:29:35.810' AS DateTime), N'MEDIA', 52193)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (12, N'ramiro.itokazu@gmail.com', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-02T19:30:20.960' AS DateTime), N'MEDIA', 55657)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (13, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T19:30:54.323' AS DateTime), N'MEDIA', 56755)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (14, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T19:31:51.187' AS DateTime), N'MEDIA', 56718)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (15, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T19:44:40.107' AS DateTime), N'MEDIA', 56740)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (16, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T19:44:58.727' AS DateTime), N'MEDIA', 56892)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (17, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T19:45:34.643' AS DateTime), N'MEDIA', 56806)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (18, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T19:46:12.533' AS DateTime), N'MEDIA', 56754)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (19, N'micaelahiga@gmail.com', N'Lider', N'Inicio de sesion', CAST(N'2024-09-02T19:48:39.593' AS DateTime), N'MEDIA', 46466)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (20, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T19:49:12.730' AS DateTime), N'MEDIA', 56796)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (21, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-02T19:49:20.780' AS DateTime), N'MEDIA', 54586)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (22, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T20:04:23.733' AS DateTime), N'MEDIA', 56618)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (23, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T20:04:52.743' AS DateTime), N'MEDIA', 56649)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (24, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T20:07:21.763' AS DateTime), N'MEDIA', 56626)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (25, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T20:07:27.713' AS DateTime), N'MEDIA', 56728)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (26, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T20:59:14.297' AS DateTime), N'MEDIA', 56754)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (27, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-02T20:59:39.203' AS DateTime), N'MEDIA', 54679)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (28, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T21:02:21.170' AS DateTime), N'MEDIA', 56567)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (29, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T21:07:47.190' AS DateTime), N'MEDIA', 56771)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (30, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:04:23.773' AS DateTime), N'MEDIA', 56651)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (31, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:04:41.203' AS DateTime), N'MEDIA', 56649)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (32, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:04:58.673' AS DateTime), N'MEDIA', 56784)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (33, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:07:25.483' AS DateTime), N'MEDIA', 56727)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (34, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:07:54.977' AS DateTime), N'MEDIA', 56758)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (35, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:09:14.800' AS DateTime), N'MEDIA', 56722)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (36, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:09:56.480' AS DateTime), N'MEDIA', 56820)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (37, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:19:10.300' AS DateTime), N'MEDIA', 56667)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (38, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:20:18.700' AS DateTime), N'MEDIA', 56690)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (39, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:20:48.090' AS DateTime), N'MEDIA', 56738)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (40, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:21:45.377' AS DateTime), N'MEDIA', 56701)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (41, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:24:57.410' AS DateTime), N'MEDIA', 56793)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (42, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:26:02.180' AS DateTime), N'MEDIA', 56656)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (43, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-02T23:28:13.813' AS DateTime), N'MEDIA', 54525)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (44, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:32:26.610' AS DateTime), N'MEDIA', 56713)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (45, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:33:13.890' AS DateTime), N'MEDIA', 56660)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (46, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-02T23:34:38.803' AS DateTime), N'MEDIA', 56791)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (47, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:35:04.753' AS DateTime), N'MEDIA', 56639)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (48, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:35:13.637' AS DateTime), N'MEDIA', 56638)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (49, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:41:07.390' AS DateTime), N'MEDIA', 56647)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (50, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:41:44.020' AS DateTime), N'MEDIA', 56660)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (51, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:42:13.023' AS DateTime), N'MEDIA', 56609)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (52, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:44:36.890' AS DateTime), N'MEDIA', 56720)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (53, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:46:19.767' AS DateTime), N'MEDIA', 56767)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (54, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:49:54.790' AS DateTime), N'MEDIA', 56788)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (55, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:55:52.457' AS DateTime), N'MEDIA', 56711)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (56, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T00:56:19.870' AS DateTime), N'MEDIA', 56780)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (57, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T01:06:22.310' AS DateTime), N'MEDIA', 56623)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (58, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T01:07:31.393' AS DateTime), N'MEDIA', 56636)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (59, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T01:08:09.180' AS DateTime), N'MEDIA', 56738)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (60, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T01:09:57.580' AS DateTime), N'MEDIA', 56798)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (61, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T01:14:06.353' AS DateTime), N'MEDIA', 56644)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (62, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-05T01:14:23.687' AS DateTime), N'MEDIA', 56625)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (63, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-06T14:15:34.603' AS DateTime), N'MEDIA', 56716)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (64, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-06T14:17:07.943' AS DateTime), N'MEDIA', 54555)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (65, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-06T14:18:09.810' AS DateTime), N'MEDIA', 56795)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (66, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-06T14:19:29.077' AS DateTime), N'MEDIA', 54649)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (67, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-06T14:24:36.347' AS DateTime), N'MEDIA', 56749)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (68, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-06T15:26:40.687' AS DateTime), N'MEDIA', 56702)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (69, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-06T15:32:14.963' AS DateTime), N'MEDIA', 56679)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (70, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-06T15:32:52.357' AS DateTime), N'MEDIA', 54517)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (71, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-06T17:04:56.857' AS DateTime), N'MEDIA', 56788)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (72, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:19:34.123' AS DateTime), N'MEDIA', 56807)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (73, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:20:01.820' AS DateTime), N'MEDIA', 56595)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (74, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:20:29.490' AS DateTime), N'MEDIA', 56763)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (75, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:20:44.680' AS DateTime), N'MEDIA', 56710)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (76, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T17:27:26.487' AS DateTime), N'MEDIA', 54618)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (77, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:31:32.973' AS DateTime), N'MEDIA', 56687)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (78, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:32:49.640' AS DateTime), N'MEDIA', 56836)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (79, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:37:48.167' AS DateTime), N'MEDIA', 56889)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (80, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T17:46:46.497' AS DateTime), N'MEDIA', 56854)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (81, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T17:49:16.763' AS DateTime), N'MEDIA', 54656)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (82, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T17:50:27.227' AS DateTime), N'MEDIA', 54576)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (83, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T18:01:36.527' AS DateTime), N'MEDIA', 54535)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (84, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:18:53.367' AS DateTime), N'MEDIA', 56830)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (85, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:30:39.007' AS DateTime), N'MEDIA', 56814)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (86, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:37:03.653' AS DateTime), N'MEDIA', 56762)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (87, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:38:06.330' AS DateTime), N'MEDIA', 56827)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (88, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:43:25.930' AS DateTime), N'MEDIA', 56785)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (89, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:43:52.340' AS DateTime), N'MEDIA', 56782)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (90, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:45:16.777' AS DateTime), N'MEDIA', 56814)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (91, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:45:40.630' AS DateTime), N'MEDIA', 56760)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (92, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:47:24.033' AS DateTime), N'MEDIA', 56824)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (93, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:47:58.053' AS DateTime), N'MEDIA', 56940)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (94, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T19:48:11.387' AS DateTime), N'MEDIA', 56771)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (95, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:02:48.207' AS DateTime), N'MEDIA', 54521)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (96, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:08:45.873' AS DateTime), N'MEDIA', 56746)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (97, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:12:20.173' AS DateTime), N'MEDIA', 56558)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (98, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:13:27.500' AS DateTime), N'MEDIA', 56691)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (99, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:14:16.413' AS DateTime), N'MEDIA', 56672)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (100, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:20:19.077' AS DateTime), N'MEDIA', 56680)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (101, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:21:17.703' AS DateTime), N'MEDIA', 56660)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (102, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:26:30.507' AS DateTime), N'MEDIA', 54451)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (103, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:29:20.957' AS DateTime), N'MEDIA', 54477)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (104, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:31:00.173' AS DateTime), N'MEDIA', 54346)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (105, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:31:33.377' AS DateTime), N'MEDIA', 56637)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (106, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:34:07.930' AS DateTime), N'MEDIA', 56699)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (107, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:35:50.923' AS DateTime), N'MEDIA', 54482)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (108, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:39:09.757' AS DateTime), N'MEDIA', 54611)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (109, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:40:40.883' AS DateTime), N'MEDIA', 54409)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (110, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:43:07.793' AS DateTime), N'MEDIA', 54506)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (111, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:43:42.913' AS DateTime), N'MEDIA', 56677)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (112, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:45:02.230' AS DateTime), N'MEDIA', 56641)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (113, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:48:17.780' AS DateTime), N'MEDIA', 54592)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (114, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:50:43.487' AS DateTime), N'MEDIA', 56665)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (115, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:54:58.980' AS DateTime), N'MEDIA', 54630)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (116, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T20:55:22.410' AS DateTime), N'MEDIA', 56686)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (117, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:55:38.857' AS DateTime), N'MEDIA', 54612)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (118, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T20:58:23.900' AS DateTime), N'MEDIA', 54553)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (119, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:02:10.710' AS DateTime), N'MEDIA', 54348)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (120, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:05:59.810' AS DateTime), N'MEDIA', 54607)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (121, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T21:08:44.993' AS DateTime), N'MEDIA', 56740)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (122, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:09:07.343' AS DateTime), N'MEDIA', 54549)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (123, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:11:56.590' AS DateTime), N'MEDIA', 54513)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (124, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:13:50.813' AS DateTime), N'MEDIA', 54439)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (125, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:16:03.110' AS DateTime), N'MEDIA', 54452)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (126, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:16:20.450' AS DateTime), N'MEDIA', 54433)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (127, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:19:44.753' AS DateTime), N'MEDIA', 54575)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (128, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:22:30.730' AS DateTime), N'MEDIA', 54406)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (129, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T21:25:12.350' AS DateTime), N'MEDIA', 54450)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (130, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T22:25:24.697' AS DateTime), N'MEDIA', 56703)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (131, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T22:27:31.023' AS DateTime), N'MEDIA', 54504)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (132, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T22:28:04.460' AS DateTime), N'MEDIA', 54521)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (133, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T22:29:21.157' AS DateTime), N'MEDIA', 54516)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (134, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T22:31:15.113' AS DateTime), N'MEDIA', 54469)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (135, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T22:41:15.583' AS DateTime), N'MEDIA', 54482)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (136, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T22:46:26.160' AS DateTime), N'MEDIA', 54585)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (137, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-08T23:36:04.440' AS DateTime), N'MEDIA', 54517)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (138, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T23:36:54.690' AS DateTime), N'MEDIA', 56789)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (139, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T23:40:55.830' AS DateTime), N'MEDIA', 56735)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (140, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-08T23:43:52.237' AS DateTime), N'MEDIA', 56726)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (141, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-09T00:50:12.167' AS DateTime), N'MEDIA', 54389)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (142, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T00:54:28.747' AS DateTime), N'MEDIA', 56755)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (143, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Da de alta un usuario', CAST(N'2024-09-09T00:55:04.587' AS DateTime), N'BAJA', 65569)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (144, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T00:56:36.650' AS DateTime), N'MEDIA', 56765)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (145, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Cierra sesion', CAST(N'2024-09-09T00:56:37.547' AS DateTime), N'MEDIA', 52775)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (146, N'liomessi10@gmail.com', N'Lider', N'Inicio de sesion', CAST(N'2024-09-09T00:57:28.843' AS DateTime), N'MEDIA', 43517)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (147, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T00:58:48.733' AS DateTime), N'MEDIA', 56843)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (148, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T01:00:43.373' AS DateTime), N'MEDIA', 56592)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (149, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-09T01:02:37.857' AS DateTime), N'MEDIA', 54480)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (150, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T01:03:25.870' AS DateTime), N'MEDIA', 56636)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (151, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T01:04:15.367' AS DateTime), N'MEDIA', 56634)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (152, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T01:05:00.647' AS DateTime), N'MEDIA', 56547)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (153, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T01:06:14.923' AS DateTime), N'MEDIA', 56645)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (154, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T01:08:04.230' AS DateTime), N'MEDIA', 56657)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (155, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Cierra sesion', CAST(N'2024-09-09T01:08:16.727' AS DateTime), N'BAJA', 52342)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1002, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:16:44.437' AS DateTime), N'MEDIA', 56737)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1003, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:19:37.943' AS DateTime), N'MEDIA', 56814)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1004, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:20:05.507' AS DateTime), N'MEDIA', 56619)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1005, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:24:29.080' AS DateTime), N'MEDIA', 56775)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1006, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:26:04.607' AS DateTime), N'MEDIA', 56686)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1007, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:28:43.533' AS DateTime), N'MEDIA', 56761)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1008, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:29:30.537' AS DateTime), N'MEDIA', 56708)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1009, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:30:34.783' AS DateTime), N'MEDIA', 56663)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1010, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:31:18.600' AS DateTime), N'MEDIA', 56713)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1011, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:32:25.703' AS DateTime), N'MEDIA', 56692)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1012, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:32:55.297' AS DateTime), N'MEDIA', 56740)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1013, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:35:29.597' AS DateTime), N'MEDIA', 56802)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1014, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:47:31.377' AS DateTime), N'MEDIA', 56723)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1015, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:49:25.413' AS DateTime), N'MEDIA', 56803)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1016, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:52:51.473' AS DateTime), N'MEDIA', 56698)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1017, N'micaelahiga@gmail.com', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-09T22:54:28.433' AS DateTime), N'MEDIA', 54605)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1018, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T22:59:45.187' AS DateTime), N'MEDIA', 56848)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1019, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T23:03:58.060' AS DateTime), N'MEDIA', 56777)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1020, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T23:07:08.970' AS DateTime), N'MEDIA', 56753)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1021, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T23:22:23.177' AS DateTime), N'MEDIA', 56656)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1022, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-09T23:23:33.273' AS DateTime), N'MEDIA', 56686)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1023, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:15:00.223' AS DateTime), N'MEDIA', 57430)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1024, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:31:43.220' AS DateTime), N'MEDIA', 57520)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1025, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Cierra sesion', CAST(N'2024-09-10T00:32:27.547' AS DateTime), N'BAJA', 53208)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1026, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:32:33.933' AS DateTime), N'MEDIA', 57518)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1027, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:37:21.897' AS DateTime), N'MEDIA', 57540)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1028, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:38:12.097' AS DateTime), N'MEDIA', 57556)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1029, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:41:05.193' AS DateTime), N'MEDIA', 57502)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1030, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:49:25.757' AS DateTime), N'MEDIA', 57656)
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1031, N'ramiro.itokazu@gmail.com', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-10T00:49:47.560' AS DateTime), N'MEDIA', 57726)
SET IDENTITY_INSERT [dbo].[Bitacora] OFF
GO
SET IDENTITY_INSERT [dbo].[DigitoVerificador] ON 

INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (1, N'Bitacora', 10358051)
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (2, N'FamiliaPatente', 0)
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (3, N'Permiso', 0)
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (4, N'Puesto', 0)
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (5, N'Usuario', 0)
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (6, N'UsuarioPermiso', 0)
SET IDENTITY_INSERT [dbo].[DigitoVerificador] OFF
GO
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (4, 3, 0)
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (1, 1002, 0)
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (2, 3, 0)
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (2, 1002, 0)
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (4, 1002, 0)
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (1, N'Administrador', NULL, 0)
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (2, N'Empleado', NULL, 0)
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (3, N'Gestiion de usuarios', N'Gestion_Usuarios', 0)
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (4, N'Prueba', NULL, 0)
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (5, N'Prueba 2', NULL, 0)
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (1002, N'Bitacora', N'Bitacora', 0)
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
SET IDENTITY_INSERT [dbo].[Puesto] ON 

INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (1, N'Empleado', 0)
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (2, N'RRHH', 0)
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (3, N'Lider', 0)
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (4, N'WebMaster', 0)
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (5, N'Administrador', 0)
SET IDENTITY_INSERT [dbo].[Puesto] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Contraseña], [IdPuesto], [Area], [FechaIngreso], [DVH]) VALUES (1, N'Ramiro', N'Itokazu', N'ramiro.itokazu@gmail.com', N'123', 4, N'Sistemas', CAST(N'2024-08-16T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Contraseña], [IdPuesto], [Area], [FechaIngreso], [DVH]) VALUES (2, N'Micaela', N'Higa', N'micaelahiga@gmail.com', N'123', 5, N'Operaciones', CAST(N'2024-08-16T00:27:28.797' AS DateTime), 0)
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Contraseña], [IdPuesto], [Area], [FechaIngreso], [DVH]) VALUES (1005, N'Lionel', N'Messi', N'liomessi10@gmail.com', N'123456.M', 3, N'Gerencia', CAST(N'2024-09-09T00:54:52.833' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[UsuarioPermiso] ON 

INSERT [dbo].[UsuarioPermiso] ([Id_UsuarioPermiso], [UsuarioId], [PatenteId], [DVH]) VALUES (1, 1, 1, 0)
INSERT [dbo].[UsuarioPermiso] ([Id_UsuarioPermiso], [UsuarioId], [PatenteId], [DVH]) VALUES (2, 2, 2, 0)
SET IDENTITY_INSERT [dbo].[UsuarioPermiso] OFF
GO
ALTER TABLE [dbo].[FamiliaPatente] ADD  DEFAULT (NULL) FOR [DVH]
GO
ALTER TABLE [dbo].[Permiso] ADD  DEFAULT ((0)) FOR [DVH]
GO
ALTER TABLE [dbo].[Puesto] ADD  DEFAULT ((0)) FOR [DVH]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [DVH]
GO
/****** Object:  StoredProcedure [dbo].[sp_d_familiaPatente]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_d_usuarioPermiso]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_bitacora]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_familiaPatente]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_permiso]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_usuario]    Script Date: 10/9/2024 00:52:48 ******/
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
	@Area NVARCHAR(50),
	@FechaIngreso datetime
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO Usuario (Nombre, Apellido, Email, Contraseña, IdPuesto, Area, FechaIngreso)
    VALUES (@Nombre, @Apellido, @Email, @Contraseña, @IdPuesto, @Area, @FechaIngreso);

    SELECT SCOPE_IDENTITY() AS Id
END

GO
/****** Object:  StoredProcedure [dbo].[sp_i_usuarioPermiso]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ObtenerPermisosNoAsignados]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_bitacora]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_bitacoraCriticidad]    Script Date: 10/9/2024 00:52:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_bitacoraCriticidad]
@criticidad nvarchar(10)
AS
BEGIN
	
	SELECT TOP 50 * FROM Bitacora
	WHERE Criticidad = @criticidad
	ORDER BY 1 DESC

END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_bitacoraFecha]    Script Date: 10/9/2024 00:52:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_bitacoraFecha]
    @fechaDesde DATETIME = NULL,
    @fechaHasta DATETIME = NULL
AS
BEGIN
    SELECT TOP 50 *
    FROM Bitacora
    WHERE (Fecha >= @fechaDesde OR @fechaDesde IS NULL)
      AND (Fecha <= @fechaHasta OR @fechaHasta IS NULL)
    ORDER BY Fecha DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_bitacoraTipoUsuario]    Script Date: 10/9/2024 00:52:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_bitacoraTipoUsuario]
@tipoUsuario nvarchar(20)
AS
BEGIN
	
	SELECT TOP 50 * FROM Bitacora
	WHERE TipoUsuario = @tipoUsuario
	ORDER BY 1 DESC

END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_bitacoraUsuario]    Script Date: 10/9/2024 00:52:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_s_bitacoraUsuario]
@idUsuario int
AS
BEGIN
	
	SELECT TOP 50 * FROM Bitacora bit
	INNER JOIN Usuario usu ON usu.Email = bit.Email
	WHERE usu.Id = @idUsuario
	ORDER BY 1 DESC

END
GO
/****** Object:  StoredProcedure [dbo].[sp_s_familia_validacion]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_familiaPatente]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_familiaPatente]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_notNull]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_usuarioPermiso]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_puesto]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_usuarioPermiso_permiso]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_usuarios]    Script Date: 10/9/2024 00:52:48 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_validarUsuarioContraseña]    Script Date: 10/9/2024 00:52:48 ******/
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
USE [master]
GO
ALTER DATABASE [HrHub] SET  READ_WRITE 
GO
