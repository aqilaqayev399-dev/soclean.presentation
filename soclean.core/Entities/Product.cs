using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
    public int CategoryId { get; set; } 
}
