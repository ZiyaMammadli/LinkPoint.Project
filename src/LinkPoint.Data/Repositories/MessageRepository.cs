using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(LinkPointDbContext context) : base(context)
    {
    }
}
