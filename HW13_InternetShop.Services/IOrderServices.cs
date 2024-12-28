using HW13_InternetShop._Contract.QueryModels;

namespace HW13_InternetShop.Services
{
    public interface IOrderServices : IShopServices<Order>
    {
        Order Create(Order order);
    }
}