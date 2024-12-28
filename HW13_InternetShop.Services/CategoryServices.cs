using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Repository;

using DataModel = HW13_InternetShop.Data.Models;

namespace HW13_InternetShop.Services
{
    public class CategoryServices : IShopServices<Category>
    {
        private readonly IShopRepository<DataModel.Category> _categoryRep;

        public CategoryServices(IShopRepository<DataModel.Category> categoryRep)
        {
            _categoryRep = categoryRep;
        }

        public Category Add(Category obj)
        {
            var categNames = _categoryRep.GetAll().Select(cat => cat.Name);

            if (!IsExist(obj.Id) && !categNames.Contains(obj.Name))
            {
                _categoryRep.Add(new DataModel.Category()
                {
                    Id = obj.Id,
                    Name = obj.Name
                });
            }
            return obj;
        }

        public bool Delete(int id)
        {
            if (IsExist(id))
            {
                _categoryRep.Delete(id);
                return true;
            }
            else return false;
        }

        public void DeleteAll()
        {
            _categoryRep.DeleteAll();
        }

        public IEnumerable<Category> GetAll()
        {
            var categoriesId = _categoryRep.GetAll().Select(cat => cat.Id);
            var allCategoriesQueryModel = new List<Category>();

            foreach (var id in categoriesId)
            {
                allCategoriesQueryModel.Add(GetById(id));
            }

            return allCategoriesQueryModel;
        }

        public Category GetById(int id)
        {
            var _cat = _categoryRep.GetById(id);

            if (_cat is not null)
                return new Category() { Id = _cat.Id, Name = _cat.Name }; //returns QueryModel
            else
                return null!;
        }

        public bool IsExist(int id)
        {
            var categoriesId = _categoryRep.GetAll().Select(cat => cat.Id);
            return categoriesId.Contains(id);
        }

        public Category Update(int id, Category updated)
        {
            var cat = _categoryRep.GetById(id);

            if (cat is not null)
            {
                cat.Name = updated.Name;
                _categoryRep.Update(id, cat);
            }
            else
            {
                cat = new DataModel.Category() { Id = id, Name = updated.Name };
                _categoryRep.Add(cat);
            }
            return updated;
        }
    }
}
