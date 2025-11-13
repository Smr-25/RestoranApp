using RestaurantApp.PL.Helpers;

var serviceProvider = ConfigureServices();
var menuExecutor = serviceProvider.GetRequiredService<MenuItemExecutor>();
var orderExecutor = serviceProvider.GetRequiredService<OrderExecutor>();
var categoryExecutor = serviceProvider.GetRequiredService<CategoryExecutor>();

while (true)
{
    ShowMainMenu();
    Choice:
    var choiceInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(choiceInput))
    {
        ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
        goto Choice;
    }

    if (!int.TryParse(choiceInput, out var choice))
    {
        ConsoleHelper.WriteError("Yanlış format! Yenidən cəhd edin.");
        goto Choice;
    }

    switch (choice)
    {
        case (int)MainMenuChoice.MenuOperations:
            await HandleMenuOperations(menuExecutor);
            break;
        case (int)MainMenuChoice.CategoryOperations:
            await HandleCategoryOperations(categoryExecutor);
            break;
        case (int)MainMenuChoice.OrderOperations:
            await HandleOrderOperations(orderExecutor);
            break;
        case (int)MainMenuChoice.Exit:
            ConsoleHelper.WriteSuccess("Sistemdən çıxılır...");
            return;
        default:
            ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
            goto Choice;
    }
}

void ShowMainMenu()
{
    Console.WriteLine();
    ConsoleHelper.WriteHeader(new string('=', 50));
    ConsoleHelper.WriteHeader("   RESTORAN İDARƏETMƏ SİSTEMİ");
    ConsoleHelper.WriteHeader(new string('=', 50));
    ConsoleHelper.WriteInfo("1. Menu üzərində əməliyyat aparmaq");
    ConsoleHelper.WriteInfo("2. Kateqoriyalar üzərində əməliyyat aparmaq");
    ConsoleHelper.WriteInfo("3. Sifarişlər üzərində əməliyyat aparmaq");
    ConsoleHelper.WriteInfo("0. Sistemdən çıxmaq");
    ConsoleHelper.WriteHeader(new string('=', 50));
    ConsoleHelper.WritePrompt("Seçiminiz: ");
}

async Task HandleMenuOperations(MenuItemExecutor executor)
{
    while (true)
    {
        executor.ShowMenuOperationsMenuAsync();
        Choice:
        var choiceInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(choiceInput))
        {
            ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
            goto Choice;
        }

        if (!int.TryParse(choiceInput, out var choice))
        {
            ConsoleHelper.WriteError("Yanlış format! Yenidən cəhd edin.");
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
            case (int)MenuChoice.ShowById:
                await executor.ExecuteShowMenuItemByIdAsync();
                break;
            case (int)MenuChoice.Exit:
                ConsoleHelper.WriteSuccess("Menu əməliyyatlarından çıxılır...");
                return;
            default:
                ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
                goto Choice;
        }
    }
}

async Task HandleOrderOperations(OrderExecutor executor)
{
    while (true)
    {
        executor.ShowOrderOperationsMenuAsync();
        Choice:
        var choiceInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(choiceInput))
        {
            ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
            goto Choice;
        }

        if (!int.TryParse(choiceInput, out int choice))
        {
            ConsoleHelper.WriteError("Yanlış format! Yenidən cəhd edin.");
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
                ConsoleHelper.WriteSuccess("Sifariş əməliyyatlarından çıxılır...");
                return;
            default:
                ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
                goto Choice;
        }
    }
}

async Task HandleCategoryOperations(CategoryExecutor executor)
{
    while (true)
    {
        executor.ShowCategoryOperationsMenuAsync();
        Choice:
        var choiceInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(choiceInput))
        {
            ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
            goto Choice;
        }

        if (!int.TryParse(choiceInput, out int choice))
        {
            ConsoleHelper.WriteError("Yanlış format! Yenidən cəhd edin.");
            goto Choice;
        }

        switch (choice)
        {
            case (int)CategoryChoice.Add:
                await executor.ExecuteAddCategoryAsync();
                break;
            case (int)CategoryChoice.Edit:
                await executor.ExecuteEditCategoryAsync();
                break;
            case (int)CategoryChoice.Remove:
                await executor.ExecuteRemoveCategoryAsync();
                break;
            case (int)CategoryChoice.ShowAll:
                await executor.ExecuteShowAllCategoriesAsync();
                break;
            case (int)CategoryChoice.ShowById:
                await executor.ExecuteShowCategoryByIdAsync();
                break;
            case (int)CategoryChoice.Exit:
                ConsoleHelper.WriteSuccess("Kateqoriya əməliyyatlarından çıxılır...");
                return;
            default:
                ConsoleHelper.WriteError("Yanlış seçim! Yenidən cəhd edin.");
                goto Choice;
        }
    }
}

IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    services.AddAutoMapper(_ => { }, typeof(MappingProfile).Assembly);

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddDbContext<RestaurantDbContext>(options =>
        options.UseSqlServer(
            "Server=MOON01\\SQLEXPRESS;Database=RestaurantAppDb;Trusted_Connection=True;TrustServerCertificate=True"));
    services.AddScoped<IMenuItemService, MenuItemService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddLogging();
    services.AddScoped<MenuItemExecutor>();
    services.AddScoped<OrderExecutor>();
    services.AddScoped<CategoryExecutor>();
    return services.BuildServiceProvider();
}