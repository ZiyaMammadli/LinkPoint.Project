using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAccountSettingsService
{
    Task UpdateUserAbout(UserAboutPostDto userAboutPostDto);
    Task<UserAboutGetDto> GetUserAbout(int UserId);
    Task UpdateUserWork(UserWorkPostDto userWorkPostDto);
}
