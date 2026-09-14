# user-register-api
- Use .Net core version 8
- Use SQL Server Database
- Runing localhost: https://localhost:7226/swagger

#Start Project
- Open file appsettings.json
- Change SQLConnection to your database

# SQL Config
# Step 1. Insert Table

-- [Books]
CREATE TABLE [dbo].[Books](
	[Book_Id] [uniqueidentifier] NOT NULL,
	[ISBN] [nvarchar](15) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](80) NOT NULL,
	[Publisher] [nvarchar](80) NULL,
	[Publish_Year] [int] NULL,
	[Total_Copies] [int] NOT NULL,
	[Available_Copies] [int] NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[Category_Id] [uniqueidentifier] NOT NULL,
	[Is_Active] [bit] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Book_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

-- [Staff]
CREATE TABLE [dbo].[Staff](
	[Staff_Id] [uniqueidentifier] NOT NULL,
	[ID_Card] [nvarchar](15) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Is_Active] [bit] NOT NULL,
	[Create_Date] [datetime] NULL,
	[Update_Date] [datetime] NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Staff_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

-- [Members]
CREATE TABLE [dbo].[Members](
	[Member_Id] [uniqueidentifier] NOT NULL,
	[ID_Card] [nvarchar](15) NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](80) NOT NULL,
	[Phone] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Is_Active] [bit] NOT NULL,
	[Create_By] [uniqueidentifier] NULL,
	[Create_Date] [datetime] NULL,
	[Update_By] [uniqueidentifier] NULL,
	[Update_Date] [datetime] NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Member_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

-- [Categories]
CREATE TABLE [dbo].[Categories](
	[Category_Id] [uniqueidentifier] NOT NULL,
	[Category_Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

-- [Borrowings]
CREATE TABLE [dbo].[Borrowings](
	[Borrow_Id] [uniqueidentifier] NOT NULL,
	[Book_Id] [uniqueidentifier] NOT NULL,
	[Member_Id] [uniqueidentifier] NOT NULL,
	[Staff_Id] [uniqueidentifier] NOT NULL,
	[Borrow_Date] [date] NOT NULL,
	[Due_Date] [date] NOT NULL,
	[Return_Date] [date] NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Borrowings] PRIMARY KEY CLUSTERED 
(
	[Borrow_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

-- [Fines]
CREATE TABLE [dbo].[Fines](
	[Fine_Id] [uniqueidentifier] NOT NULL,
	[Borrow_Id] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[Payment_Status] [nvarchar](50) NULL,
	[Paid_date] [datetime] NULL,
 CONSTRAINT [PK_Fines] PRIMARY KEY CLUSTERED 
(
	[Fine_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


# Step 2. Mocking some data

-- [Categories]
INSERT INTO Categories (Category_Id, Category_Name)
VALUES ('e1d2c3b4-a5b6-4c7d-8e9f-0a1b2c3d4e5f', 'Educations'), ('f3e2d1c0-b9a8-4736-8251-0d9c8b7a6f5e', 'Sports');

-- [Books]
INSERT INTO Books (Book_Id, ISBN, Title, Author, Publisher, Publish_Year, Total_Copies, Available_Copies, Location, Category_Id, Is_Active)
VALUES ('a1c94297-b892-4e2b-bf2d-cc6358e8b111', '120034567895', 'Programing Teaching', 'Somsak', 'General Publisher', 2026, 5, 5, 'Shelf 1, Row number 1', 'e1d2c3b4-a5b6-4c7d-8e9f-0a1b2c3d4e5f', 1),
('5c7d8e9f-0a1b-4c2d-3e4f-5a6b7c8d9e0f', '654789000127', 'Food Teaching', 'Marisa', 'General Publisher', 2026, 10, 10, 'Shelf 2, Row number 1', 'e1d2c3b4-a5b6-4c7d-8e9f-0a1b2c3d4e5f', 1),
('1d2e3f4a-5b6c-4d7e-8f9a-0b1c2d3e4f5a', '365478009103', 'Football', 'Sutin', 'DD Publisher', 2026, 3, 3, 'Shelf 3, Row number 1', 'f3e2d1c0-b9a8-4736-8251-0d9c8b7a6f5e', 1),
('b7c8d9e0-fa1b-4c2d-3e4f-5a6b7c8d9e0f', '365479214008', 'Basketball', 'Anna', 'DD Publisher', 2026, 8, 8, 'Shelf 3, Row number 2', 'f3e2d1c0-b9a8-4736-8251-0d9c8b7a6f5e', 1);

-- [Staff ]
-- ** Login password: Test-P@ssw0rd
INSERT INTO Staff (
Staff_Id, ID_Card, Username, [Password], First_Name, Last_Name, 
Position, Email, Phone, Is_Active, Create_Date)
VAlUES ('d3b07384-d113-4956-a55a-ae1644d32d3e', '1122334455667', 'Admin', '$2a$11$UE.VVxIWovwQjXClYIQIbOaEtRd4M37qq/I2u6EIfDRLYo8cCuZcO', 
'Manee', 'Jaidee', 'Library Admin', 'Manee@mail.com', '0921345678', 1, GETDATE());