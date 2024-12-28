using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Repository;


using DataModel = HW13_InternetShop.Data.Models;

namespace HW13_InternetShop.Services
{
    public class CustomerService : IShopServices<Customer>
    {
        private readonly IShopRepository<DataModel.Customer> _customerRep;
        private readonly IShopRepository<DataModel.Order> _orderRep;
        private readonly IShopRepository<DataModel.OrderItem> _orderItemsRep;

        public CustomerService(IShopRepository<DataModel.Customer> customerRep,
            IShopRepository<DataModel.Order> orderRep,
            IShopRepository<DataModel.OrderItem> orderItemsRep)
        {
            _orderItemsRep = orderItemsRep;
            _customerRep = customerRep;
            _orderRep = orderRep;
        }

        public Customer Add(Customer customer)
        {
            if (customer is not null && !IsExist(customer.Id))
            {
                _customerRep.Add(new DataModel.Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone
                });
            }

            return customer!;
        }

        public bool Delete(int id)
        {
            if (IsExist(id))
            {
                var customer = _customerRep.GetById(id);
                var orders = _orderRep.GetAll().Where(x => x.CustomerId == id);

                foreach (var order in orders)
                {
                    var orderItems = _orderItemsRep.GetAll().Where(item => item.OrderId == order.Id);
                    foreach (var item in orderItems)
                    {
                        _orderItemsRep.Delete(item.Id);
                    }
                    _orderRep.Delete(order.Id);
                }

                _customerRep.Delete(id);

                return true;
            }

            return false;
        }


        public void DeleteAll()
        {
            _orderItemsRep.DeleteAll();
            _orderRep.DeleteAll();
            _customerRep.DeleteAll();
        }

        public IEnumerable<Customer> GetAll()
        {
            //get all customers Id's
            var customersIdList = _customerRep.GetAll().Select(customer => customer.Id);

            if (customersIdList is not null)
            {
                var _QModels = new List<Customer>();

                foreach (var id in customersIdList)
                {
                    var customer = GetById(id);
                    _QModels.Add(customer);
                }

                return _QModels;
            }
            else return null!;
        }

        public Customer GetById(int id)
        {
            var _QModel = new Customer();

            if (IsExist(id))
            {
                DataModel.Customer _DBModel = _customerRep.GetById(id);
                _QModel.Id = id;
                _QModel.Name = _DBModel.Name;
                _QModel.LastName = _DBModel.LastName;
                _QModel.Phone = _DBModel.Phone;
                _QModel.Email = _DBModel.Email;
                _QModel.OrderIndexes = _orderRep.GetAll().Where(order => order.CustomerId == id).Select(order => order.Id);
                return _QModel;
            }
            else return null!;
        }

        public bool IsExist(int id)
        {
            var customer = _customerRep.GetById(id);
            return customer is not null;
        }

        public Customer Update(int id, Customer updated)
        {
            if (IsExist(id))
            {
                var customer = _customerRep.GetById(id);

                customer.Name = updated.Name;
                customer.LastName = updated.LastName;
                customer.Phone = updated.Phone;
                customer.Email = updated.Email;

                _customerRep.Update(updated.Id, customer);
            }
            else
            {
                var customer = new DataModel.Customer();
                customer.Name = updated.Name;
                customer.LastName = updated.LastName;
                customer.Phone = updated.Phone;
                customer.Email = updated.Email;
                _customerRep.Add(customer);
            }

            return updated;
        }
    }
}
