using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Slider : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFile { get; set;} = null!;
}