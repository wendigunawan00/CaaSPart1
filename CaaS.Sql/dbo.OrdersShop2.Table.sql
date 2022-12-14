USE [DbCaaS00Testing]
GO
/****** Object:  Table [dbo].[OrdersShop2]    Script Date: 20.11.2022 23:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersShop2](
	[order_id] [varchar](40) NOT NULL,
	[cust_id] [varchar](40) NOT NULL,
	[cart_id] [varchar](40) NOT NULL,
	[order_date] [datetime] NOT NULL,
 CONSTRAINT [PK_OrdersShop2] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrdersShop2]  WITH CHECK ADD  CONSTRAINT [FK_OrdersShop2_CartsShop2] FOREIGN KEY([cart_id])
REFERENCES [dbo].[CartsShop2] ([cart_id])
GO
ALTER TABLE [dbo].[OrdersShop2] CHECK CONSTRAINT [FK_OrdersShop2_CartsShop2]
GO
ALTER TABLE [dbo].[OrdersShop2]  WITH CHECK ADD  CONSTRAINT [FK_OrdersShop2_CustomersShop2] FOREIGN KEY([cust_id])
REFERENCES [dbo].[CustomersShop2] ([person_id])
GO
ALTER TABLE [dbo].[OrdersShop2] CHECK CONSTRAINT [FK_OrdersShop2_CustomersShop2]
GO
