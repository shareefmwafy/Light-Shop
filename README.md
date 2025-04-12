# Light Shop E-Commerce Platform (ASP.NET Core)

## About the Project
This is a complete e-commerce backend platform built using **ASP.NET Core Web API**. It supports user authentication, product management, cart system, order processing, and secure RESTful APIs.

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
