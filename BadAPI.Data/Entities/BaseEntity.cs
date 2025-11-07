using System.ComponentModel.DataAnnotations;

namespace BadAPI.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
