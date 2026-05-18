# RestoranApp (.NET 10)

RestoranApp is a layered ASP.NET Core MVC restaurant management application built with **.NET 10** and **Entity Framework Core 10 (preview)**.

It lets you manage:
- **Menu items** (create, list, edit, delete)
- **Orders** (create, list, details, delete)
- Filtering by category, price range, date range, and search text

---

## Tech Stack

- **.NET SDK:** 10.x
- **Framework:** ASP.NET Core MVC (`net10.0`)
- **Data Access:** Entity Framework Core 10 (preview)
- **Database:** SQL Server
- **Object Mapping:** AutoMapper
- **UI:** Razor Views + Bootstrap

---

## Solution Structure

The solution uses a clean layered architecture:

- `RestaurantApp.Core`  
  Domain entities and base models.

- `RestaurantApp.DDL`  
  Data layer (DbContext, EF Core configurations, repositories, migrations).

- `RestaurantApp.BBL`  
  Business logic layer (services, DTOs, mapping profiles, custom exceptions).

- `RestoranApplication.PL`  
  Presentation layer (MVC controllers, views, static assets, app startup).

---

## Prerequisites

Before running the app, make sure you have:

1. **.NET 10 SDK** installed
2. **SQL Server** instance available
3. **EF Core CLI tools** (optional but recommended):

```bash
dotnet tool install --global dotnet-ef
```

---

## Configuration

Set your SQL Server connection string using `DefaultConnection`.

You can add it to:
- `/home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/appsettings.Development.json`
- or `/home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/appsettings.Mac.json` (this file is optionally loaded in `Program.cs`)

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=RestaurantAppDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

## Database Setup

Migrations already exist in `RestaurantApp.DDL/Migrations` and include seed data (categories/menu items).

Apply migrations:

```bash
dotnet ef database update --project RestaurantApp.DDL --startup-project RestoranApplication.PL
```

---

## Run the Application

From the repository root:

```bash
dotnet restore
dotnet build RestaurantApp.sln
dotnet run --project RestoranApplication.PL
```

Then open the URL shown in the console (usually `https://localhost:xxxx`).

---

## Main Functional Modules

### Menu Management
- Create menu items by category
- Edit and delete menu items
- List with filters:
  - Category
  - Price interval
  - Name search

### Order Management
- Create orders with multiple order items
- Automatic validation for invalid counts or missing entities
- List with filters:
  - Date interval
  - Exact date
  - Total amount interval
- View order details and delete orders

---

## Useful Commands

```bash
# Build
dotnet build RestaurantApp.sln

# Run tests (currently no test project; command is still valid)
dotnet test RestaurantApp.sln

# Run app
dotnet run --project RestoranApplication.PL
```

---

## Notes

- The project is targeting **.NET 10** in all layers.
- EF Core packages are currently **10.0.0-preview** versions.
- If SQL Server connection is not configured, the app cannot start database operations.

