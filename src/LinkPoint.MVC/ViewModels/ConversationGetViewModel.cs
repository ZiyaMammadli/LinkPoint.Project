namespace LinkPoint.MVC.ViewModels;

public class ConversationGetViewModel
{
    public int ConversationId { get; set; }
    public string User1Id { get; set; }
    public string User2Id { get; set; }
    public string UserName { get; set; }
    public string UserProfileImage { get; set; }
    public string? LastMessage { get; set; }
    public List<MessageGetViewModel> Messages { get; set; }
    public DateTime? LastMessageDate { get; set; }
}
