using HW13_InternetShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InternetShopAspNetCoreMvc.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(user => user.Id); // key
            builder.Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Property(user => user.Email).HasColumnType("VARCHAR").IsRequired().HasMaxLength(100);
            builder.Property(user => user.Phone).HasColumnName("Phone").HasColumnType("CHAR").IsRequired().HasMaxLength(14);

            //User - cartItems relationship

            //user-orders
            builder.HasMany(user => user.Orders) //user have many orders
                   .WithOne(orders => orders.Customer) // Orders have one user
                   .HasForeignKey(order => order.CustomerId) // FK to User by its Id
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
