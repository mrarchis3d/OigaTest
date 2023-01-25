USE [master]
GO
/****** Object:  Database [template]    Script Date: 25/01/2023 1:10:11 a.�m. ******/
CREATE DATABASE [template]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'template', FILENAME = N'/var/opt/mssql/data/template.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'template_log', FILENAME = N'/var/opt/mssql/data/template_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [template] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [template].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [template] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [template] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [template] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [template] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [template] SET ARITHABORT OFF 
GO
ALTER DATABASE [template] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [template] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [template] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [template] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [template] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [template] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [template] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [template] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [template] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [template] SET  DISABLE_BROKER 
GO
ALTER DATABASE [template] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [template] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [template] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [template] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [template] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [template] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [template] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [template] SET RECOVERY FULL 
GO
ALTER DATABASE [template] SET  MULTI_USER 
GO
ALTER DATABASE [template] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [template] SET DB_CHAINING OFF 
GO
ALTER DATABASE [template] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [template] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [template] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [template] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'template', N'ON'
GO
ALTER DATABASE [template] SET QUERY_STORE = OFF
GO
USE [template]
GO
/****** Object:  FullTextCatalog [FTS_UserCatalog]    Script Date: 25/01/2023 1:10:11 a.�m. ******/
CREATE FULLTEXT CATALOG [FTS_UserCatalog] WITH ACCENT_SENSITIVITY = OFF
AS DEFAULT
GO
/****** Object:  Table [dbo].[User]    Script Date: 25/01/2023 1:10:11 a.�m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [template] SET  READ_WRITE 
GO
USE [master]
GO
/****** Object:  Database [template]    Script Date: 25/01/2023 1:10:11 a.�m. ******/
CREATE DATABASE [template]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'template', FILENAME = N'/var/opt/mssql/data/template.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'template_log', FILENAME = N'/var/opt/mssql/data/template_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [template] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [template].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [template] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [template] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [template] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [template] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [template] SET ARITHABORT OFF 
GO
ALTER DATABASE [template] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [template] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [template] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [template] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [template] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [template] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [template] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [template] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [template] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [template] SET  DISABLE_BROKER 
GO
ALTER DATABASE [template] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [template] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [template] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [template] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [template] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [template] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [template] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [template] SET RECOVERY FULL 
GO
ALTER DATABASE [template] SET  MULTI_USER 
GO
ALTER DATABASE [template] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [template] SET DB_CHAINING OFF 
GO
ALTER DATABASE [template] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [template] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [template] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [template] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'template', N'ON'
GO
ALTER DATABASE [template] SET QUERY_STORE = OFF
GO
USE [template]
GO
/****** Object:  FullTextCatalog [FTS_UserCatalog]    Script Date: 25/01/2023 1:10:11 a.�m. ******/
CREATE FULLTEXT CATALOG [FTS_UserCatalog] WITH ACCENT_SENSITIVITY = OFF
AS DEFAULT
GO
/****** Object:  Table [dbo].[User]    Script Date: 25/01/2023 1:10:11 a.�m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [template] SET  READ_WRITE 
GO