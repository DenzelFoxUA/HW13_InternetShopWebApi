using HW13_InternetShop.Data;
using HW13_InternetShop.Data.Models;
using HW13_InternetShop.Repository;
using Microsoft.EntityFrameworkCore;


namespace HW13_InternetShop.Services
{

    public class CustomerRepository : IShopRepository<Customer>
    {
        private readonly InternetShopDBContext _dBContext;
        public CustomerRepository(InternetShopDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Customer Add(Customer entity)
        {
            //DB_Simulator.Customers.Add(entity);
            _dBContext.Customers.Add(entity);
            _dBContext.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var customer = _dBContext.Customers.FirstOrDefault(customer => customer.Id == id);
                //DB_Simulator.Customers.FirstOrDefault(x => x.Id == id);
            if (customer is not null)
            {
                _dBContext.Customers.Remove(customer);
                _dBContext.SaveChanges();
                //DB_Simulator.Customers.Remove(customer);
            }
        }

        public void DeleteAll()
        {
            _dBContext.Customers.ExecuteDelete();
            _dBContext.SaveChanges();
            //DB_Simulator.Customers.Clear();
        }

        public IEnumerable<Customer> GetAll()
        {
            //return DB_Simulator.Customers.ToList();
            return _dBContext.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            var found_item = _dBContext.Customers.FirstOrDefault(x=>x.Id == id);
                //DB_Simulator.Customers.FirstOrDefault(fi => fi.Id == id);
            if (found_item is not null)
                return found_item;
            else
                return null!;
        }

        public Customer Update(int id, Customer updated)
        {
            
            //DB_Simulator.Customers.FirstOrDefault(fi => fi.Id == id);

            _dBContext.Customers.Update(updated);

            _dBContext.SaveChanges();
            return updated;
        }



    }

}

