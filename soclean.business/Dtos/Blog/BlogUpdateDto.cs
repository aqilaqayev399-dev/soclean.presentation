using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Blog;

public class BlogUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Text { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;
    public string Picture  { get; set; } = null!;
}
