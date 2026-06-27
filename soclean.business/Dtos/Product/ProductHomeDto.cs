using soclean.business.Dtos.Category;
using soclean.business.Helper.Extension;

namespace soclean.business.Dtos.Product;

public class ProductHomeDto
{
    public int Id { get; set; }
    public PaginationResponse<ProductDto>? Products { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];

    public int? page { get; set; }


}
