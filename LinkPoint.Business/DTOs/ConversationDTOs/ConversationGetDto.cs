using LinkPoint.Business.DTOs.MessageDTOs;

namespace LinkPoint.Business.DTOs.ConversationDTOs;

public class ConversationGetDto
{
    public int ConversationId { get; set; }
    public string User1Id { get; set; }
    public string User2Id { get; set; }
    public string UserName { get; set; }
    public string UserProfileImage { get; set; }
    public string? LastMessage { get; set; }
    public DateTime? LastMessageDate { get; set; }
    public List<MessageGetDto> Messages { get; set; }
}
