using Microsoft.EntityFrameworkCore;
using PetShopsMVC.DTOs;
using PetShopsMVC.Models;
namespace PetShopsMVC.Data
{
    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<OrderDetailDTO> OrderDetails { get; set; }
        public DbSet<Carts> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetailDTO>().HasKey(o => o.OrderDetailId);

            modelBuilder.Entity<ProductDTO>()
                .Property(p => p.ProductPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDTO>()
            .Property(o => o.OrderTotal)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetailDTO>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }

    }
}
