# SQL Server


## Polybase services Problem
SQL Server PolyBase Data Movement (MSSQLSERVER) and SQL Server PolyBase Engine (MSSQLSERVER) will stock in stating and it lead's to fiiling the log folder.

In order to solve this issue follow the steps
1. Open **SQL Server Configuration Manager** you can find it in start menu or in Computer Management Under the **Service and Applications** section.
2. Expand SQL Server Network Configuration - Protocols for MSSQLSERVER
3. On the right panel right click on **TCP/IP** and click Enable.
4. Restart the computer.
5. Now you can delete the dump files from C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Log\Polybase\dump

---

## SSMS not found problem
1. Go to system properties - Advanced - Environment Variables.
2. Below **System variables** select **path** and click **Edit**.
3. Click **New**
3. Write ssms.exe path (C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE)
---

## Create Database
```SQL
Create	Database	SqlSample
Go
Use SqlSample
Go
```
---

## Create Table
```SQL
Create Table	Customers(
Id		BigInt	Identity	Not Null	Primary Key,
FirstName	nVarchar(50)		Not Null,
LastName	nVarchar(50)		Not Null,
Mobile		nVarchar(13)		Not Null	Unique,
Code		char(5)			Not Null	Unique,
DepartmentId	BigInt			Not Null	References Departments(Id)
Constraint	CK_Customer				Check(Code Like '[0-9][0-9][0-9][0-9][0-9]')) -- Check Condition
```
---

## Delete Table
```SQL
drop table Customers
```
---

## Insert Data
```SQL
Insert	Customers	(FirstName, LastName)
Values			('John','Doe'),
			('Jane','Doe')
----------------------------------------------
Insert	Customers	
Values			('John','Doe'),
			('Jane','Doe')
```
---

## Insert from select (INSERT INTO)
```SQL
INSERT INTO	Customer	(FirstName, LastName)
SELECT	FirstName, LastName
FROM	People;
```
---

## Inner Join
```SQL
Select		FirstName as StudentName,
		Title as Department
From		Students
Inner Join	Departments
On Students.DepartmentId = Departments.id
Order By (Students.FirstName)
```
---

## Create Stored Procedure
```SQL
CREATE PROCEDURE GETSTUDENTS
As
Select		FirstName as StudentName,
			Title as Department
From		Students
Inner Join	Departments
On Students.DepartmentId = Departments.id
Order By (Students.FirstName)
```
---

## Run a Stored procedure
```SQL
EXEC	GETSTUDENTS
```
---

## Creating Trigger
We can create triggers that is automatically executed in response to certain events or actions, such insert, update or delete.
In this example you can see that we log data changes in another table in case of update.
```SQL
CREATE TRIGGER  tr_Students_Update
ON              Students
AFTER UPDATE
AS
BEGIN
  INSERT INTO Changes (ActionDate, TableName, ColumnName, OldValue, NewValue)
  SELECT        GETDATE(), 'Students', 'FirstName', d.FirstName, i.FirstName
  FROM          deleted d
  INNER JOIN    inserted i
  ON            d.Id = i.Id

  INSERT INTO Changes (ActionDate, TableName, ColumnName, OldValue, NewValue)
  SELECT        GETDATE(), 'Students', 'LastName', d.LastName, i.LastName
  FROM          deleted d
  INNER JOIN    inserted i
  ON            d.Id = i.Id
END
```
---

