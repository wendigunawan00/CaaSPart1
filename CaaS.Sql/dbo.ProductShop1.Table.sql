USE [DbCaaS00Testing]
GO
/****** Object:  Table [dbo].[ProductShop1]    Script Date: 20.11.2022 23:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductShop1](
	[product_id] [varchar](40) NOT NULL,
	[product_name] [nvarchar](255) NOT NULL,
	[price] [float] NOT NULL,
	[amount_desc] [nvarchar](255) NOT NULL,
	[product_desc] [nvarchar](255) NULL,
	[download_link] [nvarchar](255) NULL,
 CONSTRAINT [PK_ProductShop1] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
