using HW13_InternetShop.Data;
using HW13_InternetShop.Data.Models;
using HW13_InternetShop.Repository;
using Microsoft.EntityFrameworkCore;

namespace HW13_InternetShop.Services
{
    public class ProductRepository : IShopRepository<Product>
    {

        private readonly InternetShopDBContext _dBContext;
        public ProductRepository(InternetShopDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Product Add(Product entity)
        {
            //DB_Simulator.Products.Add(entity);
            _dBContext.Products.Add(entity);
            _dBContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var prod = _dBContext.Products.FirstOrDefault(p => p.Id == id);
                //DB_Simulator.Products.FirstOrDefault(x => x.Id == id);
            if(prod is not null)
                _dBContext.Products.Remove(prod);

            _dBContext.SaveChanges();
                //DB_Simulator.Products.Remove(prod);
            
        }

        public void DeleteAll()
        {
            //DB_Simulator.Products.Clear();

            _dBContext.Products.ExecuteDelete();
            _dBContext.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
           //return DB_Simulator.Products.ToList();
           return _dBContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            //return DB_Simulator.Products.FirstOrDefault(prod => prod.Id == id);
            return _dBContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product Update(int id, Product updated)
        {
            var itemToUpdate = _dBContext.Products.FirstOrDefault(item => item.Id == id);
            if(itemToUpdate is not null)
            {
                itemToUpdate.Name = updated.Name;
                itemToUpdate.Description = updated.Description;
                itemToUpdate.Price = updated.Price;
                itemToUpdate.BrandId = updated.BrandId;
                itemToUpdate.CategoryId = updated.CategoryId;

                _dBContext.Products.Update(itemToUpdate);
                _dBContext.SaveChanges();
            }

            return itemToUpdate!;
        }
    }
}
