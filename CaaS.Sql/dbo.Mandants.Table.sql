USE [DbCaaS00Testing]
GO
/****** Object:  Table [dbo].[Mandants]    Script Date: 20.11.2022 23:45:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mandants](
	[person_id] [varchar](40) NOT NULL,
	[first_name] [varchar](40) NOT NULL,
	[last_name] [varchar](40) NOT NULL,
	[dob] [datetime] NULL,
	[email] [varchar](60) NOT NULL,
	[address] [varchar](40) NOT NULL,
	[status] [varchar](16) NOT NULL,
 CONSTRAINT [PK_Mandants] PRIMARY KEY CLUSTERED 
(
	[person_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Mandants]  WITH CHECK ADD  CONSTRAINT [FK_Mandants_AddresesMandants] FOREIGN KEY([address])
REFERENCES [dbo].[AddressesMandants] ([address_id])
GO
ALTER TABLE [dbo].[Mandants] CHECK CONSTRAINT [FK_Mandants_AddresesMandants]
GO
