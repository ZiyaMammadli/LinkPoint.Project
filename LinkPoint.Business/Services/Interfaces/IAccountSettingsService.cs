using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAccountSettingsService
{
    Task UpdateUserAbout(UserAboutPostDto userAboutPostDto);
    Task GetUserAbout(UserAboutGetDto userAboutGetDto);
}
