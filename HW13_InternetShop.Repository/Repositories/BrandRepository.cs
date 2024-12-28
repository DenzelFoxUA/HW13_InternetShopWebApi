
using HW13_InternetShop.Data;
using HW13_InternetShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HW13_InternetShop.Repository
{
    public class BrandRepository : IShopRepository<Brand>
    {
        private readonly InternetShopDBContext _dbContext;
        public BrandRepository(InternetShopDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public Brand Add(Brand entity)
        {
            _dbContext.Brands.Add(entity);
            _dbContext.SaveChanges();
            //DB_Simulator.Brands.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var br = _dbContext.Brands.FirstOrDefault(x => x.Id == id); //DB_Simulator.Brands.FirstOrDefault(x => x.Id == id);
            if (br is not null)
            {
                _dbContext.Brands.Remove(br);
                //DB_Simulator.Brands.Remove(br);
            }

            _dbContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dbContext.Brands.ExecuteDelete();
            _dbContext.SaveChanges();
            //DB_Simulator.Brands.Clear();
        }

        public IEnumerable<Brand> GetAll()
        {
            return _dbContext.Brands.ToList();
            //return DB_Simulator.Brands.ToList();
        }

        public Brand GetById(int id)
        {
            //return DB_Simulator.Brands.FirstOrDefault(fi => fi.Id == id);
            return _dbContext.Brands.FirstOrDefault(br => br.Id == id);
        }

        public Brand Update(int id, Brand updated)
        {
            _dbContext.Brands.Update(updated);
            _dbContext.SaveChanges();
            return updated;
                
        }
    }
}
