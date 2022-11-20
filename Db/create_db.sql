CREATE TABLE [dbo].[person] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [first_name]    VARCHAR (20) NOT NULL,
    [last_name]     VARCHAR (50) NOT NULL,
    [date_of_birth] DATE         NOT NULL,
    CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([Id] ASC)
);


SET IDENTITY_INSERT [dbo].[person] ON
INSERT INTO [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (1, N'Alan', N'Turing', N'1912-06-23')
INSERT INTO [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (2, N'Dennis', N'Ritchie', N'1914-09-25')
INSERT INTO [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (3, N'Anders', N'Hejlsberg ', N'1960-12-02')
INSERT INTO [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (4, N'James', N'Gosling', N'1955-05-19')
INSERT INTO [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (5, N'Bjarne', N'Stroustrup ', N'1950-12-30')
INSERT INTO [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (6, N'Linus', N'Torwalds', N'1969-12-28')
SET IDENTITY_INSERT [dbo].[person] OFF
