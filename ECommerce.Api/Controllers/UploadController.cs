using Microsoft.AspNetCore.Mvc;
using ECommerce.Api.Models.DTOs;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/uploads")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("product-image")]
        public async Task<ActionResult<ImageUploadResultDto>> UploadProductImage(
            [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Invalid image type");

            var fileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine(_env.WebRootPath, "ProductImages");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Ok(new ImageUploadResultDto
            {
                ImageUrl = $"/ProductImages/{fileName}"
            });
        }
    }
}
