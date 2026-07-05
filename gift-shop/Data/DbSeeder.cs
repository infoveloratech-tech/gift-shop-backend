using gift_shop.Models;

namespace gift_shop.Data;

public class DbSeeder
{
    public static async Task SeedAsync(GiftShopDbContext context)
    {
        // Seed Roles
        if (!context.Roles.Any())
        {
            var roles = new[]
            {
                new Role { Name = "Admin", Description = "Administrator with full access" },
                new Role { Name = "Manager", Description = "Manager with management access" },
                new Role { Name = "Staff", Description = "Staff member with limited access" },
                new Role { Name = "Customer", Description = "Regular customer" }
            };

            context.Roles.AddRange(roles);
        }

        // Seed Users
        if (!context.Users.Any())
        {
            var users = new[]
            {
                new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@giftshop.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Phone = "+1234567890",
                    RoleId = 1,
                    Status = "Active" 
                },
                new User
                {
                    FirstName = "John",
                    LastName = "Manager",
                    Email = "manager@giftshop.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager@123"),
                    Phone = "+1234567891",
                    RoleId = 2,
                    Status = "Active"
                }
            };

            context.Users.AddRange(users);
        }

        // Seed Categories
        if (!context.Categories.Any())
        {
            var categories = new[]
            {
                new Category
                {
                    category_name = "Electronics",
                    description= "Electronic gadgets and devices",
                    image_url = "https://via.placeholder.com/300x300?text=Electronics",
                    status = "active"
                },
                new Category
                {
                    category_name = "Home & Garden",
                    description = "Home decoration and garden items",
                    image_url = "https://via.placeholder.com/300x300?text=Home",
                   status = "active"
                },
                new Category
                {
                    category_name = "Fashion",
                    description = "Clothing and fashion accessories",
                    image_url = "https://via.placeholder.com/300x300?text=Fashion",
                    status = "active"
                },
                new Category
                {
                    category_name = "Books",
                    description = "Books and stationery",
                    image_url = "https://via.placeholder.com/300x300?text=Books",
                    status = "active"
                }
            };

            context.Categories.AddRange(categories);
        }

        // Seed Suppliers
        if (!context.Suppliers.Any())
        {
            var suppliers = new[]
            {
                new Supplier
                {
                    Name = "TechSupply Co.",
                    ContactEmail = "contact@techsupply.com",
                    ContactPhone = "+1-800-123-4567",
                    Address = "123 Tech Street",
                    City = "San Francisco",
                    State = "CA",
                    PostalCode = "94102",
                    Country = "USA",
                    IsActive = true
                },
                new Supplier
                {
                    Name = "HomeGoods International",
                    ContactEmail = "info@homegoods.com",
                    ContactPhone = "+1-800-987-6543",
                    Address = "456 Home Ave",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10001",
                    Country = "USA",
                    IsActive = true
                }
            };

            context.Suppliers.AddRange(suppliers);
        }

        await context.SaveChangesAsync();
    }
}
