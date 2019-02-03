using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .ToTable("Product")
                .Property(p => p.CategoryId)
                .HasDefaultValue(0);
            builder.Entity<Category>()
                .ToTable("Category");
            builder.Entity<Order>()
                .ToTable("Order");
            builder.Entity<OrderLine>()
                .ToTable("OrderLine");
        }
    }
}
