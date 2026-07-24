using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;

namespace RetailInventory;

public class AppDbContext : DbContext
{
    private readonly string? _connectionString;

    public AppDbContext(string? connectionString = null)
    {
        _connectionString = connectionString;
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_connectionString ?? "Data Source=RetailInventory.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(category => category.Id);
            entity.Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(product => product.Id);
            entity.Property(product => product.Name)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(product => product.Price)
                .HasColumnType("decimal(18,2)");
        });
    }
}
