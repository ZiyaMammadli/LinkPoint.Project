using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class UserAboutRepository : GenericRepository<UserAbout>, IUserAboutRepository
{
    public UserAboutRepository(LinkPointDbContext context) : base(context)
    {
    }
}
