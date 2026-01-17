using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Enums;
namespace ECommerce.Api.Models.Entities
{
    public class User
    {
         public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
