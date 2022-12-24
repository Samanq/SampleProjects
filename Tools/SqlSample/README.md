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

## Create Simple Table
```SQL
Create Table	Customers(
Id				BigInt	Identity	Not Null	Primary Key,
FirstName		nVarchar(50)		Not Null,
LastName		nVarchar(50)		Not Null,
Mobile			nVarchar(13)		Not Null	Unique,
Code			char(5)				Not Null	Unique,
Constraint		CK_Customer						Check(Code Like '[0-9][0-9][0-9][0-9][0-9]')) -- Check Condition
```
---

## Create Simple Table
```SQL
Create Table	Departments(
Id				BigInt	Identity	Not Null	Primary Key,
Title			nVarchar(50)		Not Null)
```
---

## Create Table with relation
```SQL
Create Table	Students(
Id				BigInt	Identity	Not Null	Primary Key,
FirstName		nVarchar(50)		Not Null,
DepartmentId	BigInt				Not Null	References Departments(Id))
```
---

## Insert Data
```SQL
Insert	Customers	(FirstName,LastName,Mobile,Code)
		Values		('Jane','Doe','+989124474326','12346')	
```
---

## Insert Data
```SQL
Insert	Departments	(Title)
		Values		('Accounting')
Insert	Departments	(Title)
		Values		('Research')
```
---

## Insert Data
```SQL
Insert	Students    (DepartmentId, FirstName)
		Values		(1, 'John')
Insert	Students	(DepartmentId, FirstName)
		Values		(2, 'Jane')
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

## Delete a Table
```SQL
drop table Customers
```
