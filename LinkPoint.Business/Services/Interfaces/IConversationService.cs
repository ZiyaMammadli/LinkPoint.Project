using LinkPoint.Business.DTOs.ConversationDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IConversationService
{
    Task<List<ConversationGetDto>> GetAllConversationsAsync();//
    Task<ConversationGetDto> GetByIdConversationAsync(int conversationId);//
    Task CreateConversationAsync(string User2Id);// 
}
