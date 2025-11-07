using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.GlobalConstants;

namespace BadAPI.Data.Entities
{
    [Serializable]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Product_Name_Max_Length)]
        public string? Name { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category CategoryName { get; set; } = new Category();

        [NotMapped]
        public string? InternalCode { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}