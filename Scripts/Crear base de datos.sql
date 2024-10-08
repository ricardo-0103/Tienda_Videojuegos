USE [master]
GO
/****** Object:  Database [TiendaVideojuegos]    Script Date: 22/11/2023 10:33:14 p. m. ******/
CREATE DATABASE [TiendaVideojuegos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TiendaVideojuegos', FILENAME = N'C:\SQLData\TiendaVideojuegos.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TiendaVideojuegos_log', FILENAME = N'C:\SQLData\Logs\TiendaVideojuegos_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TiendaVideojuegos] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TiendaVideojuegos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TiendaVideojuegos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET ARITHABORT OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TiendaVideojuegos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TiendaVideojuegos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TiendaVideojuegos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TiendaVideojuegos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TiendaVideojuegos] SET  MULTI_USER 
GO
ALTER DATABASE [TiendaVideojuegos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TiendaVideojuegos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TiendaVideojuegos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TiendaVideojuegos] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TiendaVideojuegos] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TiendaVideojuegos] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TiendaVideojuegos] SET QUERY_STORE = ON
GO
ALTER DATABASE [TiendaVideojuegos] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TiendaVideojuegos]
GO
/****** Object:  Table [dbo].[Alquiler]    Script Date: 22/11/2023 10:33:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alquiler](
	[idAlquiler] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[idJuego] [int] NOT NULL,
	[fecha_ini] [date] NOT NULL,
	[fecha_dev] [date] NOT NULL,
	[precio_juego] [int] NULL,
	[precio_total] [int] NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Alquiler] PRIMARY KEY CLUSTERED 
(
	[idAlquiler] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 22/11/2023 10:33:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[fecha_nacimiento] [date] NOT NULL,
	[cedula] [varchar](50) NOT NULL,
	[telefono] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Precio_videojuego]    Script Date: 22/11/2023 10:33:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Precio_videojuego](
	[idPrecio] [int] IDENTITY(1,1) NOT NULL,
	[idJuego] [int] NOT NULL,
	[precio_dia] [int] NOT NULL,
 CONSTRAINT [PK_Precio_videojuego] PRIMARY KEY CLUSTERED 
(
	[idPrecio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Videojuego]    Script Date: 22/11/2023 10:33:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videojuego](
	[idJuego] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[año] [date] NOT NULL,
	[protagonistas] [varchar](50) NULL,
	[director] [varchar](50) NULL,
	[productor] [varchar](50) NULL,
	[plataforma] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Videojuego] PRIMARY KEY CLUSTERED 
(
	[idJuego] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alquiler]  WITH CHECK ADD  CONSTRAINT [FK_Alquiler_Cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Cliente] ([idCliente])
GO
ALTER TABLE [dbo].[Alquiler] CHECK CONSTRAINT [FK_Alquiler_Cliente]
GO
ALTER TABLE [dbo].[Alquiler]  WITH CHECK ADD  CONSTRAINT [FK_Alquiler_Videojuego] FOREIGN KEY([idJuego])
REFERENCES [dbo].[Videojuego] ([idJuego])
GO
ALTER TABLE [dbo].[Alquiler] CHECK CONSTRAINT [FK_Alquiler_Videojuego]
GO
ALTER TABLE [dbo].[Precio_videojuego]  WITH CHECK ADD  CONSTRAINT [FK_Precio_videojuego_Videojuego] FOREIGN KEY([idJuego])
REFERENCES [dbo].[Videojuego] ([idJuego])
GO
ALTER TABLE [dbo].[Precio_videojuego] CHECK CONSTRAINT [FK_Precio_videojuego_Videojuego]
GO
USE [master]
GO
ALTER DATABASE [TiendaVideojuegos] SET  READ_WRITE 
GO
