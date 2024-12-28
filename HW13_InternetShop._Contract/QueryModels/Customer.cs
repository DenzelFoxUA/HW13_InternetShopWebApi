using System.ComponentModel.DataAnnotations;

namespace HW13_InternetShop._Contract.QueryModels
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(14)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public IEnumerable<int> OrderIndexes { get; set; }


    }
}
