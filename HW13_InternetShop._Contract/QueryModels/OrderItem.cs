
using System.ComponentModel.DataAnnotations;


namespace HW13_InternetShop._Contract.QueryModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required]
        
        public int ProductId { get; set; }
        //[Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(100)]
        public string BrandName { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        
        public int Count { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
    }
}
