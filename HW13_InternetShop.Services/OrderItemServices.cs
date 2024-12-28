using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataModel = HW13_InternetShop.Data.Models;

namespace HW13_InternetShop.Services
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IShopRepository<DataModel.OrderItem> _itemsRep;
        private readonly IShopRepository<DataModel.Product> _prodRep;
        private readonly IShopRepository<DataModel.Brand> _brandRep;
        private readonly IShopRepository<DataModel.Category> _catRep;
        public OrderItemServices(IShopRepository<DataModel.OrderItem> itemsRep,
            IShopRepository<DataModel.Product> prodRep,
            IShopRepository<DataModel.Brand> brandRep,
            IShopRepository<DataModel.Category> catRep)
        {
            _itemsRep = itemsRep;
            _prodRep = prodRep;
            _brandRep = brandRep;
            _catRep = catRep;
        }

        public OrderItem Add(OrderItem obj)
        {
            if (obj is not null)
            {

                _itemsRep.Add(new DataModel.OrderItem()
                {
                    Id = obj.Id,
                    OrderId = obj.OrderId,
                    ProductId = obj.ProductId,
                    Count = obj.Count
                });
            }
            return obj!;
        }

        public bool Delete(int id)
        {
            if (IsExist(id))
            {
                _itemsRep.Delete(id);
                return true;
            }
            else
                return false;
        }

        public void DeleteAll()
        {
            _itemsRep.DeleteAll();
        }

        public IEnumerable<OrderItem> GetAll()
        {
            var items = new List<OrderItem>();
            var itemsFromDb = _itemsRep.GetAll();

            if (itemsFromDb is not null)
            {
                foreach (var item in itemsFromDb)
                {
                    items.Add(GetById(item.Id));
                }
                return items;
            }
            else return null!;

        }

        public OrderItem GetById(int id)
        {
            var itemFromDb = _itemsRep.GetById(id);

            if (itemFromDb is not null)
            {
                var product = _prodRep.GetById(itemFromDb.ProductId);

                if (product != null)
                {
                    var brand = _brandRep.GetById(product.BrandId);
                    var category = _catRep.GetById(product.CategoryId);

                    return new OrderItem()
                    {
                        Id = itemFromDb.Id,
                        OrderId = itemFromDb.OrderId,
                        ProductId = itemFromDb.ProductId,
                        Count = itemFromDb.Count,
                        ProductName = product.Name,
                        CategoryName = category.Name,
                        BrandName = brand.Name,
                        Price = product.Price
                    };
                }
            }

            return null!;
        }

        public bool IsExist(int id)
        {
            var item = _itemsRep.GetById(id);
            if (item is not null)
                return true;
            return false;
        }

        public OrderItem Update(int id, OrderItem updated)
        {
            if (IsExist(id))
            {
                var item = new DataModel.OrderItem();

                item.Id = id;
                item.OrderId = updated.Id;
                item.ProductId = updated.ProductId;
                item.Count = updated.Count;
                _itemsRep.Update(id, item);
            }
            else
            {
                var item = new DataModel.OrderItem()
                {
                    Id = id,
                    OrderId = updated.Id,
                    ProductId = updated.ProductId,
                    Count = updated.Count
                };

                _itemsRep.Add(item);
            }

            return updated;
        }


        public decimal GetTotalByOrderId(int orderId)
        {
            var items = _itemsRep.GetAll().Where(order => order.Id == orderId);
            //_dBContext.OrderItems.Where(order => order.Id == orderId);

            decimal total = 0;

            foreach (var item in items)
            {
                var prod = _prodRep.GetById(item.ProductId);
                total += prod.Price * item.Count;
            }

            return total;
        }
    }
}
