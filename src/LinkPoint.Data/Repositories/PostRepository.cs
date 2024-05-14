using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(LinkPointDbContext context) : base(context)
    {
    }
}
