using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Data.Models;

using QueryModel = HW13_InternetShop._Contract.QueryModels;
using DataModel = HW13_InternetShop.Data.Models;

using HW13_InternetShop.Repository;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;


namespace HW13_InternetShop.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IShopRepository<DataModel.Customer> _customersRep;
        private readonly IShopRepository<DataModel.Order> _ordersRep;
        private readonly IShopRepository<DataModel.Brand> _brandRep;
        private readonly IShopRepository<DataModel.Category> _categoryRep;
        private readonly IShopRepository<DataModel.Product> _productRep;
        private readonly IShopRepository<DataModel.OrderItem> _orderItemsRep;

        private readonly IOrderItemServices _orderItemsService;

        public OrderServices(IOrderItemServices orderItemsService,
        IShopRepository<DataModel.Customer> customerRep,
            IShopRepository<DataModel.Brand> brandRep,
            IShopRepository<DataModel.Category> categoryRep,
            IShopRepository<DataModel.Product> productRep,
            IShopRepository<DataModel.Order> orderRep,
            IShopRepository<DataModel.OrderItem> itemsRep)
        {
            _categoryRep = categoryRep;
            _productRep = productRep;
            _brandRep = brandRep;
            _ordersRep = orderRep;
            _orderItemsRep = itemsRep;
            _customersRep = customerRep;

            _orderItemsService = orderItemsService;

        }

        public IEnumerable<QueryModel.Order> GetAll()
        {
            var orders = new List<QueryModel.Order>();
            IEnumerable<int> ordersIdsFromDB = _ordersRep.GetAll().Select(x => x.Id);

            foreach (var id in ordersIdsFromDB)
            {
                var order = GetById(id);
                orders.Add(order);
            }

            return orders;

        }

        public QueryModel.Order GetById(int orderId)
        {

            var order = _ordersRep.GetById(orderId);

            var customer = _customersRep.GetById(order.CustomerId);

            var listOfItems = GetOrderItemsByOrderId(orderId);

            var orderFullView = new QueryModel.Order();

            if (order is not null &&
               listOfItems is not null &&
               customer is not null)
            {
                orderFullView.Id = orderId;
                orderFullView.CustomerId = customer.Id;
                orderFullView.CustomerEmail = customer.Email;
                orderFullView.CustomerPhone = customer.Phone;
                orderFullView.OrderItems = listOfItems;
                orderFullView.Total = CalculateTotalSum(orderId);
                //CalculateTotalSum(_orderItemsRep.GetAll().Where(order => order.OrderId == orderId));
            }

            return orderFullView;
        }

        public decimal CalculateTotalSum(int orderId)
        {
            var items = GetAllOrderItemsFromDB(orderId);
            decimal total = 0;
            foreach (var item in items)
            {
                var product = _productRep.GetById(item.ProductId);
                total += product.Price * item.Count;
            }

            return total;
        }

        public IEnumerable<DataModel.OrderItem> GetAllOrderItemsFromDB(int orderId)
        {
            return _orderItemsRep.GetAll().Where(item => item.OrderId == orderId);
        }

        public IEnumerable<QueryModel.OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            var itemsFromDB = GetAllOrderItemsFromDB(orderId);

            var contractModelItems = new List<QueryModel.OrderItem>();

            foreach (var item in itemsFromDB)
            {
                var orderItem = new QueryModel.OrderItem();

                var product = _productRep.GetById(item.ProductId);
                var brand = _brandRep.GetById(product.BrandId);
                var category = _categoryRep.GetById(product.CategoryId);

                orderItem.Id = item.Id;
                orderItem.BrandName = brand.Name;
                orderItem.Price = product.Price;
                orderItem.ProductId = item.ProductId;
                orderItem.Count = item.Count;
                orderItem.CategoryName = category.Name;
                orderItem.ProductName = product.Name;

                contractModelItems.Add(orderItem);
            }

            return _orderItemsService.GetAll().Where(item => item.OrderId == orderId).ToList();
        }

        public QueryModel.Order Add(QueryModel.Order order)
        {

            DataModel.Order _orderDataForDB = new DataModel.Order()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Total = 0
            };

            _ordersRep.Add(_orderDataForDB);

            //foreach (var item in order.OrderItems)
            //{
            //    item.Id = _orderDataForDB.Id;
            //    _orderItemsService.Add(item);
            //}

            //_orderDataForDB.Total = _orderItemsService.GetTotalByOrderId(_orderDataForDB.Id);
            //    //CalculateTotalSum(_orderItemsService.GetAll().Where(item => item.OrderId == order.Id));

            //_ordersRep.Update(_orderDataForDB.Id, _orderDataForDB);

            return order;
        }

        public QueryModel.Order Create(QueryModel.Order order)
        {

            //1. Add body of order to DB
            Add(order);


            //2. Add items and bind them to order Id
            if (order.OrderItems is not null && order.OrderItems.Count() > 0)
            {
                foreach (var item in order.OrderItems)
                {
                    item.OrderId = _ordersRep.GetAll().Max(order => order.Id);
                    _orderItemsService.Add(item);
                }

            }

            //3. Commit and save objects


            return order;
        }

        public void DeleteAll()
        {
            _orderItemsRep.DeleteAll();
            _ordersRep.DeleteAll();
        }

        public bool Delete(int orderId)
        {
            var order = GetById(orderId);

            if (order is not null)
            {
                foreach (var item in order.OrderItems)
                {
                    _orderItemsRep.Delete(item.Id);
                }
                _ordersRep.Delete(orderId);

                return true;
            }
            return false;

        }

        public QueryModel.Order Update(int id, QueryModel.Order updated)
        {
            if (IsExist(id))
            {
                var oldOrderItems = _orderItemsRep.GetAll().Where(x => x.Id == id);

                foreach (var item in oldOrderItems)
                {
                    _orderItemsRep.Delete(item.Id);
                }

                foreach (var item in updated.OrderItems)
                {
                    _orderItemsRep.Add(new DataModel.OrderItem()
                    {
                        Id = updated.Id,
                        Count = item.Count,
                        OrderId = item.OrderId,
                        ProductId = item.ProductId
                    });
                }

                _ordersRep.Update(id, new DataModel.Order()
                {
                    Id = updated.Id,
                    CustomerId = updated.CustomerId,
                    Total = updated.Total

                });

            }
            return updated;
        }

        public bool IsExist(int orderId)
        {
            var exist = _ordersRep.GetById(orderId);

            return exist is not null;
        }
    }
}
