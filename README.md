# FTSS => Fast, Testable, Secure, and Scalable restful APIs by C# .Net Core



## Aims:
I follow these approaches by developing this project:
* Fast in developing and fast in real environment
* Testable with automation tests
* Secure RESTful APIs
* Scalable

## Stack:
I using Microsoft stack for developing and Postman for running APIs:
* C#
* Dot Net Core 3.1
* Asp.NET Core MVC REST
* Dapper as ORM
* JWT
* NUnit
* Moq
* MS-SQL Server
* Postman

## Free:
This project is completely open-source and everyone can branch, download, redeploy, using for learning or enterprise purpose.

## Responsibility:
I created this project to share my knowledge and experience to get feedback from other developers to correct myself and improve my skills. However, I work hard to create a clean and trust code, it may contain ambiguous classes or methods that I hope get better by your comments in the future.

## Code-First or DB-First:
Since this project created for developing complex and huge projects with a vast variety of functionalities, my approach is DB-First.

## Authentication:
I defined users and roles inside Database. All authentication functions such as Login, remember the password, change and reset the password, and create a new user are implemented by Stored-Procedures inside the database.

## Security and Authorization:
Each end-points must send their JWT in the request header for authorization. Before doing anything, the application tries to find-out user info, roles, and authorization. For checking user or role access to a certain API, I used Stored-Procedure to check Is-Access at database. In database I defined tables for maintaining API address and titles, roles, role access to APIs, and user roles table.

## Tiers:
I used a seperate project for each tier:
* **FTSS.API:** _Is the upper tier of solution which contains RESTful API interface and dependecy pool_

* **FTSS.Models:** _Models database objects_

* **FTSS.DP.Dapper:** _Implement access to database objects using Dapper ORM_

* **FTSS.Logic:** _All common functions such as JWT generator, Error Log, Messages, Mappers, and Convert types were Implemented by this tier_

# Implementation
In this section I described the source code in details.

## Login
End-Point calling api/Users/Login and send Email & Password. If Email & Password is valid, Login will return JWT token + User non-sensitive data.
![Login flowchart](https://github.com/heidarbozorg/FTSS/raw/master/FTSS.API/Documents/Login/FTSS%20Login.png)
