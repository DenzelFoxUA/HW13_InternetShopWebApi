using HW13_InternetShop.Data;
using HW13_InternetShop.Data.Models;
using HW13_InternetShop.Repository;
using Microsoft.EntityFrameworkCore;

namespace HW13_InternetShop.Services
{
    public class OrderItemRepository : IShopRepository<OrderItem>
    {
        private readonly InternetShopDBContext _dBContext;
        public OrderItemRepository(InternetShopDBContext dBContext)
        {
            _dBContext = dBContext;

        }

        public Data.Models.OrderItem Add(Data.Models.OrderItem entity)
        {

            _dBContext.OrderItems.Add(entity);
            _dBContext.SaveChanges();

            return entity;
        }

        

        public void Delete(int id)
        {

            var item = GetById(id);
            if(item != null)
            {
                _dBContext.OrderItems.Remove(item);
            }

            _dBContext.SaveChanges();
            //HW13_InternetShop.DB_Simulator.OrderItems.Remove(found_item);
        }

        public void DeleteAll()
        {
            //HW13_InternetShop.DB_Simulator.OrderItems.Clear();
            _dBContext.OrderItems.ExecuteDelete();
            _dBContext.SaveChanges();
        }

        public IEnumerable<OrderItem> GetAll()
        {
            //return HW13_InternetShop.DB_Simulator.OrderItems.ToList();
           return _dBContext.OrderItems.ToList();
        }

        public OrderItem GetById(int id)
        {
            //return HW13_InternetShop.DB_Simulator.OrderItems.FirstOrDefault(orItem => orItem.Id == id);
            return _dBContext.OrderItems.FirstOrDefault(x => x.Id == id);
        }

        public OrderItem Update(int id, OrderItem updated)
        {
            updated.Id = id;
            _dBContext.OrderItems.Update(updated);
            _dBContext.SaveChanges();
            return updated;
        }


    }
}
