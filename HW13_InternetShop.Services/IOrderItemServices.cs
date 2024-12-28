using HW13_InternetShop._Contract.QueryModels;

namespace HW13_InternetShop.Services
{
    public interface IOrderItemServices : IShopServices<OrderItem>
    {
        decimal GetTotalByOrderId(int orderId);
        bool IsExist(int id);
        OrderItem Update(int id, OrderItem updated);
    }
}