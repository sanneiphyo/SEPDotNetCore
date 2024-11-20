# SEPDotNetCore

C# .NET

C# Language
.NET

Console App
Windows Forms
ASP.NET Core Web API
ASP.NET Core Web MVC
Blazor Web Assembly
Blazor Web Server

.NET framework (1, 2, 3, 3.5, 4, 4.5, 4.6, 4.7, 4.8) windows
.NET Core (1, 2, 2.2, 3, 3.1) vs2019, vs2022 - windows, linux, macos
.NET (5 - vs2019, 6 - vs2022, 7, 8 )- windows, linux, macos

vscode
visual studio 2022

windows

UI + Business Logic + Data Access => Database

Kpay

Mobile No => Transfer

Mobile No Check
10000

SLH => Collin

10000 => 0

-5000 => +5000

Bank + 5000

``` sql 

select * from Tbl_Blog 

select * from Tbl_Blog where DeleteFlag=0


update Tbl_Blog set BlogTitle ='Unique Title' where BlogId= 1


delete from Tbl_Blog where BlogId=3

update Tbl_Blog set DeleteFlag=0

update Tbl_Blog set DeleteFlag=1 where BlogId=5
```

select * from tbl_blog with (no lock)

commit data /umcommit data

insert into

update tbl_blog

1 -  mg mg 1
2 - mg mg 2
3- mg mg 3 - mg mg 6
4 - mg mg -4dotnet ef dbcontext scaffold "Server=.;Database=DigitalWallet;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f
5 - mg mg 5

efcore database first (manual , auto) / codefirst


dotnet ef dbcontext scaffold "Server=.;Database=SEPDotNetCore;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f


dotnet ef dbcontext scaffold "Server=.;Database=DigitalWallet;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f

------------------------------------------------------------------------------------------------

Visual Studio 2022 installation
Microsoft SQL Server 2022
SSMs (Sql server management system)

c# Basic
SQL Basic

DeleteFlag
Console App (create project)
DTO(data transfer object)
Nuget Package
ADO.Net

Dapper
-ORM
-Data Model

Efcore
-AppDbContext
-AsNoTracking
-Databasefirst

RestApI(ASP.Net Core Web Api)
- swagger
- postman
- http method
- http status code


-------------------------------------------------------------

Backend Api

Data Model (data access , database) 10 columns 
View Model (Frontend return Data) 2 columns
---------------------------------------------------------------

File.json

File.json => Read => Convert Object [] => Insert => Json => Write
----------------------------------------------------------------------
Kpay
Mobile No

Me - Another One

Id FullName MobileNo Balance Pin => 000000

Bank => Deposit / Withdraw

Deposit
Deposit API => MobileNo, Balance (+) => 1000 + (-1000)

Withdraw
Withdraw API => MobileNo, Balance (+) => 1000 - (-1000) at least 10,000 MMK

Transfer
Transfer API =>

FromMobileNo ToMobileNo Amount Pin Notes

FromMobile check

ToMobileNo check

FromMobileNo != ToMobileNo

Pin ==

Balance

FromMobileNo Balance -

ToMobileNo Balance +

Message (Complete)

Transaction History

Balance

Create Wallet User

Login

Change Pin

Phone No Change

Forget Password

Reset Password

First Time Login