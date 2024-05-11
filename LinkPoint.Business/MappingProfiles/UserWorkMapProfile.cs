using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.MappingProfiles;

public class UserWorkMapProfile:Profile
{
    public UserWorkMapProfile()
    {
        CreateMap<UserWorkGetDto,UserWork>().ReverseMap();
        CreateMap<UserWorkPutDto,UserWork>().ReverseMap();
        CreateMap<UserWorkPostDto,UserWork>().ReverseMap();
    }
}
