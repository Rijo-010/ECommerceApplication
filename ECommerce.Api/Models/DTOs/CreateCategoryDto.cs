using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Models.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
