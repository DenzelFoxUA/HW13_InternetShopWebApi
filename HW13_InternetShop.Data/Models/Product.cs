using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13_InternetShop.Data.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Brands")]
        public int BrandId { get; set; }

        [Required]
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }


        public IEnumerable<OrderItem> OrderItems { get; set; }
        public Category Category { get; set; }
    }
}
