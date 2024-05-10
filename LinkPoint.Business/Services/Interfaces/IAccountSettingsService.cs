using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAccountSettingsService
{
    Task UpdateUserAbout(UserAboutPutDto userAboutPostDto);
    Task<UserAboutGetDto> GetUserAbout(int UserId);
    Task CreateUserWork(UserWorkPostDto userWorkPostDto);
    Task UpdateUserWork(UserWorkPutDto userWorkPostDto);
    Task<UserWorkGetDto>GetUserWork(int UserId);
    Task CreateUserEducation(UserEducationPostDto userEducationPostDto);
    Task UpdateUserEducation(UserEducationPutDto userEducationPutDto);
    Task<UserEducationGetDto>GetUserEducation(int UserId);
    Task CreateUserInterest(UserInterestPostDto userInterestPostDto);
    Task UpdateUserInterest(UserInterestPutDto userInterestPutDto);
    Task<List<UserInterestGetDto>> GetAllUserInterests(int UserId);
    Task ChangePassword(string UserId,ChangePasswordDto changePasswordDto);
}
    