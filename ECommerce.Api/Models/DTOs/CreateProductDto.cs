using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Models.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Range(0, 999999)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [MaxLength(300)]
        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
