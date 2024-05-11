using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class FriendShipRepository : GenericRepository<FriendShip>, IFriendShipRepository
{
    public FriendShipRepository(LinkPointDbContext context) : base(context)
    {
    }
}
