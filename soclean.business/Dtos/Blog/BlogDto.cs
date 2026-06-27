using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Blog;

public class BlogDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string PictureFile { get; set; } = null!;
}


public class BlogDetailDto : IDto
{
    public BlogDto Blog { get; set; } = null!;
    public List<BlogDto> LatesBlogs { get; set; } = null!;
}