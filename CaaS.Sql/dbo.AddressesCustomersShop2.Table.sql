USE [DbCaaS00Testing]
GO
/****** Object:  Table [dbo].[AddressesCustomersShop2]    Script Date: 20.11.2022 23:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressesCustomersShop2](
	[address_id] [varchar](40) NOT NULL,
	[street] [varchar](40) NOT NULL,
	[floor] [varchar](40) NOT NULL,
	[postal_code] [float] NOT NULL,
	[city] [varchar](40) NOT NULL,
	[province] [varchar](40) NOT NULL,
	[country] [varchar](40) NOT NULL,
 CONSTRAINT [PK_AddressesCustomersShop2] PRIMARY KEY CLUSTERED 
(
	[address_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
