using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) 
        {
        
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);

            // Seed initial data
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 2000, Stock = 10 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest smartphone", Price = 1200, Stock = 15 },
                new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling headphones", Price = 500.50, Stock = 20 }
            );
        }
    }
}
