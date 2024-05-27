using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkPoint.Core.Repositories;

public interface IAppUserRepository
{
    DbSet<AppUser> Table { get; }
    Task InsertAsync(AppUser user);
    Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes);
    Task<AppUser> GetSingleAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes);
    Task<AppUser> GetByIdAsync(int id);
    void Delete(AppUser user);
    Task<int> CommitAsync();
    Task<bool> IsExist(Expression<Func<AppUser, bool>> expression);
}
