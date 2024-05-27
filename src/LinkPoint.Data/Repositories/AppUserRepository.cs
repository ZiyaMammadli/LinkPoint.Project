using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkPoint.Data.Repositories;

public class AppUserRepository:IAppUserRepository
{
    private readonly LinkPointDbContext _context;
    public AppUserRepository(LinkPointDbContext context)
    {
        _context = context;
    }
    public DbSet<AppUser> Table => _context.Set<AppUser>();

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Delete(AppUser user)
    {
        Table.Remove(user);
    }

    public async Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return expression is not null
            ? await query.Where(expression).ToListAsync()
            : await query.ToListAsync();
    }

    public async Task<AppUser> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<AppUser> GetSingleAsync(Expression<Func<AppUser, bool>> expression = null, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return expression is not null
            ? await query.Where(expression).FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync();
    }

    public async Task InsertAsync(AppUser user)
    {
        await Table.AddAsync(user);
    }
    public async Task<bool> IsExist(Expression<Func<AppUser, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }
}
