using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace LinkPoint.Business.Hubs;

public class ChatHub:Hub
{
    private readonly IMessageRepository _messageRepository;

    public ChatHub(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    public async Task SendMessageAsync(int conversationId,string userId, string messageContent)
    {
        Message message = new Message()
        {
            ConversationId = conversationId,
            UserId = userId,
            Content = messageContent,
            CreatedDate = DateTime.UtcNow,
        };
        await _messageRepository.InsertAsync(message);
        await _messageRepository.CommitAsync();
        await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage",userId, messageContent);
    }

    public async Task JoinConservationAsync(int conversationId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
    }

    public async Task LeaveConservationAsync(int conversationId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
    }
}
