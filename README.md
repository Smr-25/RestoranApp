# 🍽️ RestoranApp

RestoranApp is a modern ASP.NET Core MVC restaurant management application built with **.NET 10** using a clean layered architecture approach.

The project provides menu management and order management functionalities designed to simplify restaurant operations.

---

# 📌 Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [Database & EF Core Migrations](#database--ef-core-migrations)
- [Configuration](#configuration)
- [Project Structure](#project-structure)
- [Build & Testing](#build--testing)
- [Known Issues](#known-issues)

---

# 📖 Overview

RestoranApp is designed to streamline restaurant workflows through a clean and maintainable architecture.

The application includes:

- Menu management
- Product filtering & searching
- Order management
- Order detail tracking
- Dynamic filtering options

---

# ⚙️ Tech Stack

| Technology | Description |
|------------|-------------|
| .NET 10 | ASP.NET Core MVC Framework |
| Entity Framework Core 10 Preview | ORM |
| SQL Server | Database Provider |
| AutoMapper | DTO & object mapping |
| Bootstrap + jQuery | Frontend UI |

---

# 🏗️ Architecture

The solution follows a 4-layer architecture:

---

## 🔹 RestaurantApp.Core

Contains:

- Domain entities
- Base models
- Shared abstractions

Main entities:

- `Category`
- `MenuItem`
- `Order`
- `OrderItem`
- `BaseEntity`

---

## 🔹 RestaurantApp.DDL

Responsible for:

- `RestaurantDbContext`
- Entity configurations
- Generic Repository Pattern
- Migrations & seed data

Includes:

- `IRepository<T>`
- `Repository<T>`

---

## 🔹 RestaurantApp.BBL

Contains:

- Business services
- DTOs
- Interfaces
- AutoMapper profiles
- Custom exception classes

Custom exceptions:

- `EntityNotFoundException`
- `EntityAlreadyExistException`
- `CountZeroException`

---

## 🔹 RestoranApplication.PL

Responsible for:

- MVC Controllers & Views
- Dependency Injection configuration
- UI rendering
- User interaction flow

Application startup is configured inside:

```text
Program.cs
```

---

# ✨ Features

## 🍴 Menu Operations

- Create menu items
- Update existing products
- Delete products
- List all menu items
- Category filtering
- Price range filtering
- Search by product name

---

## 🧾 Order Operations

- Create new orders
- Cancel orders
- List all orders
- Filter by date range
- Filter by amount range
- Filter by exact date
- View order details by ID/Number

---

# 📋 Requirements

Before running the project, make sure you have:

- **.NET 10 SDK**
- **SQL Server**
- Optional: EF Core CLI Tool

Verify SDK installation:

```bash
dotnet --info
```

Install EF CLI tool (optional):

```bash
dotnet tool install --global dotnet-ef
```

---

# 🚀 Getting Started

Navigate to the repository root:

```bash
cd /home/runner/work/RestoranApp/RestoranApp
```

Restore dependencies:

```bash
dotnet restore RestaurantApp.sln
```

Run the application:

```bash
dotnet run --project /home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/RestoranApplication.PL.csproj
```

Default launch URLs:

```text
http://localhost:5113
https://localhost:7189
```

---

# 🗄️ Database & EF Core Migrations

Apply existing migrations to the database:

```bash
dotnet ef database update \
  --project /home/runner/work/RestoranApp/RestoranApp/RestaurantApp.DDL/RestaurantApp.DDL.csproj \
  --startup-project /home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/RestoranApplication.PL.csproj
```

---

## 🌱 Seed Data

Initial seed data includes:

### Categories

- Soups
- Main Courses
- Drinks
- Desserts

### Sample Menu Items

Several sample products are automatically inserted during migration.

---

# ⚡ Configuration

The application expects a SQL Server connection string named:

```text
DefaultConnection
```

Add the following configuration inside:

```text
RestoranApplication.PL/appsettings.Development.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RestoranAppDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

## Optional Local Configuration

`Program.cs` also loads:

```text
appsettings.Mac.json
```

as an optional configuration source.

You may create this file for local environment overrides.

---

# 📂 Project Structure

```text
RestoranApp/
├── RestaurantApp.sln
├── RestaurantApp.Core/
│   ├── Common/
│   └── Models/
├── RestaurantApp.DDL/
│   ├── Data/
│   ├── Repositories/
│   └── Migrations/
├── RestaurantApp.BBL/
│   ├── Dtos/
│   ├── Interfaces/
│   ├── Services/
│   ├── Profiles/
│   └── Exceptions/
└── RestoranApplication.PL/
    ├── Controllers/
    ├── Views/
    ├── wwwroot/
    └── Program.cs
```

---

# 🧑‍💻 Build & Testing

Run from the repository root:

## Build

```bash
dotnet build RestaurantApp.sln
```

## Run Tests

```bash
dotnet test RestaurantApp.sln
```

---

# ⚠️ Known Issues

- Some dependencies such as `AutoMapper 12.0.1` may currently show GitHub advisory warnings
- Since the project uses **.NET 10** and **EF Core 10 Preview**, matching SDK versions are important for stable environments

---

# 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Open a Pull Request

---

# 📄 License

This project is intended for educational and demonstration purposes.
