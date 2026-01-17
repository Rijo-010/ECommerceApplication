using ECommerce.Api.Data;
using ECommerce.Api.Interfaces;
using ECommerce.Api.Models.DTOs;
using ECommerce.Api.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public ProductDto Create(CreateProductDto dto)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Id == dto.CategoryId && c.IsActive);

            if (category == null)
                throw new InvalidOperationException("Invalid category");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                IsActive = true
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return Map(product, category.Name);
        }

        public ProductDto? Update(int id, CreateProductDto dto)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return null;

            var category = _context.Categories
                .FirstOrDefault(c => c.Id == dto.CategoryId && c.IsActive);

            if (category == null)
                throw new InvalidOperationException("Invalid category");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.ImageUrl = dto.ImageUrl;
            product.CategoryId = dto.CategoryId;

            _context.SaveChanges();

            return Map(product, category.Name);
        }

        public bool Deactivate(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return false;

            product.IsActive = false;
            _context.SaveChanges();
            return true;
        }

        public List<ProductDto> GetAll()
        {
            return _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Select(p => Map(p, p.Category.Name))
                .ToList();
        }

        public List<ProductDto> GetActive()
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.Category.IsActive)
                .AsNoTracking()
                .Select(p => Map(p, p.Category.Name))
                .ToList();
        }

        public ProductDto? GetById(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id && p.IsActive);

            if (product == null)
                return null;

            return Map(product, product.Category.Name);
        }

        public List<ProductDto> GetByCategory(int categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId &&
                            p.IsActive &&
                            p.Category.IsActive)
                .AsNoTracking()
                .Select(p => Map(p, p.Category.Name))
                .ToList();
        }



        public bool Delete(int id)
        {
            var product =_context.Products.Find(id);
            if(product==null){return false;}
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        private static ProductDto Map(Product p, string categoryName)
        {
            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = categoryName
            };
        }
    }
}
