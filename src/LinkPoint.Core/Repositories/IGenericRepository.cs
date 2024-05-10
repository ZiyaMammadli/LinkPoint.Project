using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkPoint.Core.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    DbSet<TEntity> Table { get; }
    Task InsertAsync(TEntity entity);

    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression = null, params string[] includes);
    Task<TEntity> GetByIdAsync(int id);
    void Delete(TEntity entity);
    Task<int> CommitAsync();
}
