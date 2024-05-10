using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class UserEducationRepository : GenericRepository<UserEducation>, IUserEducationRepository
{
    public UserEducationRepository(LinkPointDbContext context) : base(context)
    {
    }
}
