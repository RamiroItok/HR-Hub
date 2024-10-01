USE [master]
GO
/****** Object:  Database [HrHub]    Script Date: 30/9/2024 23:20:50 ******/
CREATE DATABASE [HrHub]
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
/****** Object:  Table [dbo].[Area]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  Table [dbo].[Bitacora]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  Table [dbo].[DigitoVerificador]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  Table [dbo].[FamiliaPatente]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  Table [dbo].[Permiso]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  Table [dbo].[Puesto]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  Table [dbo].[UsuarioPermiso]    Script Date: 30/9/2024 23:20:50 ******/
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
SET IDENTITY_INSERT [dbo].[Area] ON 
GO
INSERT [dbo].[Area] ([Id], [Area]) VALUES (1, N'RRHH')
GO
INSERT [dbo].[Area] ([Id], [Area]) VALUES (2, N'Sistemas')
GO
INSERT [dbo].[Area] ([Id], [Area]) VALUES (3, N'Marketing')
GO
INSERT [dbo].[Area] ([Id], [Area]) VALUES (4, N'Ventas')
GO
INSERT [dbo].[Area] ([Id], [Area]) VALUES (5, N'Operaciones')
GO
INSERT [dbo].[Area] ([Id], [Area]) VALUES (6, N'Gerencia')
GO
INSERT [dbo].[Area] ([Id], [Area]) VALUES (7, N'No Aplica')
GO
SET IDENTITY_INSERT [dbo].[Area] OFF
GO
SET IDENTITY_INSERT [dbo].[Bitacora] ON 
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1056, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T19:38:56.323' AS DateTime), N'MEDIA', 106846)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1057, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T19:40:00.460' AS DateTime), N'MEDIA', 106547)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1058, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T19:41:20.220' AS DateTime), N'MEDIA', 106596)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1059, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T19:46:21.583' AS DateTime), N'MEDIA', 106689)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1060, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:00:13.383' AS DateTime), N'MEDIA', 106501)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1061, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:11:41.877' AS DateTime), N'MEDIA', 106545)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1062, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Recuperación de contraseña', CAST(N'2024-09-15T23:17:41.473' AS DateTime), N'ALTA', 126384)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1063, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:17:56.117' AS DateTime), N'MEDIA', 106742)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1064, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:21:42.363' AS DateTime), N'MEDIA', 106577)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1065, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Cierra sesion', CAST(N'2024-09-15T23:34:26.497' AS DateTime), N'BAJA', 102309)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1066, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Recuperación de contraseña', CAST(N'2024-09-15T23:34:59.937' AS DateTime), N'ALTA', 126528)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1067, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:35:32.643' AS DateTime), N'MEDIA', 106634)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1068, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:41:15.620' AS DateTime), N'MEDIA', 106608)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1069, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:43:20.737' AS DateTime), N'MEDIA', 106565)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1070, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:44:30.033' AS DateTime), N'MEDIA', 106597)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1071, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:55:42.657' AS DateTime), N'MEDIA', 106679)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1072, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:56:23.430' AS DateTime), N'MEDIA', 106678)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1073, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-15T23:56:42.863' AS DateTime), N'MEDIA', 106694)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1074, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T00:15:08.270' AS DateTime), N'MEDIA', 106607)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1075, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T00:16:46.090' AS DateTime), N'MEDIA', 106654)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1076, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T00:19:21.680' AS DateTime), N'MEDIA', 106575)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1077, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T00:23:15.037' AS DateTime), N'MEDIA', 106554)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1078, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T00:32:30.160' AS DateTime), N'MEDIA', 106497)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1079, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Cambio de contraseña', CAST(N'2024-09-16T00:33:03.727' AS DateTime), N'ALTA', 112964)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (1080, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T00:34:17.110' AS DateTime), N'MEDIA', 106619)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2053, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:25:36.430' AS DateTime), N'MEDIA', 106658)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2054, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:30:18.863' AS DateTime), N'MEDIA', 106599)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2055, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:30:46.963' AS DateTime), N'MEDIA', 106614)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2056, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:32:10.973' AS DateTime), N'MEDIA', 106485)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2057, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:39:30.167' AS DateTime), N'MEDIA', 106624)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2058, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:40:55.700' AS DateTime), N'MEDIA', 106627)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2059, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:41:30.793' AS DateTime), N'MEDIA', 106518)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2060, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:42:19.050' AS DateTime), N'MEDIA', 106661)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2061, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:43:42.397' AS DateTime), N'MEDIA', 106601)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2062, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T20:44:44.037' AS DateTime), N'MEDIA', 106652)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2063, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T22:58:01.613' AS DateTime), N'MEDIA', 106628)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2064, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T23:01:46.000' AS DateTime), N'MEDIA', 106623)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2065, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Se realizó una copia de seguridad', CAST(N'2024-09-16T23:02:02.007' AS DateTime), N'ALTA', 146051)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2066, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-16T23:02:50.793' AS DateTime), N'MEDIA', 106547)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (2067, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Se realizó un restore.', CAST(N'2024-09-17T00:54:01.793' AS DateTime), N'ALTA', 116675)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3067, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T15:58:04.067' AS DateTime), N'MEDIA', 106713)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3068, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T15:59:26.060' AS DateTime), N'MEDIA', 106798)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3069, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:00:48.040' AS DateTime), N'MEDIA', 106675)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3070, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:01:53.917' AS DateTime), N'MEDIA', 106617)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3071, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:03:18.927' AS DateTime), N'MEDIA', 106669)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3072, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:11:41.450' AS DateTime), N'MEDIA', 106578)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3073, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:29:10.177' AS DateTime), N'MEDIA', 106643)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3074, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:32:14.063' AS DateTime), N'MEDIA', 106624)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3075, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:33:53.860' AS DateTime), N'MEDIA', 106689)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3076, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-19T16:37:11.770' AS DateTime), N'MEDIA', 106645)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3077, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-20T19:34:39.900' AS DateTime), N'MEDIA', 106797)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3078, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-20T19:36:29.087' AS DateTime), N'MEDIA', 106810)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3079, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-20T19:37:56.827' AS DateTime), N'MEDIA', 106822)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (3080, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-20T19:38:46.957' AS DateTime), N'MEDIA', 106820)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4077, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T19:18:48.433' AS DateTime), N'MEDIA', 106832)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4078, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T19:28:42.580' AS DateTime), N'MEDIA', 106738)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4079, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T19:44:01.603' AS DateTime), N'MEDIA', 106620)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4080, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T20:06:52.443' AS DateTime), N'MEDIA', 106600)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4081, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T20:07:17.883' AS DateTime), N'MEDIA', 106637)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4082, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T20:39:07.783' AS DateTime), N'MEDIA', 106692)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4083, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T20:40:02.983' AS DateTime), N'MEDIA', 106481)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4084, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T20:41:21.330' AS DateTime), N'MEDIA', 106512)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4085, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Cierra sesion', CAST(N'2024-09-22T20:41:34.753' AS DateTime), N'BAJA', 102218)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4086, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-22T20:41:40.183' AS DateTime), N'MEDIA', 106528)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4087, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:17:09.943' AS DateTime), N'MEDIA', 106662)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4088, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:17:36.023' AS DateTime), N'MEDIA', 106659)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4089, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:18:16.400' AS DateTime), N'MEDIA', 106640)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4090, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:18:45.000' AS DateTime), N'MEDIA', 106673)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4091, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:19:03.023' AS DateTime), N'MEDIA', 106584)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4092, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:26:41.650' AS DateTime), N'MEDIA', 106585)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4093, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:31:34.460' AS DateTime), N'MEDIA', 106561)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4094, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:32:23.787' AS DateTime), N'MEDIA', 106541)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4095, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:33:48.470' AS DateTime), N'MEDIA', 106680)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4096, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:36:10.237' AS DateTime), N'MEDIA', 106530)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4097, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:41:00.967' AS DateTime), N'MEDIA', 106452)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4098, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T01:43:09.180' AS DateTime), N'MEDIA', 106644)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4099, N'qO5+kqBvJk0VglIuQENyAA==', N'Lider', N'Registra el usuario Lionel Messi', CAST(N'2024-09-23T01:44:38.140' AS DateTime), N'BAJA', 86016)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4100, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:33:50.093' AS DateTime), N'MEDIA', 106587)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4101, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:36:51.520' AS DateTime), N'MEDIA', 106650)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4102, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:49:44.777' AS DateTime), N'MEDIA', 106746)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4103, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:52:51.510' AS DateTime), N'MEDIA', 106618)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4104, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:53:36.113' AS DateTime), N'MEDIA', 106689)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4105, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:54:01.467' AS DateTime), N'MEDIA', 106563)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4106, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:56:15.240' AS DateTime), N'MEDIA', 106682)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4107, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T22:59:44.820' AS DateTime), N'MEDIA', 106760)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4108, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:00:37.877' AS DateTime), N'MEDIA', 106604)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4109, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:30:43.373' AS DateTime), N'MEDIA', 106591)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4110, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:32:42.653' AS DateTime), N'MEDIA', 106603)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4111, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:33:55.927' AS DateTime), N'MEDIA', 106689)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4112, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:34:43.260' AS DateTime), N'MEDIA', 106651)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4113, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:36:21.417' AS DateTime), N'MEDIA', 106611)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4114, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:38:33.897' AS DateTime), N'MEDIA', 106694)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4115, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:41:17.963' AS DateTime), N'MEDIA', 106641)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4116, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:42:22.303' AS DateTime), N'MEDIA', 106583)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4117, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:42:46.973' AS DateTime), N'MEDIA', 106689)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4118, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:47:30.817' AS DateTime), N'MEDIA', 106639)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4119, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:48:22.787' AS DateTime), N'MEDIA', 106673)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4120, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:51:04.637' AS DateTime), N'MEDIA', 106584)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4121, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Registra el usuario Juan Roman Riquelme', CAST(N'2024-09-23T23:52:08.127' AS DateTime), N'BAJA', 167565)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4122, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Cierra sesion', CAST(N'2024-09-23T23:52:31.187' AS DateTime), N'BAJA', 102231)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4123, N'HceW1FBaeHRHmIDDV3BltBR1hN6zoiAO8b+4FgTD1qE=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-23T23:52:40.707' AS DateTime), N'MEDIA', 101962)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4124, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-24T16:48:46.950' AS DateTime), N'MEDIA', 106806)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4125, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Registra el usuario Paula Higa', CAST(N'2024-09-24T16:49:44.993' AS DateTime), N'BAJA', 135990)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4126, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-24T19:19:50.000' AS DateTime), N'MEDIA', 106724)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4127, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-24T19:22:18.657' AS DateTime), N'MEDIA', 106709)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4128, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Registra el usuario Pepe Lopez', CAST(N'2024-09-24T19:23:01.027' AS DateTime), N'BAJA', 137051)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4129, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-24T19:29:49.773' AS DateTime), N'MEDIA', 106883)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4130, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-27T22:35:19.860' AS DateTime), N'MEDIA', 106719)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4131, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Lider', N'Inicio de sesion', CAST(N'2024-09-29T18:47:47.830' AS DateTime), N'MEDIA', 106843)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4132, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Inicio de sesion', CAST(N'2024-09-29T18:52:33.300' AS DateTime), N'MEDIA', 50739)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4133, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Cierra sesion', CAST(N'2024-09-29T18:52:35.153' AS DateTime), N'BAJA', 46410)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4134, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Inicio de sesion', CAST(N'2024-09-29T18:55:05.297' AS DateTime), N'MEDIA', 50769)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4135, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Cierra sesion', CAST(N'2024-09-29T18:55:08.057' AS DateTime), N'BAJA', 46458)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4136, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T18:55:47.137' AS DateTime), N'MEDIA', 106017)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4137, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T18:58:01.710' AS DateTime), N'MEDIA', 105886)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4138, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Registra el usuario Lucas Lopez', CAST(N'2024-09-29T18:59:19.913' AS DateTime), N'BAJA', 139834)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4139, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Registra el usuario Micaela Gonzalez', CAST(N'2024-09-29T19:00:43.847' AS DateTime), N'BAJA', 156765)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4140, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Cierra sesion', CAST(N'2024-09-29T19:02:15.140' AS DateTime), N'BAJA', 101462)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4141, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:02:20.923' AS DateTime), N'MEDIA', 105754)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4142, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Cierra sesion', CAST(N'2024-09-29T19:02:22.500' AS DateTime), N'BAJA', 101425)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4143, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Inicio de sesion', CAST(N'2024-09-29T19:02:31.660' AS DateTime), N'MEDIA', 50645)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4144, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Cierra sesion', CAST(N'2024-09-29T19:02:32.997' AS DateTime), N'BAJA', 46298)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4145, N'ubtITppYJ7AZXVdnnO+vUuBkhhI56PoKOeGQjN6ufO8=', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-29T19:02:39.327' AS DateTime), N'MEDIA', 110239)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4146, N'ubtITppYJ7AZXVdnnO+vUuBkhhI56PoKOeGQjN6ufO8=', N'Empleado', N'Cierra sesion', CAST(N'2024-09-29T19:02:40.680' AS DateTime), N'BAJA', 105729)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4147, N'HXBE9KrTH1sM7oO29gnbjSscQ4f6cLxdfuS3qJHQjVY=', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-29T19:03:00.293' AS DateTime), N'MEDIA', 113440)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4148, N'HXBE9KrTH1sM7oO29gnbjSscQ4f6cLxdfuS3qJHQjVY=', N'WebMaster', N'Cierra sesion', CAST(N'2024-09-29T19:06:25.610' AS DateTime), N'BAJA', 109244)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4149, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:21:59.573' AS DateTime), N'MEDIA', 105980)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4150, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:23:21.863' AS DateTime), N'MEDIA', 105815)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4151, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:26:20.377' AS DateTime), N'MEDIA', 105842)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4152, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:27:09.973' AS DateTime), N'MEDIA', 105985)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4153, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:32:11.140' AS DateTime), N'MEDIA', 105797)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4154, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Cierra sesion', CAST(N'2024-09-29T19:33:00.620' AS DateTime), N'BAJA', 101412)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4155, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:33:21.740' AS DateTime), N'MEDIA', 105829)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4156, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:37:29.220' AS DateTime), N'MEDIA', 106033)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4157, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Cierra sesion', CAST(N'2024-09-29T19:37:54.853' AS DateTime), N'BAJA', 101629)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4158, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'RRHH', N'Inicio de sesion', CAST(N'2024-09-29T19:40:02.990' AS DateTime), N'MEDIA', 105782)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4159, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T19:49:35.247' AS DateTime), N'MEDIA', 115107)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4160, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T19:50:59.520' AS DateTime), N'MEDIA', 115092)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4161, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T19:53:15.280' AS DateTime), N'MEDIA', 114997)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4162, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T19:53:49.150' AS DateTime), N'MEDIA', 115120)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4163, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T19:56:18.790' AS DateTime), N'MEDIA', 115096)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4164, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T20:12:55.447' AS DateTime), N'MEDIA', 114897)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4165, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-29T20:13:11.210' AS DateTime), N'BAJA', 110407)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4167, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T20:14:46.267' AS DateTime), N'MEDIA', 114928)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4168, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-29T20:15:23.010' AS DateTime), N'BAJA', 110490)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4170, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T20:17:16.073' AS DateTime), N'MEDIA', 114922)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4171, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-29T20:17:19.750' AS DateTime), N'BAJA', 110611)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4172, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T20:17:27.030' AS DateTime), N'MEDIA', 114957)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4173, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-29T20:17:31.870' AS DateTime), N'BAJA', 110501)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4174, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Intento de login fallido', CAST(N'2024-09-29T20:17:47.887' AS DateTime), N'ALTA', 130396)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4175, N'ubtITppYJ7AZXVdnnO+vUuBkhhI56PoKOeGQjN6ufO8=', N'Empleado', N'Intento de login fallido', CAST(N'2024-09-29T20:19:01.660' AS DateTime), N'ALTA', 125471)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4176, N'ubtITppYJ7AZXVdnnO+vUuBkhhI56PoKOeGQjN6ufO8=', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-29T20:19:10.247' AS DateTime), N'MEDIA', 110065)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4177, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-29T20:23:02.507' AS DateTime), N'MEDIA', 114787)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4178, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:07:56.633' AS DateTime), N'MEDIA', 114949)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4179, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:09:12.143' AS DateTime), N'MEDIA', 114839)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4180, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Intento de login fallido', CAST(N'2024-09-30T01:10:29.710' AS DateTime), N'ALTA', 130266)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4181, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:10:33.330' AS DateTime), N'MEDIA', 114770)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4182, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Intento de login fallido', CAST(N'2024-09-30T01:11:16.290' AS DateTime), N'ALTA', 130210)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4183, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:11:20.430' AS DateTime), N'MEDIA', 114714)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4184, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:14:48.443' AS DateTime), N'MEDIA', 114937)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4185, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:17:02.337' AS DateTime), N'MEDIA', 114806)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4186, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:20:09.043' AS DateTime), N'MEDIA', 114841)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4187, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:21:40.700' AS DateTime), N'MEDIA', 114762)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4188, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:24:36.290' AS DateTime), N'MEDIA', 114898)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4189, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:25:49.407' AS DateTime), N'MEDIA', 114984)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4190, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:28:25.477' AS DateTime), N'MEDIA', 114923)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4191, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-30T01:28:48.957' AS DateTime), N'BAJA', 110646)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4192, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Inicio de sesion', CAST(N'2024-09-30T01:28:55.647' AS DateTime), N'MEDIA', 50745)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4193, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Intento de login fallido', CAST(N'2024-09-30T01:30:37.793' AS DateTime), N'ALTA', 130275)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4194, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:30:42.113' AS DateTime), N'MEDIA', 114797)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4195, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:32:21.940' AS DateTime), N'MEDIA', 114775)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4196, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:32:52.063' AS DateTime), N'MEDIA', 114844)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4197, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:33:10.450' AS DateTime), N'MEDIA', 114755)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4198, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:33:27.773' AS DateTime), N'MEDIA', 114898)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4199, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:34:05.303' AS DateTime), N'MEDIA', 114843)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4200, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:34:55.663' AS DateTime), N'MEDIA', 114928)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4201, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:36:11.097' AS DateTime), N'MEDIA', 114818)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4202, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T01:36:38.217' AS DateTime), N'MEDIA', 114978)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4203, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T19:47:30.440' AS DateTime), N'MEDIA', 114970)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4204, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Intento de login fallido', CAST(N'2024-09-30T19:54:05.653' AS DateTime), N'ALTA', 130383)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4205, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T19:54:09.427' AS DateTime), N'MEDIA', 115050)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4206, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T19:56:10.757' AS DateTime), N'MEDIA', 114935)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4207, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T19:58:38.737' AS DateTime), N'MEDIA', 115143)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4208, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:00:16.423' AS DateTime), N'MEDIA', 114786)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4209, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:08:18.947' AS DateTime), N'MEDIA', 114942)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4210, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:09:56.507' AS DateTime), N'MEDIA', 114989)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4211, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:16:46.177' AS DateTime), N'MEDIA', 114941)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4212, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:17:56.067' AS DateTime), N'MEDIA', 114973)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4213, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:18:40.510' AS DateTime), N'MEDIA', 114863)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4214, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:19:10.297' AS DateTime), N'MEDIA', 114827)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4215, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:20:16.243' AS DateTime), N'MEDIA', 114814)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4216, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:20:45.847' AS DateTime), N'MEDIA', 114847)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4217, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:23:30.060' AS DateTime), N'MEDIA', 114785)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4218, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:23:58.743' AS DateTime), N'MEDIA', 114963)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4219, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:24:58.783' AS DateTime), N'MEDIA', 114978)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4220, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:28:20.027' AS DateTime), N'MEDIA', 114843)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4221, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:31:30.587' AS DateTime), N'MEDIA', 114769)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4222, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:32:34.857' AS DateTime), N'MEDIA', 114856)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4223, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:33:58.240' AS DateTime), N'MEDIA', 114977)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4224, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:34:55.993' AS DateTime), N'MEDIA', 114938)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4225, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:36:25.770' AS DateTime), N'MEDIA', 114917)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4226, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:46:23.620' AS DateTime), N'MEDIA', 114895)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4227, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Intento de login fallido', CAST(N'2024-09-30T20:50:15.453' AS DateTime), N'ALTA', 130243)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4228, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:50:20.343' AS DateTime), N'MEDIA', 114765)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4229, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:53:10.330' AS DateTime), N'MEDIA', 114793)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4230, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-30T20:53:34.620' AS DateTime), N'BAJA', 110534)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4231, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T20:54:19.480' AS DateTime), N'MEDIA', 114970)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4232, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:01:24.087' AS DateTime), N'MEDIA', 114794)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4233, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:01:36.900' AS DateTime), N'MEDIA', 114847)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4234, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:01:55.180' AS DateTime), N'MEDIA', 114863)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4235, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:02:12.737' AS DateTime), N'MEDIA', 114756)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4236, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:02:26.570' AS DateTime), N'MEDIA', 114845)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4237, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:03:04.217' AS DateTime), N'MEDIA', 114790)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4238, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-30T21:23:53.750' AS DateTime), N'BAJA', 110520)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4239, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Intento de login fallido', CAST(N'2024-09-30T21:24:27.497' AS DateTime), N'ALTA', 130326)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4240, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:24:42.500' AS DateTime), N'MEDIA', 114865)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4241, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T21:34:10.710' AS DateTime), N'MEDIA', 114792)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4242, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T23:16:05.297' AS DateTime), N'MEDIA', 114891)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4243, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T23:16:49.763' AS DateTime), N'MEDIA', 115031)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4244, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Inicio de sesion', CAST(N'2024-09-30T23:18:36.800' AS DateTime), N'MEDIA', 114990)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4245, N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'Administrador', N'Cierra sesion', CAST(N'2024-09-30T23:18:55.740' AS DateTime), N'BAJA', 110641)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4246, N'HXBE9KrTH1sM7oO29gnbjSscQ4f6cLxdfuS3qJHQjVY=', N'WebMaster', N'Inicio de sesion', CAST(N'2024-09-30T23:19:03.243' AS DateTime), N'MEDIA', 113520)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4247, N'HXBE9KrTH1sM7oO29gnbjSscQ4f6cLxdfuS3qJHQjVY=', N'WebMaster', N'Cierra sesion', CAST(N'2024-09-30T23:19:08.787' AS DateTime), N'BAJA', 109245)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4248, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Inicio de sesion', CAST(N'2024-09-30T23:19:14.740' AS DateTime), N'MEDIA', 50706)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4249, N'HJPuZNOoTVpC3ZdGs6Kxow==', N'Lider', N'Cierra sesion', CAST(N'2024-09-30T23:19:25.420' AS DateTime), N'BAJA', 46376)
GO
INSERT [dbo].[Bitacora] ([Id], [Email], [TipoUsuario], [Descripcion], [Fecha], [Criticidad], [DVH]) VALUES (4250, N'ubtITppYJ7AZXVdnnO+vUuBkhhI56PoKOeGQjN6ufO8=', N'Empleado', N'Inicio de sesion', CAST(N'2024-09-30T23:19:38.787' AS DateTime), N'MEDIA', 110262)
GO
SET IDENTITY_INSERT [dbo].[Bitacora] OFF
GO
SET IDENTITY_INSERT [dbo].[DigitoVerificador] ON 
GO
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (1, N'Bitacora', 24609779)
GO
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (2, N'FamiliaPatente', 0)
GO
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (3, N'Permiso', 0)
GO
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (4, N'Puesto', 0)
GO
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (5, N'Usuario', 684075)
GO
INSERT [dbo].[DigitoVerificador] ([IdTabla], [NombreTabla], [DV]) VALUES (6, N'UsuarioPermiso', 0)
GO
SET IDENTITY_INSERT [dbo].[DigitoVerificador] OFF
GO
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (4, 3, 0)
GO
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (1, 1002, 0)
GO
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (2, 3, 0)
GO
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (2, 1002, 0)
GO
INSERT [dbo].[FamiliaPatente] ([PadreId], [HijoId], [DVH]) VALUES (4, 1002, 0)
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 
GO
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (1, N'Administrador', NULL, 0)
GO
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (2, N'Empleado', NULL, 0)
GO
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (3, N'Gestiion de usuarios', N'Gestion_Usuarios', 0)
GO
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (4, N'WebMaster', NULL, 0)
GO
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (5, N'Lider', NULL, 0)
GO
INSERT [dbo].[Permiso] ([Id], [Nombre], [Permiso], [DVH]) VALUES (1002, N'Bitacora', N'Bitacora', 0)
GO
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
SET IDENTITY_INSERT [dbo].[Puesto] ON 
GO
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (1, N'Empleado', 0)
GO
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (3, N'Lider', 0)
GO
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (4, N'WebMaster', 0)
GO
INSERT [dbo].[Puesto] ([Id], [Puesto], [DVH]) VALUES (5, N'Administrador', 0)
GO
SET IDENTITY_INSERT [dbo].[Puesto] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Contraseña], [IdPuesto], [IdArea], [FechaNacimiento], [Genero], [FechaIngreso], [Estado], [DVH]) VALUES (3, N'PLWMS3BORXad5WYDBd/X0A==', N'1DV3mcelx9U3GaWS15bnzw==', N'HJPuZNOoTVpC3ZdGs6Kxow==', N'AC97B476B8292DABA174B1FAF5C0AB7E', 3, 2, CAST(N'1996-09-13' AS Date), N'Masculino', CAST(N'2024-09-05' AS Date), 0, 126003)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Contraseña], [IdPuesto], [IdArea], [FechaNacimiento], [Genero], [FechaIngreso], [Estado], [DVH]) VALUES (5, N'qO5+kqBvJk0VglIuQENyAA==', N'FpIlz41KyvtcmpjBB/UvpA==', N'f+HF7IyKwezyazMl8zL8341h3mP1bWL9Py59j+Enwgg=', N'AC97B476B8292DABA174B1FAF5C0AB7E', 5, 2, CAST(N'1994-06-03' AS Date), N'Masculino', CAST(N'2024-09-22' AS Date), 0, 185121)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Contraseña], [IdPuesto], [IdArea], [FechaNacimiento], [Genero], [FechaIngreso], [Estado], [DVH]) VALUES (10, N'69KOrqVXHOSEuoy2Wc7mNA==', N'KwLbY01FMs33RHLrH4cTig==', N'ubtITppYJ7AZXVdnnO+vUuBkhhI56PoKOeGQjN6ufO8=', N'AC97B476B8292DABA174B1FAF5C0AB7E', 1, 1, CAST(N'2000-05-04' AS Date), N'Masculino', CAST(N'2024-09-29' AS Date), 0, 184266)
GO
INSERT [dbo].[Usuario] ([Id], [Nombre], [Apellido], [Email], [Contraseña], [IdPuesto], [IdArea], [FechaNacimiento], [Genero], [FechaIngreso], [Estado], [DVH]) VALUES (11, N'CnW9f+n8a242EZqhG4JIqg==', N'9vYEdDvaswxngkpKwSqPjw==', N'HXBE9KrTH1sM7oO29gnbjSscQ4f6cLxdfuS3qJHQjVY=', N'AC97B476B8292DABA174B1FAF5C0AB7E', 4, 3, CAST(N'2000-09-01' AS Date), N'Femenino', CAST(N'2024-09-29' AS Date), 0, 188685)
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[UsuarioPermiso] ON 
GO
INSERT [dbo].[UsuarioPermiso] ([Id_UsuarioPermiso], [UsuarioId], [PatenteId], [DVH]) VALUES (1, 1, 1, 0)
GO
INSERT [dbo].[UsuarioPermiso] ([Id_UsuarioPermiso], [UsuarioId], [PatenteId], [DVH]) VALUES (2, 2, 2, 0)
GO
SET IDENTITY_INSERT [dbo].[UsuarioPermiso] OFF
GO
ALTER TABLE [dbo].[FamiliaPatente] ADD  DEFAULT (NULL) FOR [DVH]
GO
ALTER TABLE [dbo].[Permiso] ADD  DEFAULT ((0)) FOR [DVH]
GO
ALTER TABLE [dbo].[Puesto] ADD  DEFAULT ((0)) FOR [DVH]
GO
/****** Object:  StoredProcedure [dbo].[sp_d_familiaPatente]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_d_usuarioPermiso]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_bitacora]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_familiaPatente]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_permiso]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_usuario]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_i_usuarioPermiso]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ObtenerPermisosNoAsignados]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RealizarBackup]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_area]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_bitacora]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_familia_validacion]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_familiaPatente]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_familiaPatente]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_notNull]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_permiso_usuarioPermiso]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_puesto]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_UsuarioEmail]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_usuarioPermiso_permiso]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_usuarios]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_s_validarUsuarioContraseña]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_u_DesbloquearUsuario]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_u_EstadoUsuarioBloqueo]    Script Date: 30/9/2024 23:20:50 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_u_usuarioContraseña]    Script Date: 30/9/2024 23:20:50 ******/
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
