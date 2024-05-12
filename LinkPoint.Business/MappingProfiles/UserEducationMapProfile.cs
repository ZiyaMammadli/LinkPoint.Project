using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.MappingProfiles;

public class UserEducationMapProfile:Profile
{
    public UserEducationMapProfile()
    {
        CreateMap<UserEducationPutDto, UserEducation>().ReverseMap();
        CreateMap<UserEducationPostDto, UserEducation>().ReverseMap();
        CreateMap<UserEducationGetDto, UserEducation>().ReverseMap();
    }
}
