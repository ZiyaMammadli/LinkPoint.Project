using AutoMapper;
using LinkPoint.Business.DTOs.ContactMessageDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Enums;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LinkPoint.Business.Services.Implementations;

public class ContactMessageService : IContactMessageService
{
    private readonly IContactMessageRepository _contactMessageRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public ContactMessageService(IContactMessageRepository contactMessageRepository,UserManager<AppUser> userManager,IMapper mapper)
    {
        _contactMessageRepository = contactMessageRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task CreateContactMessage(ContactMessagePostDto contactMessagePostDto)
    {
        var user= await _userManager.FindByIdAsync(contactMessagePostDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var contactMessage=_mapper.Map<ContactMessage>(contactMessagePostDto);
        contactMessage.Status=ContactMessageStatus.Pending;
        contactMessage.CreatedDate= DateTime.UtcNow;
        contactMessage.UpdatedDate= DateTime.UtcNow;    
        await _contactMessageRepository.InsertAsync(contactMessage);
        await _contactMessageRepository.CommitAsync();
    }

    public async Task<List<ContactMessageGetDto>> GetAllContactMessage()
    {
        var ContactMessages=await _contactMessageRepository.GetAllAsync();
        List<ContactMessageGetDto> contactMessageGetDtos = new List<ContactMessageGetDto>();
        if (ContactMessages.Count > 0)
        {
            foreach (var contactMessage in ContactMessages)
            {
                var contactMessageGetDto = _mapper.Map<ContactMessageGetDto>(contactMessage);
                contactMessageGetDtos.Add(contactMessageGetDto);
            }
        }
        return contactMessageGetDtos;
    }

    public async Task<ContactMessageGetDto> GetContactMessageById(int id)
    {
        var contactMessage=await _contactMessageRepository.GetByIdAsync(id);
        if (contactMessage is null) throw new ContactMessageNotFoundException(404, "ContactMessage is not found");
        var contactMessageGetDto = _mapper.Map<ContactMessageGetDto>(contactMessage);
        return contactMessageGetDto;
    }

    public async Task AcceptContactMessage(int ContactMessageId)
    {
        var contactMessage=await _contactMessageRepository.GetByIdAsync(ContactMessageId);
        if (contactMessage is null) throw new ContactMessageNotFoundException(404, "ContactMessage is not found");
        contactMessage.Status=ContactMessageStatus.Accepted;
        contactMessage.UpdatedDate=DateTime.UtcNow;
        await _contactMessageRepository.CommitAsync();
    }

    public async Task RejectContactMessage(int ContactMessageId)
    {
        var contactMessage = await _contactMessageRepository.GetByIdAsync(ContactMessageId);
        if (contactMessage is null) throw new ContactMessageNotFoundException(404, "ContactMessage is not found");
        contactMessage.Status = ContactMessageStatus.Rejected;
        contactMessage.UpdatedDate=DateTime.UtcNow;
        await _contactMessageRepository.CommitAsync();
    }
}
