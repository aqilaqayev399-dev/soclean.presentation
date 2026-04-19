using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Product;

public class ProductDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
    public int CategoryId { get; set; }
}

