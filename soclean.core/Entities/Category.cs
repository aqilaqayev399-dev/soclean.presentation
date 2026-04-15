using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
}
