using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13_InternetShop.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Customers")]
        public int CustomerId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }



        public IEnumerable<OrderItem> OrderItems { get; set; }
        public Customer Customer { get; set; }

    }
}
