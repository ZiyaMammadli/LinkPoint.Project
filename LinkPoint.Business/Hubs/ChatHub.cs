using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LinkPoint.Business.Hubs;

public class ChatHub : Hub
{
    private readonly static Dictionary<string, string> OnlineUsers = new Dictionary<string, string>();
    private readonly IMessageRepository _messageRepository;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IConversationRepository _conversationRepository;

    public ChatHub(IMessageRepository messageRepository,IAppUserRepository appUserRepository,IConversationRepository conversationRepository)
    {

        _messageRepository = messageRepository;
        _appUserRepository = appUserRepository;
        _conversationRepository = conversationRepository;
    }
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var connectionId = Context.ConnectionId;

        if (userId != null)
        {
            lock (OnlineUsers)
            {
                if (!OnlineUsers.ContainsKey(userId))
                {
                    OnlineUsers.Add(userId, connectionId);
                }
            }

            var user = await _appUserRepository.GetSingleAsync(u => u.Id == userId, "Images");
            var userProfileImage = user.Images.FirstOrDefault(i => i.IsPostImage==false)?.ImageUrl;
            var userName = user.UserName;

            await Clients.All.SendAsync("UserConnected", userId, userName, userProfileImage,OnlineUsers.Count);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null)
        {
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(userId))
                {
                    OnlineUsers.Remove(userId);
                }
            }

            await Clients.All.SendAsync("UserDisconnected", userId);
        }

        await base.OnDisconnectedAsync(exception);
    }
    public async Task SendMessageAsync(int conversationId, string userId, string messageContent)
    {
        try
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
            var user = await _appUserRepository.GetSingleAsync(u => u.Id == userId, "Images");
            var userProfileImage = user.Images.FirstOrDefault(i => i.IsPostImage == false).ImageUrl;
            var conversation=await _conversationRepository.GetByIdAsync(conversationId);
            var userName = user.UserName;
            var UserId= userId;

            await Clients.All.SendAsync("ReceiveMessage", conversationId, userName, userProfileImage, messageContent, null, null, UserId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
        
    }

    //public async Task JoinConversationAsync(int conversationId)
    //{
    //    await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
    //}

    //public async Task LeaveConversationAsync(int conversationId)
    //{
    //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
    //}
}

