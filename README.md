# user-register-api
- Use .Net core version 8
- Use SQL Server Database
- Runing localhost: https://localhost:7226/swagger

#Start Project
- Open file appsettings.json
- Change SQLConnection to your database

# SQL Config
# Step 1. Create Table

# [Books]
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


# [Staff]
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


# [Members]
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


# [Categories]
CREATE TABLE [dbo].[Categories](
	[Category_Id] [uniqueidentifier] NOT NULL,
	[Category_Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


# [Borrowings]
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


# [Fines]
CREATE TABLE [dbo].[Fines](
	[Fine_Id] [uniqueidentifier] NOT NULL,
	[Borrow_Id] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[Payment_Status] [nvarchar](50) NULL,
	[Paid_Date] [date] NULL,
 CONSTRAINT [PK_Fines] PRIMARY KEY CLUSTERED 
(
	[Fine_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


# Step 2. Mocking Staff
-- ** Login password: P@ssw0rd
INSERT INTO Staff (
Staff_Id, ID_Card, Username, [Password], First_Name, Last_Name, 
Position, Email, Phone, Is_Active, Create_Date)
VAlUES ('d3b07384-d113-4956-a55a-ae1644d32d3e', '1122334455667', 'Admin', '$2a$11$sfjYmRf0RddvYO8ufQak7uUwlMkrhyH85/7fAzMLuaBmdeSJhPP22', 
'Manee', 'Jaidee', 'Library Admin', 'Manee@mail.com', '0921345678', 1, GETDATE());