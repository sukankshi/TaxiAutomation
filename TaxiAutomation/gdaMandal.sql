USE [master]
GO
/****** Object:  Database [TaxiAutomation]    Script Date: 23-04-2015 22:13:03 ******/
CREATE DATABASE [TaxiAutomation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaxiAutomation', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TaxiAutomation.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TaxiAutomation_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TaxiAutomation_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TaxiAutomation] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaxiAutomation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaxiAutomation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaxiAutomation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaxiAutomation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaxiAutomation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaxiAutomation] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaxiAutomation] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaxiAutomation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaxiAutomation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaxiAutomation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaxiAutomation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaxiAutomation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaxiAutomation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaxiAutomation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaxiAutomation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaxiAutomation] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TaxiAutomation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaxiAutomation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaxiAutomation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaxiAutomation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaxiAutomation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaxiAutomation] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaxiAutomation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaxiAutomation] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TaxiAutomation] SET  MULTI_USER 
GO
ALTER DATABASE [TaxiAutomation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaxiAutomation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaxiAutomation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaxiAutomation] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TaxiAutomation]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TransportId] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[Cost] [float] NOT NULL,
	[NoOfVehicles] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
	[BoardingLatitude] [float] NOT NULL,
	[BoardingLongitude] [float] NOT NULL,
	[DestinationLatitude] [float] NOT NULL,
	[DestinationLongitude] [float] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MobileNo] [nvarchar](50) NOT NULL,
	[EmailId] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[EmergencyNo] [nvarchar](50) NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Danger]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Danger](
	[Id] [bigint] NOT NULL,
	[DangerLocationLatitude] [float] NOT NULL,
	[DangerLocationLongitude] [float] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[TransportId] [bigint] NOT NULL,
 CONSTRAINT [PK_Danger] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Driver]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LicenceNo] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[EmailId] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](50) NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[gcmRegistrationKey] [nvarchar](50) NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transport]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transport](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RegistrationNo] [nvarchar](50) NOT NULL,
	[VehicleNo] [nvarchar](50) NOT NULL,
	[DriverId] [bigint] NOT NULL,
	[TransportType] [int] NOT NULL,
	[LocationLatitude] [float] NOT NULL,
	[LocationLongitude] [float] NOT NULL,
	[Available] [bit] NOT NULL,
	[Booked] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Transport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([Id], [TransportId], [CustomerId], [Cost], [NoOfVehicles], [TimeStamp], [BoardingLatitude], [BoardingLongitude], [DestinationLatitude], [DestinationLongitude]) VALUES (6, 1, 31, 14.978290957248769, 1, CAST(N'2015-04-23 18:55:27.5779952' AS DateTime2), 120.99, 140.65, 125.25, 187.96)
