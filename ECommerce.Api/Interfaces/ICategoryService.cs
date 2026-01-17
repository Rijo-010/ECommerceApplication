using ECommerce.Api.Models.DTOs;

namespace ECommerce.Api.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto Create(CreateCategoryDto dto);
        CategoryDto? Update(int id, CreateCategoryDto dto);
        bool Deactivate(int id);
        bool Activate(int id);
        List<CategoryDto> GetAll();
        List<CategoryDto> GetActive();

        bool Delete(int id);
    }
}
