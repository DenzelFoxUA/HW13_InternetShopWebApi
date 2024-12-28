using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HW13_InternetShop.Data.Models;


namespace InternetShopAspNetCoreMvc.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(cart_item => cart_item.Id);
            builder.Property(cart_item => cart_item.Id).ValueGeneratedOnAdd();
            builder.Property(cart_item => cart_item.Count).IsRequired();

            // Define relationships

            //relations with order
            builder.HasOne(orderItem => orderItem.Order)
                   .WithMany(order => order.OrderItems)
                   .HasForeignKey(orderItem => orderItem.OrderId) // Foreign key to User.Id
                   .OnDelete(DeleteBehavior.NoAction);//deletion of a cart-item doesnt affect user

            //relations with product
            builder.HasOne(item => item.Product)
                   .WithMany(product => product.OrderItems)
                   .HasForeignKey(item => item.ProductId)
                   .OnDelete(DeleteBehavior.NoAction); //deletion of a cart-item doesnt affect product

            
        }
    }
}
