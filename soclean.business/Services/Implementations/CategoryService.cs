using AutoMapper;
using soclean.business.Dtos.Category;
using soclean.business.Exceptions;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;
using soclean.dataccess.Repositories.Abstract.Generic;

namespace soclean.business.Services.Implementations;

public class CategoryService : CrudService<Category, CategoryCreateDto, CategoryUpdateDto, CategoryDto>, ICategoryService   
{
    private readonly ICloudinaryManager _cloudinaryManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryService(ICategoryRepository repository, IMapper mapper, ICategoryRepository categoryRepository, ICloudinaryManager cloudinaryManager) : base(repository, mapper)
    {
        _categoryRepository = categoryRepository;
        _cloudinaryManager = cloudinaryManager;
        _mapper = mapper;
    }


    public async Task<List<CategoryDto>> GetCategoriesForEditAsync(int id)
    {
        var categories =  _categoryRepository.GetAll();

        // özünü çıxart
        return categories
            .Where(c => c.Id != id)
            .Select(c => _mapper.Map<CategoryDto>(c))
            .ToList();
    }
    public async Task CreateAsync(CategoryCreateDto dto)
    {
        if (dto.PictureFile == null)
            throw new NotFoundException("Picture is required");

        string fileName = await _cloudinaryManager.FileCreateAsync(dto.PictureFile);

        var category = new Category
        {
            Name = dto.Name,
            PictureFile = fileName,
            ParentCategoryId = dto.ParentCategoryId
        };

        await _categoryRepository.CreateAsync(category);

    }


    public async Task<CategoryUpdateDto> GetUpdateDtoAsync(int id)
    {
        var category = await _categoryRepository.GetAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        var dto = new CategoryUpdateDto
        {
            Id = category.Id,
            Name = category.Name,
            Picture = category.PictureFile,
            ParentCategoryId = category.ParentCategoryId
        };

        return dto;
    }
    public  async Task UpdateAsync(CategoryUpdateDto dto)
    {
        var existCategory = await _categoryRepository.GetAsync(dto.Id);
        if (existCategory == null)
            throw new Exception("Category not found");

        if (dto.ParentCategoryId == dto.Id)
            throw new Exception("Category cannot be its own parent");

        if (dto.PictureFile != null)
        {
            string newFileName = await _cloudinaryManager.FileCreateAsync(dto.PictureFile);

            _cloudinaryManager.FileDeleteAsync(existCategory.PictureFile);

            existCategory.PictureFile = newFileName;
        }

        existCategory.Name = dto.Name;
        existCategory.ParentCategoryId = dto.ParentCategoryId;

        await _categoryRepository.SaveChangesAsync();

    }


    public async Task<CategoryEditVm> GetEditVmAsync(int id)
    {
        var category = await _categoryRepository.GetAsync(id);

        if (category == null)
            throw new NotFoundException("Category not found");

        var vm = new CategoryEditVm
        {
            Category = new CategoryUpdateDto
            {
                Id = category.Id,
                Name = category.Name,
                Picture = category.PictureFile,
                ParentCategoryId = category.ParentCategoryId
            },
            Categories = ( _categoryRepository.GetAll())
                .Where(c => c.Id != id)
                .Select(c => _mapper.Map<CategoryDto>(c))
                .ToList()
        };

        return vm;
    }


   
}
public class CategoryEditVm
{
    public CategoryUpdateDto Category { get; set; }
    public List<CategoryDto> Categories { get; set; }
}