Set-Location $PSScriptRoot

Write-Host 'Lab 1: ORM overview'
Write-Host 'Run: dotnet run -- explain'
Write-Host 'Screenshot: capture the console output after the explanation.'
Write-Host ''

Write-Host 'Lab 3: EF Core CLI migration steps'
Write-Host 'Run: dotnet tool install --global dotnet-ef'
Write-Host 'Run: dotnet ef migrations add InitialCreate'
Write-Host 'Run: dotnet ef database update'
Write-Host 'SQLite file: RetailInventory.db'
Write-Host 'Screenshot: capture the terminal after the migration update succeeds.'
Write-Host ''

Write-Host 'Lab 4: Seed initial data'
Write-Host 'Run: dotnet run -- seed'
Write-Host 'Screenshot: capture the seed output.'
Write-Host ''

Write-Host 'Lab 5: Retrieve data'
Write-Host 'Run: dotnet run -- list'
Write-Host 'Run: dotnet run -- find'
Write-Host 'Run: dotnet run -- expensive'
Write-Host 'Screenshot: capture each command output separately.'
