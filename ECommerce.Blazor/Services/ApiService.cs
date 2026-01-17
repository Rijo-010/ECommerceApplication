using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace ECommerce.Blazor.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }
    public string BaseUrl => _http.BaseAddress!.ToString().TrimEnd('/');
       
        public async Task<LoginResult?> LoginAsync(string username, string password)
        {
            var response = await _http.PostAsJsonAsync(
                "api/auth/login",
                new { username, password }
            );

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LoginResult>();
        }

        
public async Task<List<CategoryDto>> GetAllCategoriesAsync()
{
    return await _http.GetFromJsonAsync<List<CategoryDto>>("api/categories")
           ?? new();
}

public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
{
    return await _http.GetFromJsonAsync<CategoryDto>($"api/categories/{id}");
}

public async Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto dto)
{
    var response = await _http.PostAsJsonAsync("api/categories", dto);
    if (!response.IsSuccessStatusCode) return null;

    return await response.Content.ReadFromJsonAsync<CategoryDto>();
}

public async Task<bool> DeactivateCategoryAsync(int id)
{
    var response = await _http.DeleteAsync($"api/categories/{id}");
    return response.IsSuccessStatusCode;
}

public async Task<bool> DeleteCategoryAsync(int id)
{
    var response = await _http.DeleteAsync($"api/categories/hard/{id}");
    return response.IsSuccessStatusCode;
}

public async Task<List<ProductDto>> GetAllProductsAsync()
{
    return await _http.GetFromJsonAsync<List<ProductDto>>("api/products/admin/all")
           ?? new();
}


public async Task<ProductDto?> CreateProductAsync(CreateProductDto dto)
{
    var response = await _http.PostAsJsonAsync("api/products", dto);
    if (!response.IsSuccessStatusCode) return null;

    return await response.Content.ReadFromJsonAsync<ProductDto>();
}

public async Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto)
{
    var response = await _http.PutAsJsonAsync($"api/products/{id}", dto);
    if (!response.IsSuccessStatusCode) return null;

    return await response.Content.ReadFromJsonAsync<ProductDto>();
}

public async Task<bool> DeactivateProductAsync(int id)
{
    var response = await _http.DeleteAsync($"api/products/{id}");
    return response.IsSuccessStatusCode;
}

public async Task<bool> DeleteProductAsync(int id)
{
    var response = await _http.DeleteAsync($"api/products/admin/{id}");
    return response.IsSuccessStatusCode;
}

public async Task<string?> UploadProductImageAsync(IBrowserFile file)
{
    var content = new MultipartFormDataContent();

    var stream = file.OpenReadStream(5_000_000);
    var streamContent = new StreamContent(stream);
    streamContent.Headers.ContentType =
        new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

    content.Add(streamContent, "file", file.Name);

    var response = await _http.PostAsync("api/uploads/product-image", content);

    if (!response.IsSuccessStatusCode)
        return null;

    var result = await response.Content.ReadFromJsonAsync<ImageUploadResult>();
    return result?.ImageUrl;
}

public class ImageUploadResult
{
    public string ImageUrl { get; set; } = "";
}

public async Task<List<CategoryDto>> GetActiveCategoriesAsync()
{
    return await _http.GetFromJsonAsync<List<CategoryDto>>(
        "api/categories/active"
    ) ?? new();
}
public async Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId)
{
    return await _http.GetFromJsonAsync<List<ProductDto>>(
        $"api/products/category/{categoryId}"
    ) ?? new();
}


}
}