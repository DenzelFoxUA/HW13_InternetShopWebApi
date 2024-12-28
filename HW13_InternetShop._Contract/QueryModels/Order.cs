using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13_InternetShop._Contract.QueryModels
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }


        public IEnumerable<OrderItem> OrderItems { get; set; }

        //[Required]
        //public bool IsCompleted { get; set; }
    }
}
