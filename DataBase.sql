USE [master]
GO
/****** Object:  Database [Clinic]    Script Date: 6/12/2025 5:59:21 PM ******/
CREATE DATABASE [Clinic]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'P1-Clinic', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\P1-Clinic.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'P1-Clinic_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\P1-Clinic_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Clinic] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Clinic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Clinic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Clinic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Clinic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Clinic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Clinic] SET ARITHABORT OFF 
GO
ALTER DATABASE [Clinic] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Clinic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Clinic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Clinic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Clinic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Clinic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Clinic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Clinic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Clinic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Clinic] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Clinic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Clinic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Clinic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Clinic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Clinic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Clinic] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Clinic] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Clinic] SET RECOVERY FULL 
GO
ALTER DATABASE [Clinic] SET  MULTI_USER 
GO
ALTER DATABASE [Clinic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Clinic] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Clinic] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Clinic] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Clinic] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Clinic] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Clinic', N'ON'
GO
ALTER DATABASE [Clinic] SET QUERY_STORE = ON
GO
ALTER DATABASE [Clinic] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Clinic]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAppointmentsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetAppointmentsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Count =   COUNT(AppointmentID) 
	  FROM Appointments 
	  WHERE PatientID = @PatientID;

	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCanceledAppointmentsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================

--1. Pending: The appointment has been scheduled but has not
--yet occurred.
--2. Confirmed: The appointment has been confirmed by both
--the patient and the healthcare provider.
--3. Completed: The appointment has taken place as scheduled.
--4. Canceled: The appointment has been canceled either by
--the patient or the healthcare provider.
--5. Rescheduled: The appointment has been rescheduled for a
--different date or time.
--6. No Show: The patient did not show up for the appointment

