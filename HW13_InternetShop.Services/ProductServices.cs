using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Repository;

using DataModel = HW13_InternetShop.Data.Models;
using QueryModel = HW13_InternetShop._Contract.QueryModels;

namespace HW13_InternetShop.Services
{
    public class ProductServices: IShopServices<QueryModel.Product>
    {
        private readonly IShopRepository<DataModel.Product> _productRep;

        private readonly IShopRepository<DataModel.Category> _categoryRep;

        private readonly IShopRepository<DataModel.Brand> _brandRep;


        public ProductServices(IShopRepository<DataModel.Product> productRep,
            IShopRepository<DataModel.Category> categoryRep,
            IShopRepository<DataModel.Brand> brandRep) 
        { 
            _brandRep = brandRep;
            _productRep = productRep;
            _categoryRep = categoryRep;
        }

        public Product Add(QueryModel.Product product)
        {
            if(product is not null && !IsExist(product.Id))
            {
                _productRep.Add(new DataModel.Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    BrandId = product.BrandId,
                    Price = product.Price

                });
            }
            return product!;
        }

        
        public bool Delete(int id)
        {

            if (IsExist(id))
            {
                _productRep.Delete(id);
                return true;
            }
            else return false;
                
        }

        public void DeleteAll()
        {
            _productRep.DeleteAll();
        }

        public IEnumerable<Product> GetAll()
        {
            var allProductsQueryModel = new List<QueryModel.Product>();

            var allProductId_s = _productRep.GetAll().Select(prod => prod.Id); 
            if(allProductId_s is not null)
            {
                foreach (var id in allProductId_s)
                {
                    var prod = GetById(id);
                    allProductsQueryModel.Add(prod);
                }
                return allProductsQueryModel;
            }
            return null!;
        }

        public Product GetById(int id)
        {
            var prodFromDB = _productRep.GetById(id);
            if(prodFromDB is not null)
            {
                var category = _categoryRep.GetById(prodFromDB.CategoryId);
                var brand = _brandRep.GetById(prodFromDB.BrandId);

                if(category is not null && brand is not null)
                {
                    var productQModel =
                    new QueryModel.Product()
                    {
                        Id = prodFromDB.Id,
                        Name = prodFromDB.Name,
                        Description = prodFromDB.Description,
                        CategoryId = prodFromDB.CategoryId,
                        BrandId = prodFromDB.BrandId,
                        Price = prodFromDB.Price,
                        BrandName = brand.Name,
                        CategoryName = category.Name

                    };
                    return productQModel;
                }
                else return null!;
                
            }
                
            else { return null!; }
        }

        public bool IsExist(int id)
        {
            var prod = _productRep.GetById(id);
            return prod is not null;
        }

        public Product Update(int id, Product updated)
        {

            if (IsExist(id) && updated is not null)
            {
                _productRep.Update(id, new DataModel.Product()
                {
                    BrandId = updated.BrandId,
                    CategoryId = updated.CategoryId,
                    Name = updated.Name,
                    Description = updated.Description,
                    Price = updated.Price
                });

            }
            else
            {
                var prod = new DataModel.Product();
                prod.Name = updated.Name;
                prod.Description = updated.Description;
                prod.Price = updated.Price;
                prod.BrandId = updated.BrandId;
                prod.CategoryId = updated.CategoryId;
                
                _productRep.Add(prod);
            }

            return updated!;
        }
    }
}
