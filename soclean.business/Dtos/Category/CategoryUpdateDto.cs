namespace soclean.business.Dtos.Category;

public class CategoryUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
}
