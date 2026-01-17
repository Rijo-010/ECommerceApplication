using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Api.Models.Entities
{
    public class Product
    {
        
         public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [MaxLength(900)]
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
