using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.MappingProfiles;

public class UserAboutMapProfile:Profile
{
    public UserAboutMapProfile()
    {
        CreateMap<UserAboutPutDto, UserAbout>().ReverseMap();
        CreateMap<UserAboutGetDto, UserAbout>().ReverseMap();
    }
}
