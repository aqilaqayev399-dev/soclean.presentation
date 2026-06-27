using soclean.business.Dtos.Product;
using soclean.business.Helper.Extension;
using soclean.business.Services.Abstract.Generic;
using soclean.core.Entities;

namespace soclean.business.Services.Abstract;

public interface IProductService :ICrudService<Product,ProductCreateDto,ProductUpdateDto,ProductDto>
{
    Task ProductCreate(ProductCreateDto dto);
    Task<ProductUpdateDto?> GetProductForEditAsync(int id);
    Task UpdateProductAsync(ProductUpdateDto dto);
    Task<PaginationResponse<ProductDto>> GetFilteredPaginatedToursAsync(string? Title, int page, int take);
}
