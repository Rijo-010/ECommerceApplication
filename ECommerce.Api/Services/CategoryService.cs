using ECommerce.Api.Data;
using ECommerce.Api.Interfaces;
using ECommerce.Api.Models.DTOs;
using ECommerce.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public CategoryDto Create(CreateCategoryDto dto)
        {
            var exists = _context.Categories
                .Any(c => c.Name == dto.Name);

            if (exists)
                throw new InvalidOperationException("Category already exists");

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return Map(category);
        }

        public CategoryDto? Update(int id, CreateCategoryDto dto)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return null;

            category.Name = dto.Name;
            category.Description = dto.Description;

            _context.SaveChanges();
            return Map(category);
        }

        public bool Deactivate(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return false;

            category.IsActive = false;
            _context.SaveChanges();
            return true;
        }

        public List<CategoryDto> GetAll()
        {
            return _context.Categories
                .AsNoTracking()
                .Select(c => Map(c))
                .ToList();
        }

        public List<CategoryDto> GetActive()
        {
            return _context.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .Select(c => Map(c))
                .ToList();
        }

        public bool Delete(int id)
        {
          var category = _context.Categories
            .Include(c => c.Products)
            .FirstOrDefault(c => c.Id == id);

            if (category == null)
                return false;

            
            if (category.Products.Any())
                throw new InvalidOperationException(
                    "Cannot delete category with existing products");

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return true;
        }


        private static CategoryDto Map(Category c)
        {
            return new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IsActive = c.IsActive
            };
        }
    }
}
