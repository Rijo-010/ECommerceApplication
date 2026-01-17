using ECommerce.Api.Interfaces;
using ECommerce.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        

        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            try
            {
                var result = _service.Create(dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateProductDto dto)
        {
            try
            {
                var result = _service.Update(id, dto);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deactivate(int id)
        {
            var success = _service.Deactivate(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        

        [HttpGet]
        public IActionResult GetActive()
        {
            return Ok(_service.GetActive());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            return Ok(_service.GetByCategory(categoryId));
        }

        

        [HttpGet("admin/all")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpDelete("admin/{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
