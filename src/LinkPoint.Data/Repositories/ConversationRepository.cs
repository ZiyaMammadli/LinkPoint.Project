using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;

namespace LinkPoint.Data.Repositories;

public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
{
    public ConversationRepository(LinkPointDbContext context) : base(context)
    {
    }
}
