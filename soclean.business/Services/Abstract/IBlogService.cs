using soclean.business.Dtos.Blog;
using soclean.business.Services.Abstract.Generic;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface IBlogService : ICrudService<Blog, BlogCreateDto, BlogUpdateDto, BlogDto>
{
    Task CreateAsync(BlogCreateDto vm);
    Task UpdateBlogAsync(BlogUpdateDto vm);
    Task<BlogUpdateDto> GetBlogUpdateDto(int id);
    Task<BlogDetailDto> GetAllBlogsAsync(int id);

}
