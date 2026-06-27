using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;
using soclean.business.Dtos.Product;

namespace soclean.business.Dtos.Category;

public class CategoryUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Picture { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;

    public int? ParentCategoryId { get; set; }
    public CategoryDto? ParentCategory { get; set; }

    public List<CategoryDto> SubCategories { get; set; } = new();

    public List<ProductDto> Products { get; set; } = new();
}
