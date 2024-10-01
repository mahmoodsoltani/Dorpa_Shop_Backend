using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        private void SetDefaultValueForDate<T>(ModelBuilder modelBuilder)
            where T : class
        {
            Type type = typeof(T);
            modelBuilder
                .Entity<T>()
                .Property(type.GetProperty("Create_Date").Name)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder
                .Entity<T>()
                .Property(type.GetProperty("Update_Date").Name)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }

        private void AddFakeData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(SeedingData.GetUsers());
            modelBuilder.Entity<Color>().HasData(SeedingData.GetColors());
            modelBuilder.Entity<Size>().HasData(SeedingData.GetSizes());
            modelBuilder.Entity<Category>().HasData(SeedingData.GetCategories());
            modelBuilder.Entity<Brand>().HasData(SeedingData.GetBrands());
            modelBuilder.Entity<Product>().HasData(SeedingData.GetProducts());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Order>()
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            SetDefaultValueForDate<Order>(modelBuilder);
            SetDefaultValueForDate<OrderDetail>(modelBuilder);
            SetDefaultValueForDate<CartDetail>(modelBuilder);
            SetDefaultValueForDate<User>(modelBuilder);
            SetDefaultValueForDate<Address>(modelBuilder);
            SetDefaultValueForDate<Favourite>(modelBuilder);
            SetDefaultValueForDate<Review>(modelBuilder);
            SetDefaultValueForDate<Brand>(modelBuilder);
            SetDefaultValueForDate<Category>(modelBuilder);
            SetDefaultValueForDate<Discount>(modelBuilder);
            SetDefaultValueForDate<Product>(modelBuilder);
            SetDefaultValueForDate<ProductImage>(modelBuilder);
            SetDefaultValueForDate<Color>(modelBuilder);
            SetDefaultValueForDate<Size>(modelBuilder);

            modelBuilder
                .Entity<Product>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Product_Price_AboveZero", "price> 0");
                });

            modelBuilder.Entity<Product>().Property(o => o.Stock).HasDefaultValueSql("0");
            modelBuilder.Entity<User>().Property(o => o.IsAdmin).HasDefaultValueSql("FALSE");
            modelBuilder.Entity<User>().Property(o => o.IsDeleted).HasDefaultValueSql("FALSE");
            modelBuilder
                .Entity<OrderDetail>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_OrderDetail_Quantity_Positive", "quantity > 0");
                    t.HasCheckConstraint("CK_OrderDetail_Price_AboveZero", "price > 0");
                });

            modelBuilder
                .Entity<Discount>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Discount_Date", "start_Date < end_Date");
                });
            // AddFakeData(modelBuilder);
        }
    }
}
