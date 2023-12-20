using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new OnlineStoreContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<OnlineStoreContext>>()))
        {
            // Look for any movies.
            if (context.Products.Any() && context.Categories.Any())
            {
                return;   // DB has been seeded
            }
            context.Categories.AddRange(
                new Categories
                {
                    Name = "Toys",
                    Description = "Kid Like it",
                    Image = "download.jpg",
                    IsDelete = false
                },
                new Categories
                {
                    Name = "cosmetics",
                    Description = "Women",
                    Image = "download(2).jpg",
                    IsDelete = false
                }
            );
            context.SaveChanges();
            context.Products.AddRange(
                new Products
                {
                    Name = "Ball",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100 - 000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Xe Dieu Khien",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Xe Dieu Khien",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Xe Dieu Khien",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Xe Dieu Khien",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Xe Dieu Khien",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Xe Dieu Khien",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Xe Dieu Khien",
                    Description = "Good Quality",
                    Thumbnail = "download.jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 1,
                    IsDelete = false
                },
                new Products
                {
                    Name = "Son",
                    Description = "Good Quality",
                    Thumbnail = "download(2).jpg",
                    UnitPrice = 100000,
                    CreatedBy = null,
                    CreateAt = DateTime.Now,
                    CategoryId = 2,
                    IsDelete = false
                }
            );
            context.SaveChanges();
        }
    }
}