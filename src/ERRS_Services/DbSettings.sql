USE [DBSettings]
GO
/****** Object:  Table [dbo].[UserSettingss]    Script Date: 27/10/2018 05:45:41 p. m. ******/
DROP TABLE [dbo].[UserSettings]
GO
/****** Object:  Table [dbo].[UserSettingss]    Script Date: 27/10/2018 05:45:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ColumnSize] [int] NULL,
	[Theme] [int] NULL,
	[Controls] [bit] NULL,
 CONSTRAINT [PK_UserSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[UserSettings] ON 

GO
INSERT [dbo].[UserSettings] ([Id], [ColumnSize], [Theme], [Controls]) VALUES (1, 1, 1, 1)
GO
INSERT [dbo].[UserSettings] ([Id], [ColumnSize], [Theme], [Controls]) VALUES (2, 2, 2, 1)
GO
INSERT [dbo].[UserSettings] ([Id], [ColumnSize], [Theme], [Controls]) VALUES (3, 3, 3, 1)
GO
INSERT [dbo].[UserSettings] ([Id], [ColumnSize], [Theme], [Controls]) VALUES (4, 4, 4, 1)
GO
INSERT [dbo].[UserSettings] ([Id], [ColumnSize], [Theme], [Controls]) VALUES (5, 5, 5, 1)
GO
INSERT [dbo].[UserSettings] ([Id], [ColumnSize], [Theme], [Controls]) VALUES (6, 6, 6, 1)
GO
INSERT [dbo].[UserSettings] ([Id], [ColumnSize], [Theme], [Controls]) VALUES (7, 7, 7, 1)
GO
SET IDENTITY_INSERT [dbo].[UserSettings] OFF
GO
