USE [DbCaaS00Testing]
GO
/****** Object:  Table [dbo].[OrdersShop1]    Script Date: 20.11.2022 23:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersShop1](
	[order_id] [varchar](40) NOT NULL,
	[cust_id] [varchar](40) NOT NULL,
	[cart_id] [varchar](40) NOT NULL,
	[order_date] [datetime] NOT NULL,
 CONSTRAINT [PK_OrdersShop1] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrdersShop1]  WITH CHECK ADD  CONSTRAINT [FK_OrdersShop1_CartsShop1] FOREIGN KEY([cart_id])
REFERENCES [dbo].[CartsShop1] ([cart_id])
GO
ALTER TABLE [dbo].[OrdersShop1] CHECK CONSTRAINT [FK_OrdersShop1_CartsShop1]
GO
ALTER TABLE [dbo].[OrdersShop1]  WITH CHECK ADD  CONSTRAINT [FK_OrdersShop1_CustomersShop1] FOREIGN KEY([cust_id])
REFERENCES [dbo].[CustomersShop1] ([person_id])
GO
ALTER TABLE [dbo].[OrdersShop1] CHECK CONSTRAINT [FK_OrdersShop1_CustomersShop1]
GO
