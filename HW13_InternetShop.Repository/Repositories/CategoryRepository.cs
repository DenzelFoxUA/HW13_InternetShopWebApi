using HW13_InternetShop.Data;
using HW13_InternetShop.Data.Models;
using HW13_InternetShop.Repository;
using Microsoft.EntityFrameworkCore;

namespace HW13_InternetShop.Services
{
    public class CategoryRepository : IShopRepository<Category>
    {
        private readonly InternetShopDBContext _dbContext;
        public CategoryRepository(InternetShopDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public Category Add(Category entity)
        {
            _dbContext.Categories.Add(entity);
            _dbContext.SaveChanges();
            //DB_Simulator.Categories.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var cat = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
                //DB_Simulator.Categories.FirstOrDefault(x => x.Id == id);
            if (cat is not null)
            {
                //DB_Simulator.Categories.Remove(cat);
                _dbContext.Categories.Remove(cat);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            //DB_Simulator.Categories.Clear();
            _dbContext.Categories.ExecuteDelete();
            _dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            //return DB_Simulator.Categories;
            return _dbContext.Categories.ToList();
        }

        public Category GetById(int id)
        {
            //return DB_Simulator.Categories.FirstOrDefault(fi => fi.Id == id);
            return _dbContext.Categories.FirstOrDefault(cat => cat.Id == id);
        }

        public Category Update(int id, Category updated)
        {
            var category = GetById(id);
            
            category.Name = updated.Name;

            _dbContext.Categories.Update(updated);
            _dbContext.SaveChanges();
            return updated;
        }

    }
}

