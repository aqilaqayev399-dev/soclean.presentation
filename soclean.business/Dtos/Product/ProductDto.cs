using soclean.business.Dtos.Base;
using soclean.business.Dtos.Category;

namespace soclean.business.Dtos.Product;

public class ProductDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public  CategoryDto category { get; set; }
}

