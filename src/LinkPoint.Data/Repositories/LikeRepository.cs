using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    public LikeRepository(LinkPointDbContext context) : base(context)
    {
    }
}
