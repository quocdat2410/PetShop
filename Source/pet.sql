USE [PetShopp]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[cateName] [nvarchar](50) NULL,
	[cateId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[cateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT [dbo].[Categories] ([cateName], [cateId]) VALUES (N'Pate', 1)
INSERT [dbo].[Categories] ([cateName], [cateId]) VALUES (N'THỨC ĂN HẠT', 2)
INSERT [dbo].[Categories] ([cateName], [cateId]) VALUES (N'BÁNH THƯỞNG', 3)
SET IDENTITY_INSERT [dbo].[Categories] OFF
/****** Object:  Table [dbo].[User]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[role] [nvarchar](50) NULL,
	[isAdmin] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([userId], [userName], [phone], [email], [password], [role], [isAdmin]) VALUES (1, N'aaaaaa', N'1', N'a@gmail.com', N'1', N'Khách', 0)
INSERT [dbo].[User] ([userId], [userName], [phone], [email], [password], [role], [isAdmin]) VALUES (2, N'admin', N'1', N'admin@gmail.com', N'1', N'Admin', 1)
INSERT [dbo].[User] ([userId], [userName], [phone], [email], [password], [role], [isAdmin]) VALUES (3, N'admin2', N'1', N'admin2@gmail.com', N'1', N'Admin', 1)
INSERT [dbo].[User] ([userId], [userName], [phone], [email], [password], [role], [isAdmin]) VALUES (5, N'khach', N'1', N'k@gmail.com', N'1', N'Khách', 0)
INSERT [dbo].[User] ([userId], [userName], [phone], [email], [password], [role], [isAdmin]) VALUES (6, N'testt', N'1', N'ab@gmail.com', N'1', N'Khách', 0)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Product]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productName] [nvarchar](50) NULL,
	[price] [float] NULL,
	[cateId] [int] NULL,
	[color] [nvarchar](50) NULL,
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[brand] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[productImage] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON
INSERT [dbo].[Product] ([productName], [price], [cateId], [color], [productId], [brand], [description], [productImage]) VALUES (N'Pate cho Chó', 16000, 1, N'Vàng', 1, N'Hug', N'Nuôi dưỡng da lông bóng khỏe', N'pate-1.png')
INSERT [dbo].[Product] ([productName], [price], [cateId], [color], [productId], [brand], [description], [productImage]) VALUES (N'pate Monge Fresh & Monge Fruit', 20000, 1, N'Vàng', 2, N'Monge ', N'không màu thực phẩm và không chất bảo quản', N'pate-2.png')
INSERT [dbo].[Product] ([productName], [price], [cateId], [color], [productId], [brand], [description], [productImage]) VALUES (N'Pate hỗn hợp gà', 40000, 1, N'Trắng', 3, N'Kings Pet', N'Hương vị thơm ngon, kết cấu đặc mịn', N'pate-3.png')
INSERT [dbo].[Product] ([productName], [price], [cateId], [color], [productId], [brand], [description], [productImage]) VALUES (N'Thức ăn hạt mềm hữu cơ thịt Cừu', 25000, 2, N'Xanh', 4, N'Origi-7', N'Dành cho mọi giống chó thuộc mọi lứa tuổi', N'hat-1.png')
INSERT [dbo].[Product] ([productName], [price], [cateId], [color], [productId], [brand], [description], [productImage]) VALUES (N' Pedigree Adult vị thịt bò rau củ', 11300000, 2, N'Vàng', 5, N'Pedigree ', N'vitamin giúp tăng cường hệ miễn dịch', N'hat-2.png')
INSERT [dbo].[Product] ([productName], [price], [cateId], [color], [productId], [brand], [description], [productImage]) VALUES (N'Phô mai viên', 11300000, 3, N'Vàng', 6, N'Bowwow ', N'Cung cấp canxi, Vitamin A', N'banh-1.jpg')
SET IDENTITY_INSERT [dbo].[Product] OFF
/****** Object:  Table [dbo].[Cart]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](10, 2) NULL,
	[Total] [decimal](10, 2) NULL,
	[isPaid] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cart] ON
INSERT [dbo].[Cart] ([CartId], [UserId], [ProductId], [Quantity], [Price], [Total], [isPaid]) VALUES (17, 1, 1, 1, CAST(14300000.00 AS Decimal(10, 2)), CAST(14300000.00 AS Decimal(10, 2)), 0)
SET IDENTITY_INSERT [dbo].[Cart] OFF
/****** Object:  Table [dbo].[Invoice]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[invoiceId] [int] IDENTITY(1,1) NOT NULL,
	[cartId] [int] NULL,
	[userId] [int] NULL,
	[totalAmount] [decimal](10, 2) NULL,
	[invoiceDate] [datetime] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[invoiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Invoice] ON
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (1, NULL, 1, CAST(14300000.00 AS Decimal(10, 2)), CAST(0x0000B14F0014422D AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (2, NULL, 1, CAST(14300000.00 AS Decimal(10, 2)), CAST(0x0000B14F001784F5 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (3, NULL, 1, CAST(14300000.00 AS Decimal(10, 2)), CAST(0x0000B1550102DC4F AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (4, NULL, 1, CAST(14300000.00 AS Decimal(10, 2)), CAST(0x0000B156002B8D27 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (5, NULL, 6, CAST(28600000.00 AS Decimal(10, 2)), CAST(0x0000B15600F87BEF AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (6, NULL, 6, CAST(28600000.00 AS Decimal(10, 2)), CAST(0x0000B15600F887AF AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (13, NULL, 5, CAST(13300000.00 AS Decimal(10, 2)), CAST(0x0000B15600FF6FEF AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (14, NULL, 5, CAST(13300000.00 AS Decimal(10, 2)), CAST(0x0000B15600FFD34A AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (15, NULL, 5, CAST(13300000.00 AS Decimal(10, 2)), CAST(0x0000B156010090A3 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (16, NULL, 5, CAST(15300000.00 AS Decimal(10, 2)), CAST(0x0000B15601009736 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (17, NULL, 6, CAST(28600000.00 AS Decimal(10, 2)), CAST(0x0000B157002C43E1 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (18, NULL, 6, CAST(15300000.00 AS Decimal(10, 2)), CAST(0x0000B157002C43E4 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (19, NULL, 6, CAST(26600000.00 AS Decimal(10, 2)), CAST(0x0000B157002C43E4 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (20, NULL, 6, CAST(13300000.00 AS Decimal(10, 2)), CAST(0x0000B15700322754 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (21, NULL, 5, CAST(13300000.00 AS Decimal(10, 2)), CAST(0x0000B15701632674 AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (22, NULL, 5, CAST(13300000.00 AS Decimal(10, 2)), CAST(0x0000B1570180F8EC AS DateTime), 1)
INSERT [dbo].[Invoice] ([invoiceId], [cartId], [userId], [totalAmount], [invoiceDate], [status]) VALUES (23, NULL, 5, CAST(16000.00 AS Decimal(10, 2)), CAST(0x0000B1570180F8EE AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Invoice] OFF
/****** Object:  ForeignKey [FK_Cart_Product]     ******/
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([productId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Product]
GO
/****** Object:  ForeignKey [FK_Cart_User]     ******/
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
/****** Object:  ForeignKey [FK_Invoice_Cart]     ******/
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Cart] FOREIGN KEY([cartId])
REFERENCES [dbo].[Cart] ([CartId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Cart]
GO
/****** Object:  ForeignKey [FK_Invoice_User]     ******/
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User]
GO
/****** Object:  ForeignKey [FK_Product_Categories]     ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Categories] FOREIGN KEY([cateId])
REFERENCES [dbo].[Categories] ([cateId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Categories]
GO
