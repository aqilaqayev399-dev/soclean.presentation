using soclean.business.Dtos.Category;
using soclean.business.Services.Abstract.Generic;
using soclean.business.Services.Implementations;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface ICategoryService : ICrudService<Category, CategoryCreateDto, CategoryUpdateDto, CategoryDto>
{
    Task CreateAsync(CategoryCreateDto dto);
    Task UpdateAsync(CategoryUpdateDto dto);
    Task<CategoryUpdateDto> GetUpdateDtoAsync(int id);
    Task<List<CategoryDto>> GetCategoriesForEditAsync(int id);
    Task<CategoryEditVm> GetEditVmAsync(int id);

}
