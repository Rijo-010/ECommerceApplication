using ECommerce.Api.Models.DTOs;

namespace ECommerce.Api.Interfaces
{
    public interface IProductService
    {
        ProductDto Create(CreateProductDto dto);
        ProductDto? Update(int id, CreateProductDto dto);
        bool Deactivate(int id);

        List<ProductDto> GetAll();         
        List<ProductDto> GetActive();      
        ProductDto? GetById(int id);        
        List<ProductDto> GetByCategory(int categoryId); 
        
        bool Delete(int id);
    }
}
