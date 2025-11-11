var serviceProvider = ConfigureServices();

var menuExecutor = serviceProvider.GetRequiredService<MenuItemExecutor>();
var orderExecutor = serviceProvider.GetRequiredService<OrderExecutor>();

while (true)
{
    ShowMainMenu();
    Choice:
    var choiceInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(choiceInput))
    {
        Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
        goto Choice;
    }

    if (!int.TryParse(choiceInput, out var choice))
    {
        Console.WriteLine("Yanlış format! Yenidən cəhd edin.");
        goto Choice;
    }

    switch (choice)
    {
        case (int)MainMenuChoice.MenuOperations:
            await HandleMenuOperations(menuExecutor);
            break;
        case (int)MainMenuChoice.OrderOperations:
            await HandleOrderOperations(orderExecutor);
            break;
        case (int)MainMenuChoice.Exit:
            Console.WriteLine("Sistemdən çıxılır...");
            return;
        default:
            Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
            goto Choice;
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
    while (true)
    {
        await executor.ShowMenuOperationsMenuAsync();
        Choice:
        var choiceInput = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(choiceInput))
        {
            Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
            goto Choice;
        }
        if(!int.TryParse(choiceInput, out var choice))
        {
            Console.WriteLine("Yanlış format! Yenidən cəhd edin.");
            goto Choice;
        }

        switch (choice)
        {
            case (int)MenuChoice.Add:
                await executor.ExecuteAddMenuItemAsync();
                break;
            case (int)MenuChoice.Edit:
                await executor.ExecuteEditMenuItemAsync();
                break;
            case (int)MenuChoice.Remove:
                await executor.ExecuteRemoveMenuItemAsync();
                break;
            case (int)MenuChoice.ShowAll:
                await executor.ExecuteShowAllMenuItemsAsync();
                break;
            case (int)MenuChoice.ShowByCategory:
                await executor.ExecuteShowMenuItemsByCategoryAsync();
                break;
            case (int)MenuChoice.ShowByPriceRange:
                await executor.ExecuteShowMenuItemsByPriceRangeAsync();
                break;
            case (int)MenuChoice.Search:
                await executor.ExecuteSearchMenuItemsAsync();
                break;
            case (int)MenuChoice.Exit:
                Console.WriteLine("Menu əməliyyatlarından çıxılır...");
                return;
            default:
                Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                goto Choice;
        }
    }
}



async Task HandleOrderOperations(OrderExecutor executor)
{
    while (true)
    {
        await executor.ShowOrderOperationsMenuAsync();
        Choice:
        var choiceInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(choiceInput))
        {
            Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
            goto Choice;
        }

        if (!int.TryParse(choiceInput, out int choice))
        {
            Console.WriteLine("Yanlış format! Yenidən cəhd edin.");
            goto Choice;
        }
        switch (choice)
        {
            case (int)OrderChoice.Add:
                await executor.ExecuteAddOrderAsync();
                break;
            case (int)OrderChoice.Remove:
                await executor.ExecuteRemoveOrderAsync();
                break;
            case (int)OrderChoice.ShowAll:
                await executor.ExecuteShowAllOrdersAsync();
                break;
            case (int)OrderChoice.ShowByDateRange:
                await executor.ExecuteShowOrdersByDateRangeAsync();
                break;
            case (int)OrderChoice.ShowByPriceRange:
                await executor.ExecuteShowOrdersByPriceRangeAsync();
                break;
            case (int)OrderChoice.ShowByDate:
                await executor.ExecuteShowOrdersByDateAsync();
                break;
            case (int)OrderChoice.ShowByNo:
                await executor.ExecuteShowOrderByNoAsync();
                break;
            case (int)OrderChoice.Exit:
                Console.WriteLine("Sifariş əməliyyatlarından çıxılır...");
                return;
            default:
                Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                goto Choice;
        }
    }
}



IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    services.AddAutoMapper(_ => { }, typeof(MappingProfile).Assembly);

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer("Server=.;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True"));
    services.AddScoped<IMenuItemService, MenuItemService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddLogging();
    services.AddScoped<MenuItemExecutor>();
    services.AddScoped<OrderExecutor>();

    return services.BuildServiceProvider();
}

// Console.OutputEncoding = System.Text.Encoding.UTF8;
// Console.InputEncoding = System.Text.Encoding.UTF8;
