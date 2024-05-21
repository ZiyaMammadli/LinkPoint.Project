using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkPoint.Data.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(LinkPointDbContext context) : base(context)
    {
    }

    public async Task<Message> GetLastMessageAsync(Expression<Func<Message, bool>> expression)
    {
        return await Table.LastOrDefaultAsync(expression);
    }
}
