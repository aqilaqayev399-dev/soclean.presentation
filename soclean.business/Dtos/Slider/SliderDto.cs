using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Slider;

public class SldierDto : IDto
{
    public int Id {  get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
}
