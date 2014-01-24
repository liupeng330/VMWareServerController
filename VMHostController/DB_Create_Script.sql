USE [ReportingSite]
GO
/****** Object:  Table [dbo].[VMHostInfo]    Script Date: 2014/1/24 22:50:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VMHostInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IPAddress] [nvarchar](50) NOT NULL,
	[URL] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VMInfo]    Script Date: 2014/1/24 22:50:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VMInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IPAddress] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[LastBootTime] [datetime2](7) NOT NULL,
	[HostId] [int] NOT NULL,
	[MacAddress] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[VMInfo]  WITH CHECK ADD  CONSTRAINT [FK_VMInfo_VMHostInfo] FOREIGN KEY([HostId])
REFERENCES [dbo].[VMHostInfo] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VMInfo] CHECK CONSTRAINT [FK_VMInfo_VMHostInfo]
GO
