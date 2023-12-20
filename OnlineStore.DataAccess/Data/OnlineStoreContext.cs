using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Data
{
    public class OnlineStoreContext : DbContext
    {

        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
                .HasOne(e => e.ClerkUsers)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Orders>()
                .HasOne(e => e.CustomerUsers)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Stock)
                .WithOne(s => s.ProductsStocks)
                .HasForeignKey<Stocks>(s => s.Id)
                .IsRequired(false);
        }
        public DbSet<Products> Products { get; set; } = default!;
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
    }
}
