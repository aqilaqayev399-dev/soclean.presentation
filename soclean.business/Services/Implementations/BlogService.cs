using AutoMapper;
using Microsoft.EntityFrameworkCore;
using soclean.business.Dtos.Blog;
using soclean.business.Exceptions;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;

namespace soclean.business.Services.Implementations;

public class BlogService : CrudService<Blog, BlogCreateDto, BlogUpdateDto, BlogDto>, IBlogService
{

    private readonly IBlogRepository _blogRepository;
    private readonly ICloudinaryManager _cloudinaryManager;
    public BlogService(IBlogRepository repository, IMapper mapper, ICloudinaryManager cloudinaryManager) : base(repository, mapper)
    {
        _blogRepository = repository;
        _cloudinaryManager = cloudinaryManager;
    }

    public async Task<BlogDetailDto> GetAllBlogsAsync(int id)
    {
        var blog = await _blogRepository.GetAsync(id);

        if (blog == null)
        {
            throw new NotFoundException();
        }

        var latestBlogs = await _blogRepository
            .GetAll()
            .Where(x => x.Id != id)
            .OrderByDescending(x => x.Id)
            .Take(5)
            .ToListAsync();

        return new BlogDetailDto
        {
            Blog = new BlogDto
            {
                Id = blog.Id,
                Name = blog.Name,
                Text = blog.Text,
                PictureFile = blog.PictureFile
            },

            LatesBlogs = latestBlogs.Select(x => new BlogDto
            {
                Id = x.Id,
                Name = x.Name,
                Text = x.Text,
                PictureFile = x.PictureFile
            }).ToList()
        };
    }
    public async Task UpdateBlogAsync(BlogUpdateDto vm)
    {
        var blog = await _blogRepository.GetAsync(vm.Id);
        if (blog == null)
        {
            throw new NotFoundException();
        }

        if (vm.PictureFile != null)
        {
            await _cloudinaryManager.FileDeleteAsync(blog.PictureFile);
            var image = await _cloudinaryManager.FileCreateAsync(vm.PictureFile);
            blog.PictureFile = image;

        }

        blog.Name = vm.Name;
        blog.Text = vm.Text;

        _blogRepository.Update(blog);
        await _blogRepository.SaveChangesAsync();
    }

    public async Task CreateAsync(BlogCreateDto vm)
    {


        var image = await _cloudinaryManager.FileCreateAsync(vm.PictureFile);

        var blog = new Blog
        {
            Name = vm.Name,
            Text = vm.Text,
            PictureFile = image

        };

        await _blogRepository.CreateAsync(blog);

    }

    public async Task<BlogUpdateDto> GetBlogUpdateDto(int id)
    {
        var blog = await _blogRepository.GetAsync(id);
        if (blog == null)
        {
            throw new NotFoundException();
        }


        var update = new BlogUpdateDto
        {
            Id = blog.Id,
            Name = blog.Name,
            Text = blog.Text,
            Picture = blog.PictureFile,
        };

        return update;
    }

}
