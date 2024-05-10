using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class UserWorkRepository : GenericRepository<UserWork>, IUserWorkRepository
{
    public UserWorkRepository(LinkPointDbContext context) : base(context)
    {
    }
}
