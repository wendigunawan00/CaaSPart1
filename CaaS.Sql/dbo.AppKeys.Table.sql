USE [DbCaaS00Testing]
GO
/****** Object:  Table [dbo].[AppKeys]    Script Date: 20.11.2022 23:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppKeys](
	[app_key] [varchar](40) NOT NULL,
	[app_key_name] [nvarchar](255) NOT NULL,
	[app_key_password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_AppKeys] PRIMARY KEY CLUSTERED 
(
	[app_key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
