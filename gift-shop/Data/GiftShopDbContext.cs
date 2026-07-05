using gift_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace gift_shop.Data;

public class GiftShopDbContext : DbContext
{
    public GiftShopDbContext(DbContextOptions<GiftShopDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Inventory> Inventories { get; set; } = null!;
    public DbSet<Coupon> Coupons { get; set; } = null!;
    public DbSet<Shipping> Shipping { get; set; } = null!;
    public DbSet<StockTransaction> StockTransactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>()
            .HasKey(u => u.RoleId);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Configure Role entity
        modelBuilder.Entity<Role>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<Coupon>().HasKey(c => c.coupon_id);
        // Configure Category entity
        modelBuilder.Entity<Category>()
            .HasKey(c => c.category_name);

        // Configure Supplier entity
        modelBuilder.Entity<Supplier>()
            .HasKey(s => s.supplier_id);

        // Configure Product entity
        modelBuilder.Entity<Product>()
            .HasKey(p => p.product_id);
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.sku)
            .IsUnique();
        modelBuilder.Entity<OrderItem>().ToTable("order_items");
        modelBuilder.Entity<Inventory>().ToTable("inventory");
        // Configure Customer entity
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();

        // Configure Order entity
        modelBuilder.Entity<Order>()
            .HasKey(o => o.order_id);

        // Configure OrderItem entity
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => oi.order_item_id);

        // Configure Payment entity
        modelBuilder.Entity<Payment>()
            .HasKey(p => p.Id);

        // Configure Inventory entity
        modelBuilder.Entity<Inventory>()
            .HasKey(i => i.inventory_id);

        // Configure StockTransaction entity
        modelBuilder.Entity<StockTransaction>()
            .HasKey(st => st.Id);
    }
}
