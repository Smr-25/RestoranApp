# RestoranApp

`RestoranApp` .NET 10 üzərində qurulmuş, qatlı arxitekturaya sahib ASP.NET Core MVC restoran idarəetmə tətbiqidir. Layihə menyu idarəsi və sifariş idarəsi funksiyalarını təqdim edir.

## Mündəricat
- [Layihə haqqında](#layihə-haqqında)
- [Texnologiyalar](#texnologiyalar)
- [Arxitektura](#arxitektura)
- [Funksionallıq](#funksionallıq)
- [Quraşdırma tələbləri](#quraşdırma-tələbləri)
- [Layihəni işə salmaq](#layihəni-işə-salmaq)
- [Verilənlər bazası (EF Core Migrations)](#verilənlər-bazası-ef-core-migrations)
- [Konfiqurasiya](#konfiqurasiya)
- [Layihə strukturu](#layihə-strukturu)
- [Build və Test](#build-və-test)
- [Known məsələlər](#known-məsələlər)

## Layihə haqqında
Bu tətbiq restoran əməliyyatlarını sadələşdirmək üçün hazırlanıb:
- **Menyu məhsulu əlavə et / redaktə et / sil**
- **Menyu siyahısı və filterlər** (kateqoriya, qiymət intervalı, ad üzrə axtarış)
- **Sifariş yarat / ləğv et**
- **Sifariş siyahısı və filterlər** (tarix intervalı, məbləğ intervalı, dəqiq tarix)
- **Sifariş detalları** (məhsullar, say, subtotal, ümumi məbləğ)

## Texnologiyalar
- **.NET 10 (`net10.0`)**
- **ASP.NET Core MVC**
- **Entity Framework Core 10 Preview**
- **SQL Server provider (EF Core)**
- **AutoMapper**
- **Bootstrap + jQuery**

## Arxitektura
Layihə 4 əsas qatdan ibarətdir:

1. **RestaurantApp.Core**
   - Domen modelləri (`Category`, `MenuItem`, `Order`, `OrderItem`)
   - `BaseEntity`

2. **RestaurantApp.DDL**
   - `RestaurantDbContext`
   - Entity konfiqurasiyaları
   - Generic repository (`IRepository<T>`, `Repository<T>`)
   - Migrations və seed data

3. **RestaurantApp.BBL**
   - Business servis interfeysləri və implementasiyaları
   - DTO-lar
   - Xəta sinifləri (`EntityNotFoundException`, `EntityAlreadyExistException`, `CountZeroException`)
   - AutoMapper profili

4. **RestoranApplication.PL**
   - MVC Controller + View qatları
   - DI konfiqurasiyası (`Program.cs`)
   - UI və istifadəçi axını

## Funksionallıq

### Menyu əməliyyatları
- Yeni menyu məhsulu əlavə etmə
- Mövcud məhsulu redaktə etmə
- Məhsulu silmə
- Bütün məhsulları göstərmə
- Kateqoriyaya görə filtr
- Qiymət intervalına görə filtr
- Ada görə axtarış

### Sifariş əməliyyatları
- Yeni sifariş yaratma (birdən çox məhsul sətri)
- Sifarişi ləğv etmə
- Bütün sifarişləri göstərmə
- Tarix intervalına görə filtr
- Məbləğ intervalına görə filtr
- Dəqiq tarixə görə filtr
- ID/No ilə sifariş detallarına baxış

## Quraşdırma tələbləri

### 1) .NET SDK
Layihə `net10.0` target edir. Buna görə:
- .NET 10 SDK (preview ola bilər) qurulu olmalıdır.

Yoxlama:
```bash
dotnet --info
```

### 2) SQL Server
EF Core SQL Server provider istifadə olunur. Lokal SQL Server və ya SQL Server Express kifayətdir.

### 3) EF CLI (opsional, migration üçün)
```bash
dotnet tool install --global dotnet-ef
```

## Layihəni işə salmaq

Repository root-a keçin:
```bash
cd /home/runner/work/RestoranApp/RestoranApp
```

Asılılıqları bərpa edin:
```bash
dotnet restore RestaurantApp.sln
```

Tətbiqi başladın:
```bash
dotnet run --project /home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/RestoranApplication.PL.csproj
```

Default launch URL-ləri (`launchSettings.json`):
- `http://localhost:5113`
- `https://localhost:7189`

## Verilənlər bazası (EF Core Migrations)

Mövcud migration-ları DB-yə tətbiq edin:
```bash
dotnet ef database update \
  --project /home/runner/work/RestoranApp/RestoranApp/RestaurantApp.DDL/RestaurantApp.DDL.csproj \
  --startup-project /home/runner/work/RestoranApp/RestoranApp/RestoranApplication.PL/RestoranApplication.PL.csproj
```

Migrations daxilində seed data mövcuddur:
- Kateqoriyalar: `Soups`, `Main Courses`, `Drinks`, `Desserts`
- Nümunə menyu məhsulları da ilkin olaraq əlavə olunur.

## Konfiqurasiya

`Program.cs` SQL Server üçün `DefaultConnection` connection string-i gözləyir.

`RestoranApplication.PL/appsettings.Development.json` (və ya istifadə etdiyiniz mühit faylı) daxilinə əlavə edin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RestoranAppDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Qeyd: `Program.cs` daxilində `appsettings.Mac.json` optional şəkildə oxunur. İstəsəniz həmin faylı da yarada bilərsiniz.

## Layihə strukturu

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

## Build və Test
Repository root-dan:

```bash
dotnet build RestaurantApp.sln
dotnet test RestaurantApp.sln
```

## Known məsələlər
- Bəzi dependency-lər (`AutoMapper 12.0.1`) üçün hazırda GitHub advisory warning görünə bilər.
- Layihə .NET 10 + EF Core 10 Preview istifadə etdiyi üçün stabil release mühitlərində uyğun SDK versiyası vacibdir.


