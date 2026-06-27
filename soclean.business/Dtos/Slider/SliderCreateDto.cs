using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Slider;

public class SliderCreateDto : IDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;
}
