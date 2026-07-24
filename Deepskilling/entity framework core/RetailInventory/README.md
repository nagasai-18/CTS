# RetailInventory - EF Core 8.0 Lab

This workspace contains a small console app that demonstrates the core ideas from the labs using a local SQLite database:

- ORM basics and why EF Core helps
- DbContext and model configuration
- EF Core migration workflow
- Seeding initial data
- Reading data with `ToListAsync`, `FindAsync`, and `FirstOrDefaultAsync`

## What was created

- `RetailInventory.csproj`
- `Program.cs`
- `AppDbContext.cs`
- `Models/Category.cs`
- `Models/Product.cs`
- `appsettings.json`

## Lab flow

### Lab 1 - ORM and EF Core overview
Run:

```bash
dotnet run -- explain
```

Screenshot checkpoint: capture the console output after this command.

### Lab 2 - Database context and models
Review `AppDbContext.cs` and `Models/Category.cs` plus `Models/Product.cs`.

Screenshot checkpoint: capture the files in the editor after reviewing the model and context definitions.

### Lab 3 - Migrations
Run:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Screenshot checkpoint: capture the terminal after the migration and update commands succeed.

### Lab 4 - Insert initial data
Run:

```bash
dotnet run -- seed
```

Screenshot checkpoint: capture the console output after the seed command.

### Lab 5 - Retrieve data
Run these commands one at a time and capture each result:

```bash
dotnet run -- list
dotnet run -- find
dotnet run -- expensive
```

Screenshot checkpoints: capture the output after each command.

## Notes

- The app defaults to the connection string in `appsettings.json`.
- If you want a different SQL Server instance, update `ConnectionStrings:RetailInventory`.
- The `seed` command uses `Database.MigrateAsync()`, so run the migration step first.
- The database file is `RetailInventory.db` in the project folder.
