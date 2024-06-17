using LinkPoint.Business.DTOs.ContactMessageDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IContactMessageService
{
    Task CreateContactMessage(ContactMessagePostDto contactMessagePostDto);
    Task<List<ContactMessageGetDto>> GetAllContactMessage();
    Task<ContactMessageGetDto> GetContactMessageById(int id);
    Task AcceptContactMessage(int ContactMessageId);
    Task RejectContactMessage(int ContactMessageId);
}
