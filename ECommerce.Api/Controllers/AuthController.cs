using Ecommerce.Api.Interfaces;
using ECommerce.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Login(request);

            if (result == null)
                return Unauthorized("Invalid username or password");

            return Ok(result);
        }
    }
}
