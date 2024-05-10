using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.MappingProfiles;

public class UserInterestMapProfile:Profile
{
    public UserInterestMapProfile()
    {
        CreateMap<UserInterestGetDto,UserInterest>().ReverseMap();
        CreateMap<UserInterestPostDto,UserInterest>().ReverseMap();
    }
}
