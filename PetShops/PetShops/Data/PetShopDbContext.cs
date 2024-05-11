﻿using Microsoft.EntityFrameworkCore;
using PetShops.Models;
namespace PetShops.Data
{
    public class PetShopDbContext: DbContext
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<EmailSubscribe> EmailSubscribe { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
              .HasOne(p => p.Category)
              .WithMany(c => c.Products)
              .HasForeignKey(p => p.CategoryId)
              .IsRequired(false); 

            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.PetType)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.PetTypeId)
                .IsRequired(false);

            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}