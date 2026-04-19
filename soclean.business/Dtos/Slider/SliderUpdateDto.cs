using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Slider;

public class SliderUpdateDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;
    public string Picture { get; set; } = null!;
}
