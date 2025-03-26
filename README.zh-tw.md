🌏 [English](/README.md)

# JWT-Authentication-API

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)

這是一個使用 .NET 8 建立的 ASP.NET Web API 專案，並實作了 JSON Web Token (JWT) 驗證機制。
專案的目的是展示如何在 Web API 中進行身份驗證，並利用 JWT 來確保 API 的安全性。

## 目錄

- [專案介紹](#專案介紹)
- [功能特性](#功能特性)
- [安裝與設定](#安裝與設定)
- [使用說明](#使用說明)
- [使用技術](#使用技術)
- [更多資訊](#更多資訊)

## 專案介紹

本專案展示如何使用 .NET 8 的 ASP.NET Web API 建立一個簡單的後端服務，並加入 JWT 驗證來保護 API 路徑

- 使用者註冊、登入、登出
- 為每個請求提供 JWT 及有效的身份驗證機制
- 只允許已驗證的用戶訪問受保護的資源

## 功能特性

- **用戶註冊與登入**：提供用戶註冊和登入的功能，並生成 JWT token。
- **JWT 驗證**：使用 JWT token 來保護 API 路徑，確保只有經過驗證的用戶才能訪問。

## 安裝與設定

### 1. 安裝 .NET8 SDK

確保你已經安裝 .NET 8 SDK。如果尚未安裝，可以從 [Microsoft 官方網站](https://dotnet.microsoft.com/download/dotnet/8.0) 下載並安裝。

### 2. 下載專案

```bash
git clone https://github.com/maydayXi/JWT-Authentication-API.git
```

### 3. 建立資料庫

在 `appsettings.json` 中加入你的資料庫連線字串

```json
{
  "ConnectionStrings": {
    "JwtAuthDB": "Server=yourdbserverip,1433;Database=JwtAuthDB;user=yourdbuser;password=yourdbpassword;TrustServerCertificate=True;"
  }
}
```

如果是使用本機資料庫

```json
{
  "ConnectionStrings": {
    "JwtAuthDB": "Server=yourdbserverip,1433;Database=JwtAuthDB;user=yourdbuser;password=yourdbpassword;TrustServerCertificate=True;"
  }
}
```

可以到我的教學看如何使用 Rider 建立資料庫

1. **[新增 Migration](https://maydayxi.github.io/MyDevLog/posts/asp-dot-net-core-jwt-tutorial/#%E6%96%B0%E5%A2%9E-migration)**
2. **[更新資料庫](https://maydayxi.github.io/MyDevLog/posts/asp-dot-net-core-jwt-tutorial/#%E6%9B%B4%E6%96%B0%E8%B3%87%E6%96%99%E5%BA%AB)**

## 使用說明

專案使用 **[Postman](https://www.postman.com/)**

### 1. 使用者註冊

請求範例
```json
{
  "email": "your email",
  "password": "your password"
}
```

沒有提供帳號及密碼
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-empty-test.png" alt="Empty">

沒有提供密碼
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-no-password-test.png" alt="No Password">

註冊成功
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-register-test.png" alt="registration">

### 2. 使用者登入

請求範例
```json
{
  "email": "your email",
  "password": "your password"
}
```

登入成功
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-login-success.png" alt="Login success">

登入失敗
<img src="https://cdn.jsdelivr.net/gh/maydayXi/MyDevLog@main/content/posts/jwt-tutorial/postman-login-failed-test.png" alt="Login failed">

## 使用技術

- **.NET8**: ![.NET Version](https://img.shields.io/badge/.NET-8.0-blue) 用於建立 Web API。
- **JTW(JSON Web Token)**: 用戶身份驗證。
- **Entity Framework Core**: 資料庫操作。
- **ASP.NET Core**: Web API 框架

## 更多資訊

到我的教學網看更多資訊

1. **[Implementation JWT with ASP.NET Core](https://maydayxi.github.io/MyDevLog/posts/asp-dot-net-core-jwt-tutorial/)**