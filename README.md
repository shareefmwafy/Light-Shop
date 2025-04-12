# Light Shop E-Commerce Platform (ASP.NET Core)


## Project Requirements  
For a comprehensive understanding of the project scope, refer to the [**Project Requirements documentation**](./docs/ProjectRequirements.md).  
It outlines all functional and non-functional requirements, detailed use cases.

---

## Domain Models  
The domain models used in this project were derived from the project requirements to reflect the business logic accurately.  
To view the detailed domain models, please refer to the [**Domain Models documentation**](./docs/DomainModels.md).  

Here is a class diagram for the domain models of the project:

![image](https://github.com/user-attachments/assets/749fad91-559c-41c6-aa0c-87f19ec2cda5)




## Features
- User registration and login
- Products Management
- Categories Management
- Brands Collection
- RESTful APIs


## 🏗️ Project Architecture
- ASP.NET Core Web API (Backend)
- Entity Framework Core for ORM
- SQL Server Database
- AutoMapper for DTO mapping (Mapster 7.4.0)


## 🛠️ Tech Stack
- ASP.NET Core 9.0
- Entity Framework Core (9.0.4)
- Entity Framework Core Tools (9.0.3)
- SQL Server
- AutoMapper
- JWT Authentication
- Postman for API documentation
- Scalar for API testing



## 🔧 Getting Started

### Prerequisites
- .NET SDK 9
- Visual Studio / VS Code
- SQL Server
- Postman (optional) Or Any API Tester Platform

### Installation Steps
```bash
git clone https://github.com/shareefmwafy/Light-Shop
cd Light-Shop
dotnet restore
dotnet ef database update
dotnet run
