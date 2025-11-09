using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApp.BBL.DTOs;
using RestaurantApp.BBL.Interfaces;
using RestaurantApp.BBL.Services;
using RestaurantApp.DDL.Data;
using RestaurantApp.DDL.Repostories.Intefaces;
using RestaurantApp.DDL.Repostories.Concretes;
using RestaurantApp.PL.Executors;

var serviceProvider = ConfigureServices();

var menuExecutor = serviceProvider.GetRequiredService<MenuItemExecutor>();
var orderExecutor = serviceProvider.GetRequiredService<OrderExecutor>();

bool running = true;

while (running)
{
    ShowMainMenu();
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await HandleMenuOperations(menuExecutor);
            break;
        case "2":
            await HandleOrderOperations(orderExecutor);
            break;
        case "0":
            running = false;
            Console.WriteLine("Sistemdən çıxılır...");
            break;
        default:
            Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
            break;
    }
}

void ShowMainMenu()
{
    Console.WriteLine("\n" + new string('=', 50));
    Console.WriteLine("   RESTORAN İDARƏETMƏ SİSTEMİ");
    Console.WriteLine(new string('=', 50));
    Console.WriteLine("1. Menu üzərində əməliyyat aparmaq");
    Console.WriteLine("2. Sifarişlər üzərində əməliyyat aparmaq");
    Console.WriteLine("0. Sistemdən çıxmaq");
    Console.WriteLine(new string('=', 50));
    Console.Write("Seçiminiz: ");
}

async Task HandleMenuOperations(MenuItemExecutor executor)
{
    bool back = false;
    
    while (!back)
    {
        ShowMenuOperationsMenu();
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await executor.AddMenuItemAsync();
                break;
            case "2":
                await executor.EditMenuItemAsync();
                break;
            case "3":
                await executor.RemoveMenuItemAsync();
                break;
            case "4":
                await executor.ShowAllMenuItemsAsync();
                break;
            case "5":
                await executor.ShowMenuItemsByCategoryAsync();
                break;
            case "6":
                await executor.ShowMenuItemsByPriceRangeAsync();
                break;
            case "7":
                await executor.SearchMenuItemsAsync();
                break;
            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                break;
        }
    }
}

void ShowMenuOperationsMenu()
{
    Console.WriteLine("\n" + new string('=', 50));
    Console.WriteLine("   MENU ƏMƏLİYYATLARI");
    Console.WriteLine(new string('=', 50));
    Console.WriteLine("1. Yeni item əlavə et");
    Console.WriteLine("2. Item üzərində düzəliş et");
    Console.WriteLine("3. Item sil");
    Console.WriteLine("4. Bütün item-ları göstər");
    Console.WriteLine("5. Kateqoriyasına görə menu item-ları göstər");
    Console.WriteLine("6. Qiymət aralığına görə menu item-lar göstər");
    Console.WriteLine("7. Menu item-lar arasında ada görə axtarış et");
    Console.WriteLine("0. Əvvəlki menyuya qayıt");
    Console.WriteLine(new string('=', 50));
    Console.Write("Seçiminiz: ");
}

async Task HandleOrderOperations(OrderExecutor executor)
{
    bool back = false;
    
    while (!back)
    {
        ShowOrderOperationsMenu();
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await executor.AddOrderAsync();
                break;
            case "2":
                await executor.RemoveOrderAsync();
                break;
            case "3":
                await executor.ShowAllOrdersAsync();
                break;
            case "4":
                await executor.ShowOrdersByDateRangeAsync();
                break;
            case "5":
                await executor.ShowOrdersByPriceRangeAsync();
                break;
            case "6":
                await executor.ShowOrdersByDateAsync();
                break;
            case "7":
                await executor.ShowOrderDetailAsync();
                break;
            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                break;
        }
    }
}

void ShowOrderOperationsMenu()
{
    Console.WriteLine("\n" + new string('=', 50));
    Console.WriteLine("   SİFARİŞ ƏMƏLİYYATLARI");
    Console.WriteLine(new string('=', 50));
    Console.WriteLine("1. Yeni sifariş əlavə et");
    Console.WriteLine("2. Sifarişin ləğvi");
    Console.WriteLine("3. Bütün sifarişlərin ekrana çıxarılması");
    Console.WriteLine("4. Tarix aralığına görə sifarişlərin göstərilməsi");
    Console.WriteLine("5. Məbləğ aralığına görə sifarişlərin göstərilməsi");
    Console.WriteLine("6. Bir tarixdə olan sifarişlərin göstərilməsi");
    Console.WriteLine("7. Nömrəyə əsasən sifarişin məlumatlarının göstərilməsi");
    Console.WriteLine("0. Əvvəlki menyuya qayıt");
    Console.WriteLine(new string('=', 50));
    Console.Write("Seçiminiz: ");
}

IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    services.AddDbContext<RestaurantDbContext>(options =>
        options.UseSqlServer("Server=localhost;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;"));

    services.AddAutoMapper(_ => { }, typeof(MappingProfile).Assembly);

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    
    services.AddScoped<IMenuItemService, MenuItemService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<ICategoryService, CategoryService>();

    services.AddScoped<MenuItemExecutor>();
    services.AddScoped<OrderExecutor>();

    return services.BuildServiceProvider();
}


