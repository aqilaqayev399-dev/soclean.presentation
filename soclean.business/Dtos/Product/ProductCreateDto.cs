using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Product;

public class ProductCreateDto : IDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile Picture { get; set; } = null!;
    public int CategoryId { get; set; }
}

