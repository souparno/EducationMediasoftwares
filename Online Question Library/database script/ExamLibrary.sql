USE [master]
GO
/****** Object:  Database [OnlineExamDB]    Script Date: 01/23/2013 01:54:35 ******/
CREATE DATABASE [OnlineExamDB] ON  PRIMARY 
( NAME = N'nathkar_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\nathkar_db.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'nathkar_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\nathkar_db_log.LDF' , SIZE = 768KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OnlineExamDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineExamDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineExamDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [OnlineExamDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [OnlineExamDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [OnlineExamDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [OnlineExamDB] SET ARITHABORT OFF
GO
ALTER DATABASE [OnlineExamDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [OnlineExamDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [OnlineExamDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [OnlineExamDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [OnlineExamDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [OnlineExamDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [OnlineExamDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [OnlineExamDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [OnlineExamDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [OnlineExamDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [OnlineExamDB] SET  ENABLE_BROKER
GO
ALTER DATABASE [OnlineExamDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [OnlineExamDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [OnlineExamDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [OnlineExamDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [OnlineExamDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [OnlineExamDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [OnlineExamDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [OnlineExamDB] SET  READ_WRITE
GO
ALTER DATABASE [OnlineExamDB] SET RECOVERY FULL
GO
ALTER DATABASE [OnlineExamDB] SET  MULTI_USER
GO
ALTER DATABASE [OnlineExamDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [OnlineExamDB] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlineExamDB', N'ON'
GO
USE [OnlineExamDB]
GO
/****** Object:  Table [dbo].[user_master]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_master](
	[user_id] [int] NOT NULL,
	[first_name] [varchar](max) NULL,
	[last_name] [varchar](max) NULL,
	[User_name] [varchar](225) NOT NULL,
 CONSTRAINT [PK_user_master] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_user_master] ON [dbo].[user_master] 
(
	[User_name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subject_master]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[subject_master](
	[Subject_ID] [int] NOT NULL,
	[Subject_Name] [varchar](max) NULL,
 CONSTRAINT [PK_subject_master_1] PRIMARY KEY CLUSTERED 
(
	[Subject_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[file_master]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[file_master](
	[file_id] [int] NOT NULL,
	[file_name] [varchar](225) NOT NULL,
	[price] [numeric](18, 0) NULL,
	[free] [int] NULL,
 CONSTRAINT [PK_file_master_1] PRIMARY KEY CLUSTERED 
(
	[file_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_file_master] UNIQUE NONCLUSTERED 
(
	[file_name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[course_master]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[course_master](
	[Course_ID] [int] NOT NULL,
	[Course_Name] [varchar](max) NULL,
 CONSTRAINT [PK_course_master] PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_role_master]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_role_master](
	[user_id] [int] NOT NULL,
	[user_role] [varchar](max) NULL,
 CONSTRAINT [PK_user_role_master] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[request_subjectMaster]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[request_subjectMaster](
	[user_id] [int] NOT NULL,
	[Subject_id] [int] NOT NULL,
	[request_processed] [int] NULL,
 CONSTRAINT [PK_request_subjectMaster] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[Subject_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[request_CourseMaster]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[request_CourseMaster](
	[user_id] [int] NOT NULL,
	[course_id] [int] NOT NULL,
	[request_processed] [int] NULL,
 CONSTRAINT [PK_request_CourseMaster] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[course_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[password_master]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[password_master](
	[user_id] [int] NOT NULL,
	[user_password] [varchar](max) NULL,
 CONSTRAINT [PK_password_master] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[file_Relation_Master]    Script Date: 01/23/2013 01:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[file_Relation_Master](
	[course_id] [int] NOT NULL,
	[subject_id] [int] NOT NULL,
	[File_ID] [int] NOT NULL,
 CONSTRAINT [PK_file_Relation_Master] PRIMARY KEY CLUSTERED 
(
	[course_id] ASC,
	[subject_id] ASC,
	[File_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_user_role_master_user_master]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[user_role_master]  WITH CHECK ADD  CONSTRAINT [FK_user_role_master_user_master] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_master] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[user_role_master] CHECK CONSTRAINT [FK_user_role_master_user_master]
GO
/****** Object:  ForeignKey [FK_request_subjectMaster_subject_master]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[request_subjectMaster]  WITH CHECK ADD  CONSTRAINT [FK_request_subjectMaster_subject_master] FOREIGN KEY([Subject_id])
REFERENCES [dbo].[subject_master] ([Subject_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[request_subjectMaster] CHECK CONSTRAINT [FK_request_subjectMaster_subject_master]
GO
/****** Object:  ForeignKey [FK_request_subjectMaster_user_master]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[request_subjectMaster]  WITH CHECK ADD  CONSTRAINT [FK_request_subjectMaster_user_master] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_master] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[request_subjectMaster] CHECK CONSTRAINT [FK_request_subjectMaster_user_master]
GO
/****** Object:  ForeignKey [FK_request_CourseMaster_course_master]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[request_CourseMaster]  WITH CHECK ADD  CONSTRAINT [FK_request_CourseMaster_course_master] FOREIGN KEY([course_id])
REFERENCES [dbo].[course_master] ([Course_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[request_CourseMaster] CHECK CONSTRAINT [FK_request_CourseMaster_course_master]
GO
/****** Object:  ForeignKey [FK_request_CourseMaster_user_master]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[request_CourseMaster]  WITH CHECK ADD  CONSTRAINT [FK_request_CourseMaster_user_master] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_master] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[request_CourseMaster] CHECK CONSTRAINT [FK_request_CourseMaster_user_master]
GO
/****** Object:  ForeignKey [FK_password_master_user_master]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[password_master]  WITH CHECK ADD  CONSTRAINT [FK_password_master_user_master] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_master] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[password_master] CHECK CONSTRAINT [FK_password_master_user_master]
GO
/****** Object:  ForeignKey [FK_file_Relation_Master_course_master1]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[file_Relation_Master]  WITH CHECK ADD  CONSTRAINT [FK_file_Relation_Master_course_master1] FOREIGN KEY([course_id])
REFERENCES [dbo].[course_master] ([Course_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[file_Relation_Master] CHECK CONSTRAINT [FK_file_Relation_Master_course_master1]
GO
/****** Object:  ForeignKey [FK_file_Relation_Master_subject_master1]    Script Date: 01/23/2013 01:54:36 ******/
ALTER TABLE [dbo].[file_Relation_Master]  WITH CHECK ADD  CONSTRAINT [FK_file_Relation_Master_subject_master1] FOREIGN KEY([subject_id])
REFERENCES [dbo].[subject_master] ([Subject_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[file_Relation_Master] CHECK CONSTRAINT [FK_file_Relation_Master_subject_master1]
GO
