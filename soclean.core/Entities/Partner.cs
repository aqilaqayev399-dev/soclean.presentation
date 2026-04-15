using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Partner : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
}
