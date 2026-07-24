using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RetailInventory;
using RetailInventory.Models;

var command = args.FirstOrDefault()?.Trim().ToLowerInvariant();

if (string.IsNullOrWhiteSpace(command))
{
    PrintIntroduction();
    PrintCommands();
    return;
}

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .Build();

var connectionString = configuration.GetConnectionString("RetailInventory")
    ?? "Data Source=RetailInventory.db";

using var context = new AppDbContext(connectionString);

switch (command)
{
    case "explain":
        PrintIntroduction();
        break;
    case "seed":
        await SeedAsync(context);
        break;
    case "list":
        await ListProductsAsync(context);
        break;
    case "find":
        await FindProductAsync(context);
        break;
    case "expensive":
        await FindExpensiveProductAsync(context);
        break;
    case "migrate":
        Console.WriteLine("dotnet tool install --global dotnet-ef");
        Console.WriteLine("dotnet ef migrations add InitialCreate");
        Console.WriteLine("dotnet ef database update");
        Console.WriteLine("SQLite database file: RetailInventory.db");
        break;
    default:
        Console.WriteLine($"Unknown command: {command}");
        PrintCommands();
        break;
}

static void PrintIntroduction()
{
    Console.WriteLine("EF Core 8.0 Retail Inventory Lab");
    Console.WriteLine();
    Console.WriteLine("ORM maps C# classes to database tables so your code works with objects instead of raw SQL everywhere.");
    Console.WriteLine("Benefits: faster development, easier maintenance, and a cleaner abstraction over relational data.");
    Console.WriteLine();
    Console.WriteLine("EF Core vs EF6:");
    Console.WriteLine("- EF Core is cross-platform, lighter, and optimized for modern .NET features.");
    Console.WriteLine("- EF6 is older and Windows-focused, while EF Core is the active, flexible line.");
    Console.WriteLine();
    Console.WriteLine("EF Core 8.0 highlights:");
    Console.WriteLine("- JSON column mapping");
    Console.WriteLine("- Better compiled model performance");
    Console.WriteLine("- Interceptors and improved bulk scenarios");
    Console.WriteLine();
    Console.WriteLine("Screenshot checkpoint: capture this console output after the explanation step.");
}

static void PrintCommands()
{
    Console.WriteLine();
    Console.WriteLine("Available commands:");
    Console.WriteLine("  dotnet run -- explain   # Lab 1 summary");
    Console.WriteLine("  dotnet run -- migrate   # Lab 3 migration commands");
    Console.WriteLine("  dotnet run -- seed      # Lab 4 insert initial data");
    Console.WriteLine("  dotnet run -- list      # Lab 5 retrieve all products");
    Console.WriteLine("  dotnet run -- find      # Lab 5 find by ID");
    Console.WriteLine("  dotnet run -- expensive # Lab 5 first product above 50000");
}

static async Task SeedAsync(AppDbContext context)
{
    await context.Database.MigrateAsync();

    if (await context.Categories.AnyAsync())
    {
        Console.WriteLine("Seed skipped: data already exists.");
        return;
    }

    var electronics = new Category { Name = "Electronics" };
    var groceries = new Category { Name = "Groceries" };

    await context.Categories.AddRangeAsync(electronics, groceries);

    var product1 = new Product { Name = "Laptop", Price = 75000, Category = electronics };
    var product2 = new Product { Name = "Rice Bag", Price = 1200, Category = groceries };

    await context.Products.AddRangeAsync(product1, product2);
    await context.SaveChangesAsync();

    Console.WriteLine("Seed complete: Electronics, Groceries, Laptop, and Rice Bag were inserted.");
    Console.WriteLine("Screenshot checkpoint: capture this console output after the seed step.");
}

static async Task ListProductsAsync(AppDbContext context)
{
    var products = await context.Products
        .AsNoTracking()
        .OrderBy(product => product.Id)
        .ToListAsync();

    if (products.Count == 0)
    {
        Console.WriteLine("No products found. Run the seed step first.");
        return;
    }

    foreach (var product in products)
    {
        Console.WriteLine($"{product.Name} - ₹{product.Price}");
    }

    Console.WriteLine("Screenshot checkpoint: capture this product list output.");
}

static async Task FindProductAsync(AppDbContext context)
{
    var firstProduct = await context.Products
        .AsNoTracking()
        .OrderBy(product => product.Id)
        .FirstOrDefaultAsync();

    if (firstProduct is null)
    {
        Console.WriteLine("No products found. Run the seed step first.");
        return;
    }

    var product = await context.Products.FindAsync(firstProduct.Id);
    Console.WriteLine($"Found: {product?.Name}");
    Console.WriteLine("Screenshot checkpoint: capture this FindAsync result.");
}

static async Task FindExpensiveProductAsync(AppDbContext context)
{
    var expensive = await context.Products
        .AsNoTracking()
        .FirstOrDefaultAsync(product => product.Price > 50000);

    Console.WriteLine($"Expensive: {expensive?.Name}");
    Console.WriteLine("Screenshot checkpoint: capture this FirstOrDefaultAsync result.");
}
