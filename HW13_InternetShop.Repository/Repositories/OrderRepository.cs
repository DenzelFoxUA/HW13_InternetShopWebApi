using HW13_InternetShop.Data;
using HW13_InternetShop.Data.Models;
using HW13_InternetShop.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace HW13_InternetShop.Services
{
    public class OrderRepository : IShopRepository<Order>
    {
        private readonly InternetShopDBContext _dBContext;

        public OrderRepository(InternetShopDBContext dBContext)
        {

            _dBContext = dBContext;
        }

        public Data.Models.Order Add(Data.Models.Order entity)
        {
            var transact = _dBContext.Database.BeginTransaction();

            var customer = _dBContext.Customers.FirstOrDefault(x => x.Id == entity.CustomerId);

            if (customer is not null)
            {
                _dBContext.Orders.Add(entity);
                transact.Commit();
            }
            else
            {
                transact.Rollback();
            }

            _dBContext.SaveChanges();
            //HW13_InternetShop.DB_Simulator.Orders.Add(order);
            return entity;
        }

        public void Delete(int id)
        {

            var found_item = GetById(id);

            var allItemsInOrder = _dBContext.OrderItems.Where(order => order.Id == id);

            foreach (var item in allItemsInOrder)
            {
                _dBContext.Remove(item);
            }

            _dBContext.Orders.Remove(found_item);
            _dBContext.SaveChanges();
        }

        public void DeleteAll()
        {
            //HW13_InternetShop.DB_Simulator.Orders.Clear();
            
            _dBContext.OrderItems.ExecuteDelete();
            _dBContext.Orders.ExecuteDelete();
            _dBContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return _dBContext.Orders.ToList();
            //return HW13_InternetShop.DB_Simulator.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _dBContext.Orders.Find(id);
            //return HW13_InternetShop.DB_Simulator.Orders.FirstOrDefault(order => order.Id == id);
        }

        public Order Update(int id, Order updated)
        {
            var found_item = GetById(id);
                //HW13_InternetShop.DB_Simulator.Orders.FirstOrDefault(fi => fi.Id == id);

            if (found_item is not null)
            {

                //update order object
                _dBContext.Orders.Update(found_item);

                
            }
            _dBContext.SaveChanges();
            return updated;
        }

    }
}
