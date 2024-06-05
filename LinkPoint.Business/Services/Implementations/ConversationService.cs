using LinkPoint.Business.DTOs.ConversationDTOs;
using LinkPoint.Business.DTOs.MessageDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Extentions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace LinkPoint.Business.Services.Implementations;

public class ConversationService : IConversationService
{
    private readonly IConversationRepository _conversationRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IImageRepository _imageRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ConversationService(IConversationRepository conversationRepository,
        UserManager<AppUser> userManager,
        IImageRepository imageRepository,
        IMessageRepository messageRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _conversationRepository = conversationRepository;
        _userManager = userManager;
        _imageRepository = imageRepository;
        _messageRepository = messageRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task CreateConversationAsync(string UserId,string User2Id)
    {
        if (User2Id is null) throw new UserNotFoundException(404, "User is not found");
        var user=await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        Conversation conversation = new Conversation()
        {
            User1Id = user.Id,
            User2Id = User2Id,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,  
        };
        await _conversationRepository.InsertAsync(conversation);
        await _conversationRepository.CommitAsync();
    }

    public async Task<List<ConversationGetDto>> GetAllConversationsAsync(string UserId)
    {
        var conversations=await _conversationRepository.GetAllAsync(conv=>conv.User1Id==UserId || conv.User2Id==UserId, "messages.User.Images");
        List<ConversationGetDto> conversationGetDtos = new List<ConversationGetDto>();
        if (conversations.Count > 0)
        {
            foreach (var conversation in conversations)
            {
                AppUser user = null;
                if (conversation.User1Id == UserId)
                {
                     user = await _userManager.FindByIdAsync(conversation.User2Id);
                    if (user is null) throw new UserNotFoundException(404, "User is not found");
                }
                else
                {
                     user = await _userManager.FindByIdAsync(conversation.User1Id);
                    if (user is null) throw new UserNotFoundException(404, "User is not found");
                }
                var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
                if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                var messages=await _messageRepository.GetAllAsync(m => m.UserId == user.Id && m.ConversationId == conversation.Id);
                Message lastMessage = null;
                if (messages.Count > 0)
                {
                    lastMessage =messages.LastOrDefault();
                    if (lastMessage is null) throw new MessageNotFoundException(404, "Message is not found");
                }
                ConversationGetDto conversationGetDto = new ConversationGetDto
                {
                    ConversationId=conversation.Id,
                    User1Id = conversation.User1Id,
                    User2Id = conversation.User2Id,
                    UserName = user.UserName,
                    UserProfileImage = profileImage.ImageUrl,
                    LastMessage = lastMessage?.Content,
                    LastMessageDate =lastMessage?.CreatedDate.GetElapsedTime(),
                    Messages= conversation.messages.Select(m=> new MessageGetDto
                    {
                        UserName=m.User.UserName,
                        ProfileImage=m.User.Images.FirstOrDefault(i => i.IsPostImage == false).ImageUrl,
                        Content=m.Content,
                        Alignment= m.UserId==UserId ? "right" : "left",
                        SenderClass= m.UserId == UserId ? "right" : "left",
                        CreatedDate=m.CreatedDate.GetElapsedTime(),
                    }).ToList()
                };
                conversationGetDtos.Add(conversationGetDto);
            }
                return conversationGetDtos;
        }
        return conversationGetDtos;
    }

    public async Task<ConversationGetDto> GetByIdConversationAsync(int conversationId)
    {
        var conversation=await _conversationRepository.GetByIdAsync(conversationId);
        if (conversation is null) throw new ConversationNotFoundException(404, "Conversation is not found");
        var user = await _userManager.FindByIdAsync(conversation.User2Id);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
        if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        var lastMessage = await _messageRepository.GetLastMessageAsync(m => m.UserId == user.Id && m.ConversationId == conversation.Id);
        if (lastMessage is null) throw new MessageNotFoundException(404, "Message is not found");
        ConversationGetDto conversationGetDto = new ConversationGetDto
        {
            ConversationId = conversation.Id,
            User1Id = conversation.User1Id,
            User2Id = conversation.User2Id,
            UserName = user.UserName,
            UserProfileImage = profileImage.ImageUrl,
            LastMessage = lastMessage.Content,
            LastMessageDate = lastMessage.CreatedDate.GetElapsedTime(),
        };
        return conversationGetDto;
    }
}
