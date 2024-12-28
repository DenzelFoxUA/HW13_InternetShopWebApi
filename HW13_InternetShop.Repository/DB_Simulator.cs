

namespace HW13_InternetShop
{
    public static class DB_Simulator
    {
        public static List<Data.Models.Customer> Customers { get; set; } = new List<Data.Models.Customer>();
        public static List<Data.Models.Category> Categories { get; set; } = new List<Data.Models.Category>();
        public static List<Data.Models.Brand> Brands { get; set; } = new List<Data.Models.Brand>();

        public static List<Data.Models.Product> Products { get; set; } = new List<Data.Models.Product>();
        public static List<Data.Models.Order> Orders { get; set; } = new List<Data.Models.Order>();
        public static List<Data.Models.OrderItem> OrderItems { get; set; } = new List<Data.Models.OrderItem>();

        public static void GenerateCustomers()
        {

            var customers = new List<Data.Models.Customer>()
            {
                new Data.Models.Customer()
                {
                    Id = 1,
                    Name = "First",
                    LastName = "Test",
                    Phone = "test",
                    Email = "test"
                },

                new Data.Models.Customer()
                {
                    Id = 2,
                    Name = "Second",
                    LastName = "Test",
                    Phone = "test",
                    Email = "test"
                }
            };

            Customers = customers.ToList();
            //Customers = customers.Select(customer => new _Contract.Customer
            //{
            //    Id = customer.Id,
            //    Name = customer.Name,
            //    LastName = customer.LastName,
            //    Phone = customer.Phone,
            //    Email = customer.Email,
                

            //}).ToList();

        }

        public static void GenerateCategories()
        {
            var categories = new List<Data.Models.Category>
             {
                 new Data.Models.Category()
                 {
                     Id = 1,
                     Name = "Electronics"
                 },

                 new Data.Models.Category()
                 {
                     Id = 2,
                     Name = "Photo/Video"
                 }
             };

            Categories = categories;

            //Categories = categories.Select(c => new _Contract.Category
            //{
            //    Id = c.Id,
            //    Name = c.Name
            //}).ToList();
        }

        public static void GenerateBrands()
        {
            var brands = new List<Data.Models.Brand>
            {
                new Data.Models.Brand()
                {
                    Id = 1,
                    Name = "Sony"
                },
                new Data.Models.Brand()
                {
                    Id = 2,
                    Name = "Bosh"
                }
            };
            Brands = brands.ToList();
            //Brands = brands.Select(b => new _Contract.Brand { Id = b.Id, Name = b.Name }
            //).ToList();

        }



    }
}
