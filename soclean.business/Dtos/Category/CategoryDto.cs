using soclean.business.Dtos.Product;

namespace soclean.business.Dtos.Category;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
    public List<ProductDto> Products { get; set; } = [];
}