SET IDENTITY_INSERT [dbo].[Booking] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [MobileNo], [EmailId], [FirstName], [LastName], [UserName], [Password], [EmergencyNo], [TimeStamp]) VALUES (31, N'7890', N'sssukankshi@gmail.com', N'sssukankshi', N'jjaain', N'sssukku', N'1001234', N'11987654', CAST(N'2015-04-11 18:20:50.6667354' AS DateTime2))
INSERT [dbo].[Customer] ([Id], [MobileNo], [EmailId], [FirstName], [LastName], [UserName], [Password], [EmergencyNo], [TimeStamp]) VALUES (32, N'98784851245', N'ghfhfg@gfg.fd', N'fnme', N'lnme', N'unme', N'123458', N'7889564512', CAST(N'2015-04-11 18:22:30.2064212' AS DateTime2))
INSERT [dbo].[Customer] ([Id], [MobileNo], [EmailId], [FirstName], [LastName], [UserName], [Password], [EmergencyNo], [TimeStamp]) VALUES (33, N'78590', N'ssjsnjsukankshi@gmail.com', N'ssasukankshi', N'jjaaain', N'sshwjxsukku', N'10012534', N'119587654', CAST(N'2015-04-11 18:24:25.4665045' AS DateTime2))
INSERT [dbo].[Customer] ([Id], [MobileNo], [EmailId], [FirstName], [LastName], [UserName], [Password], [EmergencyNo], [TimeStamp]) VALUES (34, N'98598616', N'gvnhh@ythg.ub', N'fcgvb ', N'vhgbn', N'cfgvbh', N'56320', N'5632063', CAST(N'2015-04-13 07:02:20.5428368' AS DateTime2))
INSERT [dbo].[Customer] ([Id], [MobileNo], [EmailId], [FirstName], [LastName], [UserName], [Password], [EmergencyNo], [TimeStamp]) VALUES (37, N'87984152', N'hbjsd@gmail.com', N'djnwd', N'njdn', N'nkmd', N'4532', N'65230', CAST(N'2015-04-13 17:41:21.1949616' AS DateTime2))
INSERT [dbo].[Customer] ([Id], [MobileNo], [EmailId], [FirstName], [LastName], [UserName], [Password], [EmergencyNo], [TimeStamp]) VALUES (38, N'100023456789', N'sukankkshi@gmail.com', N'sukalnkshi', N'ljain', N'slukku', N'010234', N'0987654', CAST(N'2015-04-16 11:46:17.3160383' AS DateTime2))
INSERT [dbo].[Customer] ([Id], [MobileNo], [EmailId], [FirstName], [LastName], [UserName], [Password], [EmergencyNo], [TimeStamp]) VALUES (39, N'10258938', N'bhssukankshi@gmail.com', N'kartikey', N'kumar', N'njfsukku', N'741852963', N'456', CAST(N'2015-04-23 16:44:19.7692740' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Driver] ON 

INSERT [dbo].[Driver] ([Id], [LicenceNo], [FirstName], [LastName], [Age], [EmailId], [ContactNo], [TimeStamp], [Password], [gcmRegistrationKey]) VALUES (1, N'fgufk845', N'sukankshi', N'jain', 42, N'sukankshi@gmail.com', N'9874563210', CAST(N'2015-04-23 17:19:22.4419115' AS DateTime2), N'qwerty', NULL)
INSERT [dbo].[Driver] ([Id], [LicenceNo], [FirstName], [LastName], [Age], [EmailId], [ContactNo], [TimeStamp], [Password], [gcmRegistrationKey]) VALUES (2, N'lic007', N'himanshu', N'mandal', 21, N'hmandal007@gmail.com', N'8744020499', CAST(N'2015-04-23 17:19:22.4419115' AS DateTime2), N'qwerty', NULL)
SET IDENTITY_INSERT [dbo].[Driver] OFF
SET IDENTITY_INSERT [dbo].[Transport] ON 

INSERT [dbo].[Transport] ([Id], [RegistrationNo], [VehicleNo], [DriverId], [TransportType], [LocationLatitude], [LocationLongitude], [Available], [Booked], [TimeStamp]) VALUES (1, N'1245ghgh', N'1545gh', 1, 0, 120.25, 125.69, 1, 0, CAST(N'1996-01-12 00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Transport] ([Id], [RegistrationNo], [VehicleNo], [DriverId], [TransportType], [LocationLatitude], [LocationLongitude], [Available], [Booked], [TimeStamp]) VALUES (2, N'1254hha007', N'hajsha007', 2, 0, 148.21, 187.26, 1, 0, CAST(N'1996-01-12 00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Transport] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_EmailId_Unique]    Script Date: 23-04-2015 22:13:03 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EmailId_Unique] ON [dbo].[Customer]
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_MobileNo_Unique]    Script Date: 23-04-2015 22:13:03 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_MobileNo_Unique] ON [dbo].[Customer]
(
	[MobileNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserName_Unique]    Script Date: 23-04-2015 22:13:03 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserName_Unique] ON [dbo].[Customer]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Customer]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Transport] FOREIGN KEY([TransportId])
