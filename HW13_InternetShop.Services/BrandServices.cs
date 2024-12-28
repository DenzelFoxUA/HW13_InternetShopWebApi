using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Repository;

using DataModel = HW13_InternetShop.Data.Models;

namespace HW13_InternetShop.Services
{
    public class BrandServices : IShopServices<Brand>
    {

        private readonly IShopRepository<DataModel.Brand> _brandRep;

        public BrandServices(IShopRepository<DataModel.Brand> brandRep)
        {
            _brandRep = brandRep;
        }

        public Brand Add(Brand obj)
        {
            var brandsNames = _brandRep.GetAll().Select(br => br.Name);

            if (!IsExist(obj.Id) && !brandsNames.Contains(obj.Name))
            {
                _brandRep.Add(new DataModel.Brand()
                {
                    Id = obj.Id,
                    Name = obj.Name
                });

            }
            return obj;
        }

        public bool Delete(int id)
        {
            if(IsExist(id))
            {
                _brandRep.Delete(id);
                return true;
            }
            return false;
        }

        public void DeleteAll()
        {
            _brandRep.DeleteAll();
        }

        public IEnumerable<Brand> GetAll()
        {
            var brandsId = _brandRep.GetAll().Select(br => br.Id);
            var allBrandsQueryModel = new List<Brand>();

            foreach(var id in brandsId)
            {
                allBrandsQueryModel.Add(GetById(id));
            }

            return allBrandsQueryModel;
        }

        public Brand GetById(int id)
        {
            var _brand = _brandRep.GetById(id);

            if (_brand is not null)
                return new Brand() { Id = _brand.Id, Name = _brand.Name };
            else
                return null!;
        }

        public bool IsExist(int id)
        {
            var brandsId = _brandRep.GetAll().Select(br => br.Id);
            return brandsId.Contains(id);
        }

        public Brand Update(int id, Brand updated)
        {
            var brand = _brandRep.GetById(id);
            if (brand is not null)
            {
                brand.Name = updated.Name;
                _brandRep.Update(updated.Id, brand);
            }
            else
            {
                brand = new DataModel.Brand() { Id = id, Name = updated.Name };
                _brandRep.Add(brand);
            }

            return updated;
        }


    }
}
