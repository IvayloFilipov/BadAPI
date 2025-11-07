using System.ComponentModel.DataAnnotations;

using static Common.GlobalConstants;

namespace BadAPI.Data.Entities
{
    [Serializable]
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(Category_Name_Max_Length)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(Category_Description_Max_Length)]
        public string? Description { get; set; }
    }
}