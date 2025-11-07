using BadAPI.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadAPI.Data.Entities
{
    public class Review : BaseEntity
    {
        [Required]
        [MaxLength(1000)]
        public string? Comment { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ReviewerName { get; set; }

        // Enum - Rating from 1 to 5
        public Rating Rating { get; set; } = new Rating();

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
