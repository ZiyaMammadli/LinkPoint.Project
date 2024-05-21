using LinkPoint.Core.Entities;
using System.Linq.Expressions;

namespace LinkPoint.Core.Repositories;

public interface IMessageRepository:IGenericRepository<Message>
{
    Task<Message> GetLastMessageAsync(Expression<Func<Message, bool>> expression);
}
