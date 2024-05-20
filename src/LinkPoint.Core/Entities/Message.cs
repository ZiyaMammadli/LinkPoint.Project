using System.Security.Cryptography.X509Certificates;

namespace LinkPoint.Core.Entities;

public class Message:BaseEntity
{
    public int ConversationId { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public Conversation Conversation { get; set; }
    public AppUser User { get; set; }
}
