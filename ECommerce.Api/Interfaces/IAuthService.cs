using ECommerce.Api.Models.DTOs;
namespace Ecommerce.Api.Interfaces
{
    public interface IAuthService
    {
        UserDto? Login(LoginRequestDto request);
    }
}