# FTSS => Fast, Testable, Secure, and Scalable restful APIs by C# .Net Core



## Aims:
I follow these approaches by developing this template:
* Fast in developing and fast in real environment
* Testable with automation tests
* Secure RESTful APIs
* Scalable

## Tech stacks:
I using Microsoft stacks for developing this template and Postman for running APIs:
* C#
* Dot Net Core 3.1
* Asp.NET Core MVC REST
* EF Core
* Dapper
* JWT
* NUnit
* MS-SQL Server
* Postman

## Free:
This template is completely open-source and everyone can branch, download, redeploy, using for learning or enterprise purpose.

## Responsibility:
I deployed this template for sharing my knowledge and experience in order to get feedback from other developers to correct myself and improve my skills. However, I work hardly to deploy a clean and trust code, it maybe contains ambiguous class or method that I hope it gets better by your comments in future.

## Code-First or DB-First:
Since this template created for developing complex and huge projects with vast variety of functionalities, my approach is DB-First.

## Authentication:
I defined users and roles inside Database. All authentication functions such as Login, remember password, change and reset password, and create a new user implemented by Stored-Procedures inside database.

## Security and Authorization:
Each end-points must send their jwt in header for authorization. Before doing anything, the application tries to find-out user info, roles, and authorization.

## Tiers:
I used new project approach for each tier:
FTSS.API
Is the upper tier of solution which contains RESTful API interface

* **FTSS.Models:** _Models the database objects_

* **FTSS.DP.EF:** _Implement access to database functions using Entity Framework ORM_

* **FTSS.DP.Dapper:** _Implement access to database functions using Dapper ORM_

* **FTSS.Logic:** _All common function such as Error Log, Messages and Convert types Implement by this tier_

# Implementation
In this section I described the source code in details.

## Login
End-Point calling api/Users/Login and send Username & Password. If Username & Password is valid, Login will return JWT token + User non-sensitive data.
![Login flowchart](https://github.com/heidarbozorg/FTSS/raw/master/FTSS%20Login.png)
