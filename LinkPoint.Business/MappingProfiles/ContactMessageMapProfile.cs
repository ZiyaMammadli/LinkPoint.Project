using AutoMapper;
using LinkPoint.Business.DTOs.ContactMessageDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.MappingProfiles;

public class ContactMessageMapProfile:Profile
{
    public ContactMessageMapProfile()
    {
        CreateMap<ContactMessagePostDto,ContactMessage>().ReverseMap();
        CreateMap<ContactMessageGetDto,ContactMessage>().ReverseMap();
    }
}
