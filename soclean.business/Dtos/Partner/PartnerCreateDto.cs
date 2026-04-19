using Microsoft.AspNetCore.Http;

namespace soclean.business.Dtos.Partner;

public class PartnerCreateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;
}
