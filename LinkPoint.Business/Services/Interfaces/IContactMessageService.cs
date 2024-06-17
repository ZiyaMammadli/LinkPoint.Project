using LinkPoint.Business.DTOs.ContactMessageDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IContactMessageService
{
    Task CreateContactMessageAsync(ContactMessagePostDto contactMessagePostDto);
    Task<List<ContactMessageGetDto>> GetAllContactMessageAsync();
    Task<List<ContactMessageGetDto>> GetAllContactMessagesForUserAsync(string userId);
    Task<ContactMessageGetDto> GetContactMessageByIdAsync(int id);
    Task AcceptContactMessageAsync(int ContactMessageId);
    Task RejectContactMessageAsync(int ContactMessageId);
}
