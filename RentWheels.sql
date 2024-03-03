+CREATE TABLE [dbo].[RoleDetails] (
    [RoleId] INT PRIMARY KEY NOT NULL,
    [RoleName] NVARCHAR(50) NOT NULL
);

insert into RoleDetails values(1,'Admin'),(2,'User');
select * from RegisterDetails;


CREATE TABLE [dbo].[RegisterDetails] (
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Username] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(50) NOT NULL,
    [PhoneNo] NVARCHAR(20) PRIMARY KEY NOT NULL,
    [Gender] VARCHAR(10) NOT NULL,
    [DrivingLicenseNo] NVARCHAR(20) NOT NULL,
    [AadharNo] NVARCHAR(20) NOT NULL,
    [Age] INT NOT NULL,
    [Address] NVARCHAR(MAX) NOT NULL,
    [Password] NVARCHAR(20) NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [FK_Register_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[RoleDetails] ([RoleId])
);

drop table Cardetails;

CREATE TABLE [dbo].[CarDetails] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CarNo] VARCHAR(15)  PRIMARY KEY NOT NULL,
    [ModelName] VARCHAR(20) NOT NULL,
    [BrandName] VARCHAR(20) NOT NULL, 
    [Color] VARCHAR(50) NOT NULL,
    [RideSelection] VARCHAR(20) NOT NULL,
    [Fees] INT NOT NULL, 
    [FuelType] VARCHAR(50) NOT NULL,
    [Travel] INT NOT NULL,
    [Available] VARCHAR(10) NOT NULL,
    [ImageUrl] NVARCHAR(MAX) NOT NULL
);


CREATE TABLE [dbo].[CarRental] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [PhoneNo]     NVARCHAR (20) NOT NULL,
	[CustomerName] VARCHAR(50) NOT NULL, 
    [CarNo]       VARCHAR (15) NOT NULL,
    [Fees]        INT          NOT NULL,
    [StartDate]   DATETIME     NOT NULL,
    [EndDate]     DATETIME     NOT NULL,
    [TotalAmount] INT          NOT NULL,    
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CarRental_RegisterDetails] FOREIGN KEY ([PhoneNo]) REFERENCES [dbo].[RegisterDetails] ([PhoneNo]),
    CONSTRAINT [FK_CarRental_CarDetails] FOREIGN KEY ([CarNo]) REFERENCES [dbo].[CarDetails] ([CarNo])
);


CREATE TABLE [dbo].[CarReturn] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [CarNo] VARCHAR(15) NOT NULL, 
    [ReturnDate] DATETIME NOT NULL,
	ExpiredDays int Not null, 
	TotalAmount int Not Null,
    CONSTRAINT [FK_CarReturn_CarDetails] FOREIGN KEY ([CarNo]) REFERENCES [dbo].[CarDetails] ([CarNo])
);


INSERT INTO [dbo].[CarDetails] ([CarNo], [ModelName], [BrandName], [Color], [RideSelection], [Fees], [FuelType], [Travel], [Available], [ImageUrl]) VALUES  
('TN7890', 'Alto', 'Maruti', 'Blue', 'Hatchback', 1500, 'Petrol', 180, 'Yes', '~/Content/images/AltoMaruti.jpg'),
('TN2345', 'Baleno', 'Maruti', 'Silver', 'Hatchback', 1800, 'Petrol', 200, 'Yes', '~/Content/images/BalenoMaruti.jpg'),
('TN5678', 'City', 'Honda', 'White', 'Sedan', 2500, 'Petrol', 180, 'Yes', '~/Content/images/CityHonda.jpg'),
('TN8765', 'Creta', 'Hyundai', 'Grey', 'SUV', 2800, 'Petrol', 220, 'Yes', '~/Content/images/CretaHyundai.jpg'),
('TN9876', 'Innova', 'Toyota', 'Silver', 'SUV', 3500, 'Diesel', 250, 'Yes', '~/Content/images/InnovaToyota.jpg'),
('TN1234', 'Swift', 'Maruti', 'Red', 'Sedan', 2000, 'Petrol', 200, 'Yes', '~/Content/images/SwiftMaruti.jpg'),
('TN8766', 'Scorpio', 'Mahindra', 'Red', 'SUV', 3200, 'Diesel', 240, 'Yes', '~/Content/images/ScorpioMahindra.jpg'),
('TN5432', 'XUV500', 'Mahindra', 'Black', 'SUV', 3000, 'Diesel', 230, 'Yes', '~/Content/images/Xuv500Mahindra.jpg'),
('TN6789', 'Verna', 'Hyundai', 'Black', 'Sedan', 2700, 'Petrol', 210, 'Yes', '~/Content/images/VernaHyundai.jpg');

		
INSERT INTO [dbo].[RegisterDetails] ([Username], [Email], [PhoneNo], [Gender], [DrivingLicenseNo], [AadharNo], [Age], [Address], [Password], [RoleId])
VALUES
('Manikandan', 'manikandanpalanisamy123@gmail.com', '9360982027', 'Male', 'DL1234567890', 123456789012, 22, '123 Main Street, City, State, India', '_Mani@0122', 2),
('Keerthi', 'Keerthi@gmail.com', '9876543211', 'Male', 'DL0987654321', 987654321098, 28, '456 Elm Street, City, State, India', '_Mani@0122', 2);

select * from CarDetails;

Select * from  [dbo].[CarRental];

select * from carreturn;

Select * from  [dbo].[RegisterDetails];

drop table [dbo].[RegisterDetails];

drop table [dbo].[CarRental];

truncate table [dbo].[RegisterDetails];

truncate table [dbo].carreturn;
truncate table [dbo].Carrental;



DELETE FROM [dbo].[CarReturn];
DELETE FROM [dbo].[CarRental];
DELETE FROM [dbo].[CarDetails];
TRUNCATE TABLE [dbo].[CarDetails];

ALTER TABLE CarDetails
ALTER COLUMN Available VARCHAR(50);

Create table DisCount(
[Id] INT IDENTITY(1,1) NOT NULL,
    [CarNo] VARCHAR(15)  PRIMARY KEY NOT NULL,
    [ModelName] VARCHAR(20) NOT NULL,
    [BrandName] VARCHAR(20) NOT NULL, 
	[Offer] int,
	CONSTRAINT [FK_DisCount_CarDetails] FOREIGN KEY ([CarNo]) REFERENCES [dbo].[CarDetails] ([CarNo])
);

drop table Discount;

insert into Discount values('TN7890', 'Alto', 'Maruti', 10),('TN2345', 'Baleno', 'Maruti',30),('TN5678', 'City', 'Honda',20);

select * from CarDetails where available = 'No';


UPDATE CarDetails
SET Available = 'Yes'
WHERE Available = 'No';