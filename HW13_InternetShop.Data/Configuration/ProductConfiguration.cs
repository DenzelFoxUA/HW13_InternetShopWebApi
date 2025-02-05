﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HW13_InternetShop.Data.Models;

namespace InternetShopAspNetCoreMvc.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnType("VARCHAR").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasColumnType("VARCHAR").HasMaxLength(200);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

            // Define relationships

            builder.HasMany(p => p.OrderItems)
                   .WithOne(o_item => o_item.Product)
                   .HasForeignKey(od => od.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Category)
                   .WithMany(categ => categ.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
