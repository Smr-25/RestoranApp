# 🍽️ RestoranApp

`RestoranApp` is a N-tier (layered) architecture ASP.NET Core MVC restaurant management application built on **.NET 10**. The project provides robust functionalities for menu management and order tracking.

---

## 📋 Table of Contents
- [About the Project](#-about-the-project)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Features](#-features)
- [Prerequisites](#-prerequisites)
- [Getting Started](#-getting-started)
- [Database Configuration & Migrations](#-database-configuration--migrations)
- [Configuration](#-configuration)
- [Project Structure](#-project-structure)
- [Build and Test](#-build-and-test)
- [Known Issues](#-known-issues)

---

## 💡 About the Project
This application is designed to streamline and automate daily restaurant operations:
- **Menu Management:** Full CRUD operations (Add / Edit / Delete menu items).
- **Advanced Menu Filtering:** Search by category, price range, and item name.
- **Order Management:** Create and cancel orders dynamically.
- **Advanced Order Filtering:** Filter orders by date range, total amount range, or exact date.
- **Detailed Order Tracking:** Comprehensive breakdown of items, quantities, subtotal, and total amount.

---

## 🛠️ Tech Stack
- **Framework:** .NET 10 (`net10.0`)
- **UI/Pattern:** ASP.NET Core MVC
- **ORM:** Entity Framework Core 10 Preview
- **Database Provider:** SQL Server (via EF Core)
- **Object Mapping:** AutoMapper
- **Frontend Assets:** Bootstrap + jQuery

---

## 🏗️ Architecture
The project is decoupled into **4 main layers** following industry best practices:

1. **`RestaurantApp.Core` (Domain Layer)**
   - Domain models (`Category`, `MenuItem`, `Order`, `OrderItem`)
   - Reusable infrastructure structures (`BaseEntity`)

2. **`RestaurantApp.DDL` (Data Delivery/Access Layer)**
   - `RestaurantDbContext` configuration
   - Fluent API Entity configurations
   - Generic Repository Pattern (`IRepository<T>`, `Repository<T>`)
   - EF Core Migrations and Seed Data setup

3. **`RestaurantApp.BBL` (Business Logic Layer)**
   - Business service interfaces and concrete implementations
   - Data Transfer Objects (DTOs)
   - Custom Core Exceptions (`EntityNotFoundException`, `EntityAlreadyExistException`, `CountZeroException`)
   - AutoMapper profile mapping configurations

4. **`RestoranApplication.PL` (Presentation Layer)**
   - ASP.NET Core MVC Controllers and Razor Views
   - Dependency Injection (DI) registry configuration (`Program.cs`)
   - User Interface and system presentation flows

---

## 🚀 Features

### 🍔 Menu Operations
* Create brand new menu entries with image/details
* Update existing menu item records
* Safe deletion of menu items
* List all products with high responsiveness
* Real-time filters (Category, Price bounds, Name search)

### 📦 Order Operations
* Initialize new orders handling multiple items per line
* Cancel active orders gracefully
* Fetch global order list summaries
* Track down entries via dynamic range filters (Time window, Price constraints, Specific date match)
* Deep dive view into specific order details using Unique ID/No

---

## ⚙️ Prerequisites

### 1) .NET SDK
This codebase explicitly targets `net10.0`. Ensure you have the appropriate SDK installed:
* .NET 10 SDK (Preview versions are accepted).

Verify your setup by running:
```bash
dotnet --info
2) SQL Server
The system relies on the EF Core SQL Server provider. A local instance of SQL Server or SQL Server Express is highly recommended.

3) EF Core CLI Tool (Optional, required for executing migrations manually)
Bash
dotnet tool install --global dotnet-ef
🏃 Getting Started
Navigate to the repository root directory:

Bash
cd /home/runner/work/RestoranApp/RestoranApp
Restore project dependencies:

Bash
dotnet restore RestaurantApp.sln


3. **Spin up the Presentation Layer Application:**
   ```bash
   dotnet run --project /home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/RestoranApplication.PL.csproj
   
🌐 Default Launch URLs (launchSettings.json):
HTTP: http://localhost:5113

HTTPS: https://localhost:7189

🗄️ Database Configuration & Migrations
Apply existing Entity Framework migrations to your configured database context using the following command:

Bash
dotnet ef database update \
  --project /home/runner/work/RestoranApp/RestoranApp/RestaurantApp.DDL/RestaurantApp.DDL.csproj \
  --startup-project /home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/RestoranApplication.PL.csproj
🌱 Seed Data
The database initialization automatically seeds essential structural parameters:

Default Categories: Soups, Main Courses, Drinks, Desserts

Initial placeholder menu items are pre-populated for demonstration.

🔧 Configuration
The Program.cs file expects a valid DefaultConnection connection string inside configuration file layers.

Incorporate the target connection block into your RestoranApplication.PL/appsettings.Development.json (or any equivalent environment configuration matrix):

JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RestoranAppDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
📝 Note: Program.cs is natively configured to safely scan an optional appsettings.Mac.json environment file. If you are developing on macOS environment ecosystems, feel free to isolate configurations into that target block.

📁 Project Structure
Plaintext
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
🧪 Build and Test
Execute structural tests and solution builds directly from the solution root:

Bash
# Compile solution binaries
dotnet build RestaurantApp.sln

# Run target validation test suits
dotnet test RestaurantApp.sln
⚠️ Known Issues
Certain baseline transient dependencies (AutoMapper 12.0.1) might flag a temporary GitHub software security advisory warning.

Because this solution targets cutting-edge preview tech (.NET 10 + EF Core 10 Preview), targeted production runtime deployment environments must strictly replicate the matching SDK preview releases.
