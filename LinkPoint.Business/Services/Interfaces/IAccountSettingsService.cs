using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAccountSettingsService
{
    Task UpdateUserAbout(UserAboutPutDto userAboutPostDto);
    Task<UserAboutGetDto> GetUserAbout(string UserId);
    Task CreateUserWork(UserWorkPostDto userWorkPostDto);
    Task UpdateUserWork(UserWorkPutDto userWorkPostDto);
    Task<UserWorkGetDto>GetUserWork(string UserId);
    Task UpdateUserEducation(int Id,UserEducationPutDto userEducationPutDto);
    Task<UserEducationGetDto>GetUserEducation(string UserId);
    Task CreateUserInterest(UserInterestPostDto userInterestPostDto);
    Task DeleteUserInterest(int Id,UserInterestDeleteDto userInterestDeleteDto);
    Task<List<UserInterestGetDto>> GetAllUserInterests(string UserId);
    Task ChangePassword(ChangePasswordDto changePasswordDto);
}
    