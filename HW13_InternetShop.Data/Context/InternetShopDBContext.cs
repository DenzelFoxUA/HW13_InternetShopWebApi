using HW13_InternetShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using @DataModel = HW13_InternetShop.Data.Models;

namespace HW13_InternetShop.Data
{
    public class InternetShopDBContext : DbContext
    {
        public InternetShopDBContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InternetShopDBContext).Assembly);

        }

    }
}
