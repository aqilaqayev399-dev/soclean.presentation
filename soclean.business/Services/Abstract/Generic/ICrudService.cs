using Microsoft.EntityFrameworkCore.Query;
using soclean.business.Dtos.Base;
using soclean.core.Entities.Base;
using System.Linq.Expressions;

namespace soclean.business.Services.Abstract.Generic;

public interface ICrudService<TEntity, TCreateDto, TUpdateDto, TDto>
where TEntity : BaseEntity
where TCreateDto : IDto
where TUpdateDto : IDto
where TDto : IDto
{
    Task SaveChangesAsync();
    Task<TDto?> GetAsync(int id);
    Task<TDto?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool enableTracking = true);
    Task<TDto> CreateAsync(TCreateDto entity);
    Task<TDto> UpdateAsync(TUpdateDto entity);
    Task<TDto> DeleteAsync(int id);
}
