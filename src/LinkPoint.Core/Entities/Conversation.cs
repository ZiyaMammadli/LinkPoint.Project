namespace LinkPoint.Core.Entities;

public class Conversation:BaseEntity
{
    public string User1Id { get; set; }
    public string User2Id { get; set; }
    public DateTime? LastMessageDate { get; set; }
    public AppUser User1 { get; set; }
    public AppUser User2 { get; set; }
    public List<Message> messages { get; set; }
}
