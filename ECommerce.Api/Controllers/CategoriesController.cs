using System.Net;
using ECommerce.Api.Interfaces;
using ECommerce.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto dto)
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
        public IActionResult Update(int id, CreateCategoryDto dto)
        {
            var result = _service.Update(id, dto);
            if (result == null)
                return NotFound();

            return Ok(result);
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
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("active")]
        public IActionResult GetActive()
        {
            return Ok(_service.GetActive());
        }


        
        [HttpDelete("hard/{id}")]
        public IActionResult Delete(int id)
        {
          try{  var success = _service.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
          } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
