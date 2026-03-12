
IF DB_ID('QuantityMeasurementDB') IS NULL
BEGIN
    CREATE DATABASE QuantityMeasurementDB;
END
GO

USE QuantityMeasurementDB;
GO

/* ============================================
   Drop existing objects (safe reset)
   ============================================ */

IF OBJECT_ID('TRG_InsertMeasurementHistory','TR') IS NOT NULL
DROP TRIGGER TRG_InsertMeasurementHistory;
GO

IF OBJECT_ID('QuantityMeasurementHistory','U') IS NOT NULL
DROP TABLE QuantityMeasurementHistory;
GO

IF OBJECT_ID('QuantityMeasurements','U') IS NOT NULL
DROP TABLE QuantityMeasurements;
GO

/* ============================================
   Main Measurement Table
   ============================================ */

CREATE TABLE QuantityMeasurements
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    OperationType NVARCHAR(50) NOT NULL,
    MeasurementCategory NVARCHAR(50) NOT NULL,

    Operand1Value FLOAT NOT NULL,
    Operand1Unit NVARCHAR(50) NOT NULL,

    Operand2Value FLOAT NULL,
    Operand2Unit NVARCHAR(50) NULL,

    ResultValue FLOAT NULL,
    ResultUnit NVARCHAR(50) NULL,

    ErrorMessage NVARCHAR(500) NULL,

    Timestamp DATETIME DEFAULT GETDATE()
);
GO

/* ============================================
   History Table (Audit Logging)
   ============================================ */

CREATE TABLE QuantityMeasurementHistory
(
    HistoryId INT IDENTITY(1,1) PRIMARY KEY,

    MeasurementId INT,

    OperationType NVARCHAR(50),
    MeasurementCategory NVARCHAR(50),

    Operand1Value FLOAT,
    Operand1Unit NVARCHAR(50),

    Operand2Value FLOAT,
    Operand2Unit NVARCHAR(50),

    ResultValue FLOAT,
    ResultUnit NVARCHAR(50),

    ErrorMessage NVARCHAR(500),

    Timestamp DATETIME,

    AuditAction NVARCHAR(50),

    AuditTimestamp DATETIME DEFAULT GETDATE()
);
GO

/* ============================================
   Performance Indexes
   ============================================ */

CREATE INDEX IDX_OperationType
ON QuantityMeasurements(OperationType);
GO

CREATE INDEX IDX_Category
ON QuantityMeasurements(MeasurementCategory);
GO

CREATE INDEX IDX_Time
ON QuantityMeasurements(Timestamp);
GO

/* ============================================
   Trigger – Insert Audit History
   ============================================ */

CREATE TRIGGER TRG_InsertMeasurementHistory
ON QuantityMeasurements
AFTER INSERT
AS
BEGIN

INSERT INTO QuantityMeasurementHistory
(
MeasurementId,
OperationType,
MeasurementCategory,
Operand1Value,
Operand1Unit,
Operand2Value,
Operand2Unit,
ResultValue,
ResultUnit,
ErrorMessage,
Timestamp,
AuditAction
)

SELECT
Id,
OperationType,
MeasurementCategory,
Operand1Value,
Operand1Unit,
Operand2Value,
Operand2Unit,
ResultValue,
ResultUnit,
ErrorMessage,
Timestamp,
'INSERT'

FROM inserted;

END
GO

/* ============================================
   Stored Procedure – Save Measurement
   ============================================ */

CREATE PROCEDURE sp_SaveMeasurement
(
@OperationType NVARCHAR(50),
@MeasurementCategory NVARCHAR(50),

@Operand1Value FLOAT,
@Operand1Unit NVARCHAR(50),

@Operand2Value FLOAT = NULL,
@Operand2Unit NVARCHAR(50) = NULL,

@ResultValue FLOAT = NULL,
@ResultUnit NVARCHAR(50) = NULL,

@ErrorMessage NVARCHAR(500) = NULL
)
AS
BEGIN

INSERT INTO QuantityMeasurements
(
OperationType,
MeasurementCategory,
Operand1Value,
Operand1Unit,
Operand2Value,
Operand2Unit,
ResultValue,
ResultUnit,
ErrorMessage
)

VALUES
(
@OperationType,
@MeasurementCategory,
@Operand1Value,
@Operand1Unit,
@Operand2Value,
@Operand2Unit,
@ResultValue,
@ResultUnit,
@ErrorMessage
);

END
GO

/* ============================================
   Stored Procedure – Get All
   ============================================ */

CREATE PROCEDURE sp_GetAllMeasurements
AS
BEGIN

SELECT *
FROM QuantityMeasurements
ORDER BY Timestamp DESC;

END
GO

/* ============================================
   Stored Procedure – Filter by Operation
   ============================================ */

CREATE PROCEDURE sp_GetMeasurementsByOperation
@OperationType NVARCHAR(50)
AS
BEGIN

SELECT *
FROM QuantityMeasurements
WHERE OperationType = @OperationType
ORDER BY Timestamp DESC;

END
GO

/* ============================================
   Stored Procedure – Filter by Category
   ============================================ */

CREATE PROCEDURE sp_GetMeasurementsByCategory
@MeasurementCategory NVARCHAR(50)
AS
BEGIN

SELECT *
FROM QuantityMeasurements
WHERE MeasurementCategory = @MeasurementCategory
ORDER BY Timestamp DESC;

END
GO

/* ============================================
   Stored Procedure – Delete All
   ============================================ */

CREATE PROCEDURE sp_DeleteAllMeasurements
AS
BEGIN

DELETE FROM QuantityMeasurements;

END
GO

/* ============================================
   Stored Procedure – Count Records
   ============================================ */

CREATE PROCEDURE sp_GetTotalMeasurements
AS
BEGIN

SELECT COUNT(*) AS TotalMeasurements
FROM QuantityMeasurements;

END
GO

/* ============================================
   Stored Procedure – Get History
   ============================================ */

CREATE PROCEDURE sp_GetMeasurementHistory
AS
BEGIN

SELECT *
FROM QuantityMeasurementHistory
ORDER BY AuditTimestamp DESC;

END
GO

PRINT 'QuantityMeasurementDB setup completed successfully.';