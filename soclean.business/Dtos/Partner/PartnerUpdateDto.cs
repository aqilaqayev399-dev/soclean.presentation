using Microsoft.AspNetCore.Http;

namespace soclean.business.Dtos.Partner;

public class PartnerUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;
    public string Picture { get; set; } = null!;
}