CREATE FUNCTION [dbo].[GetCanceledAppointmentsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Count =   COUNT(AppointmentID) 
	  FROM Appointments 
	  WHERE PatientID = @PatientID  AND AppointmentStatus = 4;

	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCompletedAppointmentsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================

--1. Pending: The appointment has been scheduled but has not
--yet occurred.
--2. Confirmed: The appointment has been confirmed by both
--the patient and the healthcare provider.
--3. Completed: The appointment has taken place as scheduled.
--4. Canceled: The appointment has been canceled either by
--the patient or the healthcare provider.
--5. Rescheduled: The appointment has been rescheduled for a
--different date or time.
--6. No Show: The patient did not show up for the appointment

CREATE FUNCTION [dbo].[GetCompletedAppointmentsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Count =   COUNT(AppointmentID) 
	  FROM Appointments 
	  WHERE PatientID = @PatientID  AND AppointmentStatus = 3;

	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetConfirmedAppointmentsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================

--1. Pending: The appointment has been scheduled but has not
--yet occurred.
--2. Confirmed: The appointment has been confirmed by both
--the patient and the healthcare provider.
--3. Completed: The appointment has taken place as scheduled.
--4. Canceled: The appointment has been canceled either by
--the patient or the healthcare provider.
--5. Rescheduled: The appointment has been rescheduled for a
--different date or time.
--6. No Show: The patient did not show up for the appointment

CREATE FUNCTION [dbo].[GetConfirmedAppointmentsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Count =   COUNT(AppointmentID) 
	  FROM Appointments 
	  WHERE PatientID = @PatientID  AND AppointmentStatus = 2;

	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetMedicalRecordsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetMedicalRecordsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
	 SELECT @Count = COUNT( MedicalRecords.MedicalRecordID )
FROM     Appointments INNER JOIN
                  MedicalRecords ON Appointments.AppointmentID = MedicalRecords.AppointmentID
WHERE PatientID = @PatientID ;
	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetNoShowAppointmentsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================

--1. Pending: The appointment has been scheduled but has not
--yet occurred.
--2. Confirmed: The appointment has been confirmed by both
--the patient and the healthcare provider.
--3. Completed: The appointment has taken place as scheduled.
--4. Canceled: The appointment has been canceled either by
--the patient or the healthcare provider.
--5. Rescheduled: The appointment has been rescheduled for a
--different date or time.
--6. No Show: The patient did not show up for the appointment

CREATE FUNCTION [dbo].[GetNoShowAppointmentsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Count =   COUNT(AppointmentID) 
	  FROM Appointments 
	  WHERE PatientID = @PatientID  AND AppointmentStatus = 6;

	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetPendingAppointmentsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetPendingAppointmentsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Count =   COUNT(AppointmentID) 
	  FROM Appointments 
	  WHERE PatientID = @PatientID  AND AppointmentStatus = 1;

	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetPrescriptionsCount]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetPrescriptionsCount]
(
	-- Add the parameters for the function here
	@PatientID  INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE  @Count INT

	-- Add the T-SQL statements to compute the return value here
SELECT @Count = COUNT(Prescriptions.PrescriptionID)
FROM     Appointments INNER JOIN
                  MedicalRecords ON Appointments.AppointmentID = MedicalRecords.AppointmentID INNER JOIN
                  Prescriptions ON MedicalRecords.MedicalRecordID = Prescriptions.MedicalRecordID
WHERE Appointments.PatientID = @PatientID;
	-- Return the result of the function
	RETURN @Count;

END
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [bit] NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
 CONSTRAINT [PK__Persons__AA2FFB8587F22B37] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetNameByPersonID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetNameByPersonID]
(
    @PersonID INT
)
RETURNS TABLE
AS
RETURN
(
	SELECT Name FROM Persons
	WHERE PersonID = @PersonID
)
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
 CONSTRAINT [PK__Patients__970EC346A08EB619] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetNameByPatientID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetNameByPatientID]
(
    @PatientID INT
)
RETURNS TABLE
AS
RETURN
(
	SELECT Ps.Name 
	FROM
	Patients Pt INNER JOIN Persons Ps
	ON Pt.PersonID = Ps.PersonID
	WHERE Pt.PatientID = @PatientID
)
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[DoctorID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[Specialization] [nvarchar](100) NULL,
 CONSTRAINT [PK__Doctors__2DC00EDF119A2508] PRIMARY KEY CLUSTERED 
(
	[DoctorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetNameByDoctorID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetNameByDoctorID]
(
    @DoctorID INT
)
RETURNS TABLE
AS
RETURN
(
	SELECT Ps.Name 
	FROM
	Doctors D INNER JOIN Persons Ps
	ON D.PersonID = Ps.PersonID
	WHERE D.DoctorID = @DoctorID
)
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[DoctorID] [int] NOT NULL,
	[AppointmentDateTime] [datetime] NOT NULL,
	[AppointmentStatus] [tinyint] NOT NULL,
 CONSTRAINT [PK__Appointm__8ECDFCA2896563A4] PRIMARY KEY CLUSTERED 
(
	[AppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAppointmentDetails]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAppointmentDetails]()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        A.AppointmentID,
        (SELECT * FROM dbo.GetNameByPatientID(A.PatientID)) AS PatientName,
        (SELECT * FROM dbo.GetNameByDoctorID(A.DoctorID)) AS DoctorName,
        CAST(A.AppointmentDateTime AS TIME) AS AppointmentTime,
        A.AppointmentStatus,
        A.DoctorID,
        A.PatientID,
        A.AppointmentDateTime
    FROM Appointments A
);
GO
/****** Object:  Table [dbo].[MedicalRecords]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalRecords](
	[MedicalRecordID] [int] IDENTITY(1,1) NOT NULL,
	[VisitDescription] [nvarchar](200) NULL,
	[Diagnosis] [nvarchar](200) NULL,
	[AdditionalNotes] [nvarchar](200) NULL,
	[AppointmentID] [int] NOT NULL,
 CONSTRAINT [PK__MedicalR__4411BBC251D4E64A] PRIMARY KEY CLUSTERED 
(
	[MedicalRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[AmountPaid] [decimal](18, 0) NOT NULL,
	[AdditionalNotes] [nvarchar](200) NULL,
	[AppointmentID] [int] NOT NULL,
 CONSTRAINT [PK__Payments__9B556A585488506C] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prescriptions]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prescriptions](
	[PrescriptionID] [int] IDENTITY(1,1) NOT NULL,
	[MedicalRecordID] [int] NOT NULL,
	[MedicationName] [nvarchar](100) NOT NULL,
	[Dosage] [nvarchar](50) NOT NULL,
	[Frequency] [nvarchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[SpecialInstructions] [nvarchar](200) NULL,
 CONSTRAINT [PK__Prescrip__40130812FF0A60CF] PRIMARY KEY CLUSTERED 
(
	[PrescriptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments.DoctorID] FOREIGN KEY([DoctorID])
REFERENCES [dbo].[Doctors] ([DoctorID])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments.DoctorID]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments.PatientID] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patients] ([PatientID])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments.PatientID]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Persons]
GO
ALTER TABLE [dbo].[MedicalRecords]  WITH CHECK ADD  CONSTRAINT [FK_MedicalRecords.AppointmentID] FOREIGN KEY([AppointmentID])
REFERENCES [dbo].[Appointments] ([AppointmentID])
GO
ALTER TABLE [dbo].[MedicalRecords] CHECK CONSTRAINT [FK_MedicalRecords.AppointmentID]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Persons]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments.AppointmentID] FOREIGN KEY([AppointmentID])
REFERENCES [dbo].[Appointments] ([AppointmentID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments.AppointmentID]
GO
ALTER TABLE [dbo].[Prescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Prescriptions.MedicalRecordID] FOREIGN KEY([MedicalRecordID])
REFERENCES [dbo].[MedicalRecords] ([MedicalRecordID])
GO
ALTER TABLE [dbo].[Prescriptions] CHECK CONSTRAINT [FK_Prescriptions.MedicalRecordID]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewAppointment]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewAppointment]
	-- Add the parameters for the stored procedure here
	@PatientID INT,
	@DoctorID INT,
	@AppointmentDateTime  DATETIME,
	@AppointmentStatus	TINYINT,
	@AppointmentID INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Appointments 
	VALUES(@PatientID,@DoctorID,@AppointmentDateTime,@AppointmentStatus,null);

	SET @AppointmentID =  SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewDoctor]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewDoctor]
	@PersonID	INT,
	@Specialization NVARCHAR(100),
	@DoctorID INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Doctors 
	VALUES(@PersonID,@Specialization)

	SELECT @DoctorID = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewMedicalRecord]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewMedicalRecord]
	-- Add the parameters for the stored procedure here

	@VisitDescription	NVARCHAR(200), 
	@Diagnosis NVARCHAR(200),
	@AdditionalNotes NVARCHAR(200),
	@AppointmentID INT,
	@MedicalRecordID INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO MedicalRecords 
	VALUES(@VisitDescription,@Diagnosis,@AdditionalNotes,@AppointmentID);

	SELECT @MedicalRecordID = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewPatient]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewPatient]
	@PersonID INT,
	@PatientID INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Patients 
	VALUES(@PersonID);

	SELECT @PatientID = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewPaymentRecord]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewPaymentRecord]
   @PaymentDate DATE,
   @PaymentMethod NVARCHAR(50),
   @AmountPaid DECIMAL,
   @AdditionalNotes NVARCHAR(200),
   @AppointmentID INT,
   @PaymentID INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Payments 
	VALUES(@PaymentDate ,
			@PaymentMethod,
			@AmountPaid ,
			@AdditionalNotes ,
			@AppointmentID );
	SELECT @PaymentID = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewPerson]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewPerson]
	-- Add the parameters for the stored procedure here
	@Name	NVARCHAR(100),
	@DateOfBirth DATE,
	@Gender	BIT,
	@PhoneNumber	NVARCHAR(20),
	@Email	NVARCHAR(100),
	@Address	NVARCHAR(200),
	@PersonID INT OUTPUT
AS 
BEGIN
    -- Insert statements for procedure here
	INSERT	INTO Persons 
	VALUES(@Name,@DateOfBirth,@Gender,@PhoneNumber,@Email,@Address);

	SET @PersonID = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewPrescription]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewPrescription]
   @MedicalRecordID	INT,
   @MedicationName NVARCHAR(100),
   @Dosage NVARCHAR(50),
   @Frequency NVARCHAR(50),
   @StartDate DATE,
   @EndDate DATE,
   @SpecialInstructions NVARCHAR(200),
   @PrescriptionID INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Prescriptions
	VALUES(@MedicalRecordID,@MedicationName,@Dosage,@Frequency
	,@StartDate,@EndDate,@SpecialInstructions)

	SELECT @PrescriptionID = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteDoctor]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteDoctor]
	@DoctorID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Doctors 
		WHERE DoctorID = @DoctorID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeletePatient]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeletePatient]
	@PatientID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Patients 
	WHERE PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeletePerson]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeletePerson]
	-- Add the parameters for the stored procedure here
	@PersonID INT 
AS 
BEGIN
    -- Insert statements for procedure here
	DELETE FROM Persons 	
	WHERE PersonID = @PersonID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllAppointments]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllAppointments]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.AppointmentID 	
	,(SELECT * FROM GetNameByPatientID(A.PatientID)) AS PatientName
	,(SELECT * FROM GetNameByDoctorID(A.DoctorID))  AS DoctorName
	,A.AppointmentDateTime,A.AppointmentStatus 
	FROM Appointments A
	ORDER BY A.AppointmentDateTime;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllDoctors]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllDoctors]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Doctors.DoctorID,Persons.PersonID, Persons.Name, Persons.DateOfBirth, 
	Persons.Gender, Persons.PhoneNumber, Persons.Email, Persons.Address,
	Doctors.Specialization
	FROM     Doctors INNER JOIN
    Persons ON Doctors.PersonID = Persons.PersonID
	ORDER BY Persons.DateOfBirth
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllMedicalRecords]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllMedicalRecords]

AS
BEGIN
	
	SELECT M.MedicalRecordID
	,(SELECT * FROM GetNameByPatientID(A.PatientID)) AS PatientName
	,(SELECT * FROM GetNameByDoctorID(A.DoctorID))  AS DoctorName
	,M.VisitDescription,M.Diagnosis
	,M.AdditionalNotes
	,A.AppointmentDateTime AS MedicalRecordDateTime
	FROM
	MedicalRecords M INNER JOIN Appointments A
	ON M.AppointmentID = A.AppointmentID
	ORDER BY M.MedicalRecordID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllPatients]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllPatients]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Patients.PatientID,Persons.PersonID, Persons.Name, Persons.DateOfBirth, Persons.Gender, Persons.PhoneNumber, Persons.Email, Persons.Address
	FROM     Patients INNER JOIN
    Persons ON Patients.PersonID = Persons.PersonID
	ORDER BY Persons.DateOfBirth
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllPersons]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllPersons]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllPrescriptions]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllPrescriptions]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Prescriptions.PrescriptionID, MedicalRecords.Diagnosis, Prescriptions.MedicationName, 
	Prescriptions.Dosage, Prescriptions.Frequency,
	Prescriptions.StartDate, Prescriptions.EndDate, 
	Prescriptions.SpecialInstructions, 
    (SELECT * FROM GetNameByPatientID( Appointments.PatientID)) As PatientName, 
	(SELECT * FROM GetNameByDoctorID( Appointments.DoctorID)) As DoctorName
FROM     Appointments INNER JOIN
                  MedicalRecords ON Appointments.AppointmentID = MedicalRecords.AppointmentID INNER JOIN
                  Prescriptions ON MedicalRecords.MedicalRecordID = Prescriptions.MedicalRecordID
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointmentInfoByDateTime]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointmentInfoByDateTime] 
	-- Add the parameters for the stored procedure here
	@AppointmentDateTime DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Appointments 
	WHERE AppointmentDateTime  = @AppointmentDateTime;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointmentInfoById]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointmentInfoById]
	-- Add the parameters for the stored procedure here
	@AppointmentID	INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointmentsByDate]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointmentsByDate]
   @AppointmentDate	DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT AppointmentID, PatientName, DoctorName, AppointmentTime, AppointmentStatus
    FROM dbo.GetAppointmentDetails()
	WHERE CAST (AppointmentDateTime AS DATE ) = @AppointmentDate
	ORDER BY AppointmentDateTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointmentsByDoctorID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointmentsByDoctorID] 
	-- Add the parameters for the stored procedure here
	@DoctorID INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AppointmentID, PatientName, DoctorName, AppointmentTime, AppointmentStatus
    FROM dbo.GetAppointmentDetails()
    WHERE DoctorID = @DoctorID
    ORDER BY AppointmentDateTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointmentsByPatientID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointmentsByPatientID] 
	-- Add the parameters for the stored procedure here
	@PatientID INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AppointmentID, PatientName, DoctorName, AppointmentTime, AppointmentStatus
    FROM dbo.GetAppointmentDetails()
	WHERE PatientID = @PatientID
	ORDER BY AppointmentDateTime

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointmentsByPatientIDAndDoctorID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointmentsByPatientIDAndDoctorID] 
	-- Add the parameters for the stored procedure here
	@DoctorID INT,
	@PatientID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AppointmentID, PatientName, DoctorName, AppointmentTime, AppointmentStatus
    FROM dbo.GetAppointmentDetails()
	WHERE DoctorID = @DoctorID	AND  PatientID = @PatientID
	ORDER BY AppointmentDateTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAppointmentsByStatus]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAppointmentsByStatus]
   @AppointmentStatus	TINYINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT AppointmentID, PatientName, DoctorName, AppointmentTime, AppointmentStatus
    FROM dbo.GetAppointmentDetails()
	WHERE AppointmentStatus = @AppointmentStatus
	ORDER BY AppointmentDateTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorInfoById]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorInfoById]
	@DoctorID	INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Doctors 
		WHERE DoctorID = @DoctorID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorInfoByPersonId]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorInfoByPersonId]
	-- Add the parameters for the stored procedure here
	@PersonID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Doctors 
	WHERE PersonID = @PersonID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorsByAddress]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorsByAddress]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@Address NVARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.DoctorID,Ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender,
	Ps.PhoneNumber,Ps.Email,Ps.Address,D.Specialization
	FROM
	Doctors D
	inner join
	Persons Ps
	ON D.PersonID = Ps.PersonID
	WHERE Address = @Address;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorsByDateOfBirth]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorsByDateOfBirth]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@DateOfBirth DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.DoctorID,Ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender
	,Ps.PhoneNumber,Ps.Email,Ps.Address,D.Specialization
	FROM
	Doctors D
	inner join
	Persons Ps
	ON D.PersonID = Ps.PersonID
	WHERE DateOfBirth = @DateOfBirth;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorsByGender]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorsByGender]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@Gender BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.DoctorID,Ps.PersonID,
	Ps.Name,Ps.DateOfBirth,Ps.Gender,
	Ps.PhoneNumber,Ps.Email,Ps.Address,D.Specialization
	FROM
	Doctors D
	inner join
	Persons Ps
	ON D.PersonID = Ps.PersonID
	WHERE Gender = @Gender;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorsByName]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorsByName]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@Name NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.DoctorID,Ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender
	,Ps.PhoneNumber,Ps.Email,Ps.Address,D.Specialization
	FROM
	Doctors D
	inner join
	Persons Ps
	ON D.PersonID = Ps.PersonID
	WHERE Name LIKE CONCAT(@Name,'%');
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorsBySpecialization]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorsBySpecialization]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@Specialization NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.DoctorID,Ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender
	,Ps.PhoneNumber,Ps.Email,Ps.Address,D.Specialization
	FROM
	Doctors D
	inner join
	Persons Ps
	ON D.PersonID = Ps.PersonID
	WHERE Specialization LIKE CONCAT(@Specialization,'%');
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorSchedulePerDay]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorSchedulePerDay]
	@DoctorID INT,
   @AppointmentDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT AppointmentID, PatientName, DoctorName, AppointmentTime, AppointmentStatus
    FROM dbo.GetAppointmentDetails()
	WHERE DoctorID = @DoctorID 
	AND  CAST( AppointmentDateTime AS DATE)  = @AppointmentDate
	ORDER BY AppointmentDateTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMedicalRecordByAppointmentID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetMedicalRecordByAppointmentID]
	-- Add the parameters for the stored procedure here
	@AppointmentID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT MR.MedicalRecordID,(SELECT * FROM dbo.GetNameByPatientID(A.PatientID) )AS PatientName,
	(SELECT * FROM dbo.GetNameByDoctorID(A.DoctorID)) AS DoctorName,MR.VisitDescription ,
	MR.Diagnosis,MR.AdditionalNotes
	FROM Appointments A 
	INNER JOIN MedicalRecords MR 
	ON A.AppointmentID = MR.AppointmentID
	WHERE A.AppointmentID = @AppointmentID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMedicalRecordByID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetMedicalRecordByID] 
	-- Add the parameters for the stored procedure here
	@MedicalRecordID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM MedicalRecords 
	WHERE MedicalRecordID = @MedicalRecordID;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMedicalRecordsByDoctorID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetMedicalRecordsByDoctorID]
	-- Add the parameters for the stored procedure here
	@DoctorID	INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT MR.MedicalRecordID,(SELECT * FROM dbo.GetNameByPatientID(A.PatientID) )AS PatientName,
	(SELECT * FROM dbo.GetNameByDoctorID(A.DoctorID)) AS DoctorName,MR.VisitDescription ,
	MR.Diagnosis,MR.AdditionalNotes
	FROM Appointments A 
	INNER JOIN MedicalRecords MR 
	ON A.AppointmentID = MR.AppointmentID
	WHERE A.DoctorID = @DoctorID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMedicalRecordsByPatientID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetMedicalRecordsByPatientID]
	@PatientID	INT
AS
BEGIN
	
	SELECT MR.MedicalRecordID,(SELECT * FROM dbo.GetNameByPatientID(A.PatientID) )AS PatientName,
	(SELECT * FROM dbo.GetNameByDoctorID(A.DoctorID)) AS DoctorName,MR.VisitDescription ,
	MR.Diagnosis,MR.AdditionalNotes
	FROM Appointments A 
	INNER JOIN MedicalRecords MR 
	ON A.AppointmentID = MR.AppointmentID
	WHERE A.PatientID = @PatientID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientHistorySummary]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPatientHistorySummary]
	-- Add the parameters for the stored procedure here
	@PatientID  INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  -- Insert statements for procedure here
	SELECT  @PatientID,
	(SELECT dbo.GetAppointmentsCount(Appointments.PatientID) )AS AppointmentsCount,
	(SELECT dbo.[GetCompletedAppointmentsCount](Appointments.PatientID) )AS CompletedAppointments,
	(SELECT dbo.GetConfirmedAppointmentsCount(Appointments.PatientID)) AS ConfirmedAppointments,
	(SELECT dbo.GetPendingAppointmentsCount(Appointments.PatientID) )AS PendingAppointments,
	(SELECT dbo.GetCanceledAppointmentsCount(Appointments.PatientID) )AS CanceledAppointments,
	(SELECT dbo.GetNoShowAppointmentsCount(Appointments.PatientID) )AS NoShowAppointments,
	(SELECT dbo.GetMedicalRecordsCount(Appointments.PatientID) )AS MedicalRecordsCount ,
	(SELECT dbo.GetPrescriptionsCount(Appointments.PatientID) )AS PrescriptionCount
FROM     Appointments INNER JOIN
                  MedicalRecords ON Appointments.AppointmentID = MedicalRecords.AppointmentID INNER JOIN
                  Patients ON Appointments.PatientID = Patients.PatientID INNER JOIN
                  Persons ON Patients.PersonID = Persons.PersonID INNER JOIN
                  Prescriptions ON MedicalRecords.MedicalRecordID = Prescriptions.MedicalRecordID

WHERE Appointments.PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientInfoById]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPatientInfoById]
	@PatientID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Patients
	WHERE PatientID = @PatientID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientInfoByPersonId]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPatientInfoByPersonId]
	-- Add the parameters for the stored procedure here
	@PersonID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Patients
	WHERE PersonID = @PersonID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientsByAddress]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPatientsByAddress]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@Address NVARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Pt.PatientID,Ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender,Ps.PhoneNumber,Ps.Email,Ps.Address
	FROM
	Patients Pt
	inner join
	Persons Ps
	ON Pt.PersonID = Ps.PersonID
	WHERE Address LIKE CONCAT(@Address,'%');
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientsByDateOfBirth]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPatientsByDateOfBirth]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@DateOfBirth DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Pt.PatientID,Ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender,Ps.PhoneNumber,Ps.Email,Ps.Address
	FROM
	Patients Pt
	inner join
	Persons Ps
	ON Pt.PersonID = Ps.PersonID
	WHERE DateOfBirth = @DateOfBirth;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientsByGender]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[SP_GetPatientsByGender]
	-- Add the parameters for the stored procedure here
	@Gender BIT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Pt.PatientID,Ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender,Ps.PhoneNumber,Ps.Email,Ps.Address
	FROM
	Patients Pt
	inner join
	Persons Ps
	ON Pt.PersonID = Ps.PersonID
	WHERE Gender = @Gender;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientsByName]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[SP_GetPatientsByName]
	-- Add the parameters for the stored procedure here
	@Name NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Pt.PatientID,ps.PersonID,Ps.Name,Ps.DateOfBirth,Ps.Gender,Ps.PhoneNumber,Ps.Email,Ps.Address
	FROM
	Patients Pt
	inner join
	Persons Ps
	ON Pt.PersonID = Ps.PersonID
	WHERE Name LIKE CONCAT(@Name,'%');
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPaymentByID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPaymentByID] 
	-- Add the parameters for the stored procedure here
	@PaymentID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Payments 
	WHERE PaymentID = @PaymentID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPaymentHistoryByAppointmentID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPaymentHistoryByAppointmentID]
   @AppointmentID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Payments.PaymentID, Payments.PaymentDate, Payments.PaymentMethod, Payments.AmountPaid,
	Payments.AdditionalNotes,(SELECT * FROM GetNameByPatientID(Appointments.PatientID)) AS PatientName
	,(SELECT * FROM GetNameByDoctorID(Appointments.DoctorID))  AS DoctorName, Appointments.AppointmentStatus
FROM     Appointments INNER JOIN
 Payments ON Appointments.AppointmentID = Payments.AppointmentID
 WHERE Appointments.AppointmentID = @AppointmentID;

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPaymentHistoryByPatientID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPaymentHistoryByPatientID]
   @PatientID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Payments.PaymentID, Payments.PaymentDate, Payments.PaymentMethod, Payments.AmountPaid,
	Payments.AdditionalNotes,(SELECT * FROM GetNameByPatientID(Appointments.PatientID)) AS PatientName
	,(SELECT * FROM GetNameByDoctorID(Appointments.DoctorID))  AS DoctorName, Appointments.AppointmentStatus
FROM     Appointments INNER JOIN
 Payments ON Appointments.AppointmentID = Payments.AppointmentID
 WHERE Appointments.PatientID = @PatientID;

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPaymentHistoryWithinDateRange]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPaymentHistoryWithinDateRange]
   @Start Date,
   @End Date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Payments.PaymentID, Payments.PaymentDate, Payments.PaymentMethod, Payments.AmountPaid,
	Payments.AdditionalNotes,(SELECT * FROM GetNameByPatientID(Appointments.PatientID)) AS PatientName
	,(SELECT * FROM GetNameByDoctorID(Appointments.DoctorID))  AS DoctorName, Appointments.AppointmentStatus
FROM     Appointments INNER JOIN
 Payments ON Appointments.AppointmentID = Payments.AppointmentID
 WHERE Payments.PaymentDate between @Start and @End;

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPersonInfoByEmail]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPersonInfoByEmail]
	-- Add the parameters for the stored procedure here
	@Email NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons 
	WHERE Email = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPersonInfoById]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPersonInfoById]
	-- Add the parameters for the stored procedure here
	@PersonID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons
	WHERE PersonID = @PersonID;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPersonInfoByPhone]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPersonInfoByPhone]
	-- Add the parameters for the stored procedure here
	@PhoneNumber nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons
	WHERE PhoneNumber = @PhoneNumber;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPersonsByAddress]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPersonsByAddress]
	-- Add the parameters for the stored procedure here
	@Address NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons 
	WHERE Address LIKE CONCAT(@Address, '%')
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPersonsByDateOfBirth]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPersonsByDateOfBirth]
	-- Add the parameters for the stored procedure here
	@DateOfBirth DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons
	WHERE DateOfBirth = @DateOfBirth;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPersonsByGender]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPersonsByGender]
	
	@Gender BIT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons 
		WHERE Gender = @Gender;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPersonsByName]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPersonsByName]
	-- Add the parameters for the stored procedure here
	@Name	NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons 
	WHERE Name LIKE CONCAT(@Name, '%')
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPrescriptionByID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPrescriptionByID] 
	-- Add the parameters for the stored procedure here
	@PrescriptionID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Prescriptions 
	WHERE PrescriptionID = @PrescriptionID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPrescriptionsByPatientID]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPrescriptionsByPatientID]
   @PatientID	INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Prescriptions.PrescriptionID, MedicalRecords.Diagnosis, Prescriptions.MedicationName, 
	Prescriptions.Dosage, Prescriptions.Frequency,
	Prescriptions.StartDate, Prescriptions.EndDate, 
	Prescriptions.SpecialInstructions, 
    (SELECT * FROM GetNameByPatientID( Appointments.PatientID)) As PatientName, 
	(SELECT * FROM GetNameByDoctorID( Appointments.DoctorID)) As DoctorName
FROM     Appointments INNER JOIN
                  MedicalRecords ON Appointments.AppointmentID = MedicalRecords.AppointmentID INNER JOIN
                  Prescriptions ON MedicalRecords.MedicalRecordID = Prescriptions.MedicalRecordID
	WHERE Appointments.PatientID = @PatientID;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsAppointmentHaveMedicalRecord]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsAppointmentHaveMedicalRecord] 
	-- Add the parameters for the stored procedure here
	@AppointmentID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT MedicalRecordID FROM MedicalRecords
		WHERE AppointmentID = @AppointmentID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsAppointmentPaidFor]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsAppointmentPaidFor]
   @AppointmentID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT PaymentID FROM Payments 
	WHERE AppointmentID = @AppointmentID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsAppointmentReserved]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsAppointmentReserved] 
	-- Add the parameters for the stored procedure here
	@AppointmentDateTime DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AppointmentID
	FROM Appointments
	WHERE AppointmentDateTime = @AppointmentDateTime
	   OR (
	       CAST(AppointmentDateTime AS DATE) = CAST(@AppointmentDateTime AS DATE)
	       AND DATEPART(HOUR, AppointmentDateTime) = DATEPART(HOUR, @AppointmentDateTime)
	   );

END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsEmailUsed]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsEmailUsed] 
	-- Add the parameters for the stored procedure here
	@Email NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons
	WHERE Email = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsEmailUsedExceptPerson]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsEmailUsedExceptPerson]
	-- Add the parameters for the stored procedure here
	@PersonID INT,
	@Email NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons
	WHERE Email = @Email AND PersonID != @PersonID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsPhoneNumberUsed]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsPhoneNumberUsed] 
	-- Add the parameters for the stored procedure here
	@PhoneNumber NVARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT PersonID FROM Persons
	WHERE PhoneNumber = @PhoneNumber
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsPhoneNumberUsedExceptPerson]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsPhoneNumberUsedExceptPerson]
	-- Add the parameters for the stored procedure here
	@PersonID INT,
	@PhoneNumber NVARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Persons
	WHERE PhoneNumber = @PhoneNumber AND PersonID != @PersonID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAppointment]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateAppointment] 
	-- Add the parameters for the stored procedure here
	@AppointmentID INT ,
	@PatientID INT,
	@DoctorID INT,
	@AppointmentDateTime  DATETIME,
	@AppointmentStatus	TINYINT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Appointments]
   SET [PatientID] = @PatientID
      ,[DoctorID] = @DoctorID
      ,[AppointmentDateTime] = @AppointmentDateTime
      ,[AppointmentStatus] = @AppointmentStatus
 WHERE AppointmentID = @AppointmentID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAppointmentStatus]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateAppointmentStatus] 
	-- Add the parameters for the stored procedure here
	@AppointmentID INT ,
	@AppointmentStatus	TINYINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Appointments]
   SET [AppointmentStatus] = @AppointmentStatus
 WHERE AppointmentID = @AppointmentID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateDoctor]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateDoctor]
	-- Add the parameters for the stored procedure here
	-- Add the parameters for the stored procedure here
	@DoctorID INT,
	@PersonID INT,
	@Specialization NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Doctors 
	SET
	PersonID = @PersonID,
	Specialization = @Specialization
	WHERE DoctorID = @DoctorID;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateMedicalRecord]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateMedicalRecord]
	-- Add the parameters for the stored procedure here
	@MedicalRecordID INT,
	@VisitDescription	INT, 
	@Diagnosis NVARCHAR(200),
	@AdditionalNotes NVARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE MedicalRecords 
	SET
	VisitDescription = @VisitDescription,
	Diagnosis = @Diagnosis ,
	AdditionalNotes = @AdditionalNotes
	WHERE  MedicalRecordID = @MedicalRecordID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdatePerson]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdatePerson]
	-- Add the parameters for the stored procedure here
	@PersonID INT ,
	@Name	NVARCHAR(100),
	@DateOfBirth DATE,
	@Gender	BIT,
	@PhoneNumber	NVARCHAR(20),
	@Email	NVARCHAR(100),
	@Address	NVARCHAR(200)
AS 
BEGIN
    -- Insert statements for procedure here
	UPDATE Persons 
		SET 
			Name = @Name	,
			DateOfBirth = @DateOfBirth,
			Gender = @Gender,
			PhoneNumber = @PhoneNumber,
			Email = @Email	,
			Address = @Address	
		WHERE PersonID = @PersonID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdatePrescription]    Script Date: 6/12/2025 5:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdatePrescription]
   @PrescriptionID INT,
   @MedicalRecordID	INT,
   @MedicationName NVARCHAR(100),
   @Dosage NVARCHAR(50),
   @Frequeny NVARCHAR(50),
   @StartDate DATE,
   @EndDate DATE,
   @SpecialInstructions NVARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Prescriptions
	SET 
	MedicalRecordID =  @MedicalRecordID,
	MedicationName = @MedicationName,
	Dosage = @Dosage,
	Frequency = @Frequeny,
	StartDate = @StartDate,
	EndDate = @EndDate,
	SpecialInstructions = @SpecialInstructions
	WHERE PrescriptionID = @PrescriptionID

END
GO
USE [master]
GO
ALTER DATABASE [Clinic] SET  READ_WRITE 
GO