REFERENCES [dbo].[Transport] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Transport]
GO
ALTER TABLE [dbo].[Danger]  WITH CHECK ADD  CONSTRAINT [FK_Danger_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Danger] CHECK CONSTRAINT [FK_Danger_Customer]
GO
ALTER TABLE [dbo].[Danger]  WITH CHECK ADD  CONSTRAINT [FK_Danger_Transport] FOREIGN KEY([TransportId])
REFERENCES [dbo].[Transport] ([Id])
GO
ALTER TABLE [dbo].[Danger] CHECK CONSTRAINT [FK_Danger_Transport]
GO
ALTER TABLE [dbo].[Transport]  WITH CHECK ADD  CONSTRAINT [FK_Transport_Driver] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Driver] ([Id])
GO
ALTER TABLE [dbo].[Transport] CHECK CONSTRAINT [FK_Transport_Driver]
GO
/****** Object:  StoredProcedure [dbo].[AddBooking]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[AddBooking] 
	-- Add the parameters for the stored procedure here
	@transportid bigint,
	@customerid bigint,
	@cost float,
	@noofvehicles bigint,
	@boardinglatitude float,
	@boardinglongitude float,
	@destinationlatitude float,
	@destinationlongitude float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO Booking
                         (TransportId, CustomerId, Cost, NoOfVehicles,BoardingLatitude,BoardingLongitude,DestinationLatitude,DestinationLongitude, TimeStamp)
VALUES        (@transportid,@customerid,@cost,@noofvehicles,@boardinglatitude,@boardinglongitude,@destinationlatitude,@destinationlongitude, SYSDATETIME())
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[CheckBookStatus]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[CheckBookStatus] 
	-- Add the parameters for the stored procedure here
	@driverid bigint
AS
BEGIN
declare @booked bit;
declare @vehicleno bigint;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT @booked=Booked , @vehicleno=VehicleNo from Transport
		Where DriverId=@driverid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[CreateCustomer]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[CreateCustomer] 
	-- Add the parameters for the stored procedure here
	@mobileno nvarchar(MAX),
	@emailid nvarchar(MAX),
	@firstname nvarchar(MAX),
	@lastname nvarchar(MAX),
	@username nvarchar(MAX),
	@password nvarchar(MAX),
	@emergencyno nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO Customer
                         (MobileNo, EmailId, FirstName, LastName, UserName, Password, EmergencyNo, TimeStamp)
VALUES        (@mobileno,@emailid,@firstname,@lastname,@username,@password,@emergencyno, SYSDATETIME())
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[CreateDanger]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE PROCEDURE [dbo].[CreateDanger] 
	-- Add the parameters for the stored procedure here
	@transportid bigint,
	@customerid bigint,
	@dangerlocationlatitude float,
	@dangerlocationlongitude float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO Danger
                         (TransportId, CustomerId, DangerLocationLongitude, DangerLocationLatitude, TimeStamp)
VALUES        (@transportid,@customerid,@dangerlocationlongitude,@dangerlocationlatitude, SYSDATETIME())
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[CreateDriver]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[CreateDriver] 
	-- Add the parameters for the stored procedure here
	@licenceNo nvarchar(MAX),
	@firstName nvarchar(MAX),
	@lastName nvarchar(MAX),
	@age int,
	@emailId nvarchar(MAX),
	@contactNo nvarchar(MAX),
	@password nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO Driver
                         (LicenceNo, FirstName, LastName, Age, EmailId, ContactNo, Password, TimeStamp)
VALUES        (@licenceNo,@firstName,@lastName,@age,@emailId,@contactNo,@password, SYSDATETIME())
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[CreateTransport]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[CreateTransport] 
	-- Add the parameters for the stored procedure here
	@registrationNo nvarchar(MAX),
	@vehicleNo nvarchar(MAX),
	@driverId bigint,
	@transportType bit,
	@location nvarchar(MAX),
	@available bit,
	@booked bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO Transport
                         (RegistrationNo, VehicleNo, DriverId, TransportType, Location, Available, Booked, TimeStamp)
VALUES        (@registrationNo,@vehicleNo,@driverId,@transportType,@location,@available,@booked,SYSDATETIME())
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[CustomerForDriver]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[CustomerForDriver] 
	-- Add the parameters for the stored procedure here
	@transportid bigint
AS
BEGIN
declare @customerid bigint;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT @customerid=CustomerId from Booking
		Where TransportId=@transportid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[CustomerLogin]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[CustomerLogin] 
	-- Add the parameters for the stored procedure here
	@emailid nvarchar(MAX),
	@password nvarchar(MAX)
AS
BEGIN
declare @id int;

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT @id=Id 
		from Customer
		Where (EmailId=@emailid) and (Password=@password)
                         

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
return @id
END



GO
/****** Object:  StoredProcedure [dbo].[DeleteCustomer]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCustomer] 
	-- Add the parameters for the stored procedure here
	@emailid nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		DELETE FROM Customer
WHERE        EmailId=@emailid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[DeleteDriver]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDriver] 
	-- Add the parameters for the stored procedure here
	@driverId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		DELETE FROM Driver WHERE Id=@driverId
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[DeleteTransport]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[DeleteTransport] 
	-- Add the parameters for the stored procedure here
	@transportId nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		DELETE FROM Transport
WHERE        (Id = @transportId)
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[DriverForCustomer]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[DriverForCustomer] 
	-- Add the parameters for the stored procedure here
	@customerid bigint

AS
BEGIN
declare @driverid int,
		@booked bit;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
SELECT        Transport.DriverId, Transport.Booked
FROM            Driver INNER JOIN
                         Transport ON Driver.Id = Transport.DriverId INNER JOIN
                         Booking ON Transport.Id = Booking.TransportId INNER JOIN
                         Customer ON Booking.CustomerId = Customer.Id
WHERE        (Booking.CustomerId = @customerid)

		--SELECT t.DriverId,t.Booked from Transport t INNER JOIN Booking b on t.VehicleNo=b.TransportId and b.CustomerId=@customerid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;

		--return @driverid
END



GO
/****** Object:  StoredProcedure [dbo].[EditCustomer]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditCustomer] 
	-- Add the parameters for the stored procedure here
	@mobileno nvarchar(MAX),
	@firstname nvarchar(MAX),
	@lastname nvarchar(MAX),
	@password nvarchar(MAX),
	@emergencyno nvarchar(MAX),
	@emailid nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		UPDATE       Customer
SET                MobileNo = @mobileno, FirstName = @firstname, LastName = @lastname, Password = @password, EmergencyNo = @emergencyno, 
                         TimeStamp = SYSDATETIME() where EmailId=@emailid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[EditDriver]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[EditDriver] 
	-- Add the parameters for the stored procedure here
	@driverId bigint,
	@licenceNo nvarchar(MAX),
	@firstName nvarchar(MAX),
	@lastName nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		UPDATE       Driver
SET                LicenceNo =@licenceNo, FirstName =@firstName, LastName =@lastName WHERE Id=@driverId
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[EditTransport]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[EditTransport] 
	-- Add the parameters for the stored procedure here
	@transportId nvarchar(MAX),
	@driverId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		UPDATE       Transport
SET                DriverId = @driverId, TransportType = @transportId, TimeStamp = SYSDATETIME()
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[EnterDriverLocation]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[EnterDriverLocation] 
	-- Add the parameters for the stored procedure here
	@locationlatitude float,
	@locationlongitude float,
	@driverid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		Update  Transport
	SET
      LocationLatitude=@locationlatitude,LocationLongitude=@locationlongitude
				 where DriverId=@driverid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[EnterStatus]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[EnterStatus] 
	-- Add the parameters for the stored procedure here
	@available bit,
	@driverid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		UPDATE       Transport
SET                Available=@available , TimeStamp = SYSDATETIME()
                          where DriverId=@driverid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetAllBookings]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetAllBookings] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT        Booking.*
FROM            Booking
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetAllCustomers]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCustomers] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT        Customer.*
FROM            Customer
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetAllDangers]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDangers] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT        Danger.*
FROM            Danger
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetAllDrivers]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDrivers] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT        Driver.*
FROM            Driver
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetAllTransports]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetAllTransports] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT        Transport.*
FROM            Transport
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetCustomerBookingDetails]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerBookingDetails] 
	-- Add the parameters for the stored procedure here
	@customerid bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT c.FirstName,b.BoardingLatitude,b.BoardingLongitude,b.DestinationLatitude,b.DestinationLongitude
		from Customer c INNER JOIN Booking b on c.Id=@customerid and b.CustomerId=@customerid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetCustomerId]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerId] 
	-- Add the parameters for the stored procedure here
	@emailId nvarchar(MAX)
AS
BEGIN
declare @id int;

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT        @id=Id
FROM            Customer
WHERE        (EmailId = @emailId)
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
return @id
		
END



GO
/****** Object:  StoredProcedure [dbo].[GetDriverById]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetDriverById] 
	-- Add the parameters for the stored procedure here
	@emailid nvarchar(MAX),
	@password nvarchar(MAX)
AS
BEGIN
declare @id int;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT @id=Id from Driver
		Where EmailId=@emailid and Password=@password
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;

		return @id
END



GO
/****** Object:  StoredProcedure [dbo].[GetDriverByLicenceNo]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetDriverByLicenceNo] 
	-- Add the parameters for the stored procedure here
	@licenceNo nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT        *
FROM            Driver
WHERE        (LicenceNo = @licenceNo)
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
/****** Object:  StoredProcedure [dbo].[GetDriverDetails]    Script Date: 23-04-2015 22:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Himanshu Mandal
-- Create date: 22/03/2015
-- Description:	Creates a new Driver in the database.
-- =============================================
CREATE PROCEDURE [dbo].[GetDriverDetails] 
	-- Add the parameters for the stored procedure here
	@customerid bigint,
	 @driverid bigint
AS
BEGIN
declare @drivername nvarchar(MAX),
		@cost float,
		@taxino nvarchar(MAX),
		@contactno nvarchar(MAX)

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
	BEGIN TRY
		SELECT @drivername=d.FirstName, @contactno=d.ContactNo, @taxino=b.TransportId, @cost=b.Cost from Driver d inner join Booking b
			on d.Id=@driverid and b.CustomerId=@customerid
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		 ROLLBACK TRANSACTION;
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END



GO
USE [master]
GO
ALTER DATABASE [TaxiAutomation] SET  READ_WRITE 
GO
