using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class UserInterestRepository : GenericRepository<UserInterest>, IUserInterestRepository
{
    public UserInterestRepository(LinkPointDbContext context) : base(context)
    {
    }
}
