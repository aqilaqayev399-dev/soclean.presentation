using Microsoft.AspNetCore.Http;
using soclean.business.Dtos.Base;

namespace soclean.business.Dtos.Blog;

public class BlogCreateDto : IDto
{
    public string Name { get; set; } = null!;
    public string Text { get; set; } = null!;
    public IFormFile PictureFile { get; set; } = null!;

}
