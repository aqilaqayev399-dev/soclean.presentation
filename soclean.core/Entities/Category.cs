using soclean.core.Entities.Base;

namespace soclean.core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string PictureFile { get; set; } = null!;

    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }

    public List<Category> SubCategories { get; set; } = new();

    public List<Product> Products { get; set; } = new();

}
