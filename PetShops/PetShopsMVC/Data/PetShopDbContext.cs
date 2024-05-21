using Microsoft.EntityFrameworkCore;
using PetShopsMVC.DTOs;
using PetShopsMVC.Models;
namespace PetShopsMVC.Data
{
    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Carts> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasKey(o => o.OrderDetailId);

            modelBuilder.Entity<ProductDTO>()
                .Property(p => p.ProductPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Orders>()
            .Property(o => o.OrderTotal)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }

    }
}
