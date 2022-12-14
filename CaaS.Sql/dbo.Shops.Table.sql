USE [DbCaaS00Testing]
GO
/****** Object:  Table [dbo].[Shops]    Script Date: 20.11.2022 23:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shops](
	[shop_id] [varchar](40) NOT NULL,
	[shop_name] [nvarchar](255) NOT NULL,
	[field_descriptions] [nvarchar](255) NULL,
	[mandant_id] [varchar](40) NOT NULL,
	[address] [varchar](40) NOT NULL,
	[app_key] [varchar](40) NOT NULL,
 CONSTRAINT [PK_Shops] PRIMARY KEY CLUSTERED 
(
	[shop_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Shops]  WITH CHECK ADD  CONSTRAINT [FK_Shops_AddressesShops] FOREIGN KEY([address])
REFERENCES [dbo].[AddressesShops] ([address_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shops] CHECK CONSTRAINT [FK_Shops_AddressesShops]
GO
ALTER TABLE [dbo].[Shops]  WITH CHECK ADD  CONSTRAINT [FK_Shops_AppKeys] FOREIGN KEY([app_key])
REFERENCES [dbo].[AppKeys] ([app_key])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shops] CHECK CONSTRAINT [FK_Shops_AppKeys]
GO
ALTER TABLE [dbo].[Shops]  WITH CHECK ADD  CONSTRAINT [FK_Shops_Mandants] FOREIGN KEY([mandant_id])
REFERENCES [dbo].[Mandants] ([person_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shops] CHECK CONSTRAINT [FK_Shops_Mandants]
GO
