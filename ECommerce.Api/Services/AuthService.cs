using Ecommerce.Api.Interfaces;
using ECommerce.Api.Data;
using ECommerce.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public UserDto? Login(LoginRequestDto request)
        {
            var user = _context.Users
                .AsNoTracking()
                .FirstOrDefault(u =>
                    u.Username == request.Username &&
                    u.Password == request.Password &&
                    u.IsActive);

            if (user == null)
                return null;

            return new UserDto
            {
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }
    }
}
