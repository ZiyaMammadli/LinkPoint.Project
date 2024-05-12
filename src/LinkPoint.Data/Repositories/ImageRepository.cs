using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class ImageRepository : GenericRepository<Image>, IImageRepository
{
    public ImageRepository(LinkPointDbContext context) : base(context)
    {
    }
}
