using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Product;

public class ProductUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;
    public string OldPicture { get; set; } = null!;
    public int CategoryId { get; set; }
}

