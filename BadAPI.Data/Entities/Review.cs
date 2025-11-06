using BadAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadAPI.Data.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

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
        public string? ProductName { get; set; }
    }
}
