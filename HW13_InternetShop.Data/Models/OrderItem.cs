using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13_InternetShop.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Orders")]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Products")]
        public int ProductId { get; set; }

        [Required]
        public int Count { get; set; }


        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
