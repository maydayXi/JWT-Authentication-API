🌏 [中文](/README.zh-tw.md)

# JWT-Authentication-API

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)

This is ASP.NET Web API project built with .NET 8, 
demonstrating how to implement **JSON Web Token(JWT) authentication**
The project showcases how to secure API endpoints using JWT to ensure proper authentication and authorization.

## Table of content

- [Project Overview](#project-overview)
- [Features](#features)
- [Installation and Setup](#installation-and-setup)
- [Usage](#usage)
- [Tech Stack](#tech-stack)
- [Learn More](#learn-more)

## Project Overview

This project demonstrates how to create a secure ASP.NET Web API service 
using .NET 8 and implement JWT authentication for protecting API routes. 
The project includes the following features:

- User registration and login functionality.
- JWT generation and validate for API access.
- Protecting routes using JWT to ensure only authenticated user can access certain resources.

## Features

- **User registration and Login**: Provides endpoints for suer registration and login,
generating JWT on successful login.
- **JWT Authentication**: Protects API routes by validating JWT sent with request. 

## Installation and Setup

### 1. Install .NET8 SDK

Make sure you have the .NET 8 SDK installed. If you don't have it yet, 
you can download and install it from the [official Microsoft site](https://dotnet.microsoft.com/download/dotnet/8.0).

### 2. Clone the Repository

```bash
git clone https://github.com/maydayXi/JWT-Authentication-API.git
```

### 3. Create database

Update `appsettings.json` file with your connection string

```json
{
  "ConnectionStrings": {
    "JwtAuthDB": "Server=yourdbserverip,1433;Database=JwtAuthDB;user=yourdbuser;password=yourdbpassword;TrustServerCertificate=True;"
  }
}
```

If your database on local 

```json
{
  "ConnectionStrings": {
    "JwtAuthDB": "Server=localhost,1433;Database=JwtAuthDB;user=yourdbuser;password=yourdbpassword;TrustServerCertificate=True;"
  }
}
```

You can check out my detailed tutorial below to set up the database:

1. **[Add Database Migration](https://maydayxi.github.io/MyDevLog/posts/asp-dot-net-core-jwt-tutorial/#%E6%96%B0%E5%A2%9E-migration)**
2. **[Update Database](https://maydayxi.github.io/MyDevLog/posts/asp-dot-net-core-jwt-tutorial/#%E6%9B%B4%E6%96%B0%E8%B3%87%E6%96%99%E5%BA%AB)**

## Usage

Usage with **[Postman](https://www.postman.com/)**

### 1. User registration

Example request body
```json
{
  "email": "your email",
  "password": "your password"
}
```

No Password and email provided
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-empty-test.png" alt="Empty">

No Password provided
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-no-password-test.png" alt="No Password">

Registration
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-register-test.png" alt="registration">

### 2. User login

Example request body
```json
{
  "email": "your email",
  "password": "your password"
}
```

Login successfully
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-login-success.png" alt="Login success">

Login failed
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-login-failed-test.png" alt="Login failed">

## Tech Stack

- **.NET8**: ![.NET Version](https://img.shields.io/badge/.NET-8.0-blue) The framework used for building the Web API.
- **JWT(JSON Web Token)**: Authentication user.
- **Entity Framework Core**: ORM for interacting with Database.
- **ASP.NET Core**: Web API framework.

## Learn More

Check out my detailed tutorial below:

1. **[Implementation JWT with ASP.NET Core](https://maydayxi.github.io/MyDevLog/posts/asp-dot-net-core-jwt-tutorial/)**
2. **[Implementation JWT Refresh Token with ASP.NET Core](https://maydayxi.github.io/MyDevLog/posts/asp-net-core-jwt-tutorial-refresh-token/)**