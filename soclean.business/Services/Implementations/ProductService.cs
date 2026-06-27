using AutoMapper;
using soclean.business.Dtos.Product;
using soclean.business.Helper.Extension;
using soclean.business.Services.Abstract;
using soclean.business.Services.Implementations.Generic;
using soclean.core.Entities;
using soclean.dataccess.Repositories.Abstract;

namespace soclean.business.Services.Implementations;

public class ProductService : CrudService<Product, ProductCreateDto, ProductUpdateDto, ProductDto>, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICloudinaryManager _cloudinaryManager;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository repository, IMapper mapper, ICategoryRepository categoryRepository, ICloudinaryManager cloudinaryManager) : base(repository, mapper)
    {
        _productRepository = repository;
        _categoryRepository = categoryRepository;
        _cloudinaryManager = cloudinaryManager;
        _mapper = mapper;
    }


    public async Task<ProductUpdateDto?> GetProductForEditAsync(int id)
    {
        var product = await _productRepository.GetAsync(id);

        if (product == null)
            return null;

        return new ProductUpdateDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId
        };
    }
    public async Task UpdateProductAsync(ProductUpdateDto dto)
    {
        var product = await _productRepository.GetAsync(dto.Id);

        if (product == null)
            throw new Exception("Product not found");

        var category = await _categoryRepository.GetAsync(dto.CategoryId);

        if (category == null)
            throw new Exception("Category not found");

        
        if (dto.PictureFile != null)
        {
            string fileName =
                await _cloudinaryManager.FileCreateAsync(dto.PictureFile);

            product.PictureFile = fileName;
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.CategoryId = dto.CategoryId;

        await _productRepository.SaveChangesAsync();
    }
    public async Task ProductCreate(ProductCreateDto dto)
    {
        if (dto.Picture == null)
            throw new Exception("Picture is required");

        // Category mövcuddurmu?
        var existCategory = await _categoryRepository.GetAsync
          (dto.CategoryId);

        if (existCategory == null)
            throw new Exception("Category not found");

        string fileName = await _cloudinaryManager.FileCreateAsync(dto.Picture);

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            PictureFile = fileName,
            CategoryId = dto.CategoryId
        };

        await _productRepository.CreateAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task<PaginationResponse<ProductDto>> GetFilteredPaginatedToursAsync(
      string? Title,

      int page,
      int take)
    {
        var products = await GetAllAsync();

        // Search filter
        if (!string.IsNullOrWhiteSpace(Title))
        {
            products = products
                .Where(p => p.Name.Contains(Title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

      

       
      

     
      

        // Pagination
        var totalCount = products.Count;
        var paginated = products
            .Skip((page - 1) * take)
            .Take(take)
            .ToList();

        var data = _mapper.Map<List<ProductDto>>(paginated);

        return new PaginationResponse<ProductDto>(
            data,
            (int)Math.Ceiling((decimal)totalCount / take),
            page,
            totalCount
        );
    }

}
