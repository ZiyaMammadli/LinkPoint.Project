using LinkPoint.Business.DTOs.AccountSettingsDTOs.BackgroundImageDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.ProfileImageDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;
using System.Globalization;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAccountSettingsService
{
    Task<List<UserGetDto>> GetAllDontFollowingUsersAsync(string UserId,int count);// Userin izlemediyi butun userleri gosterir
    Task<List<UserGetDto>> GetAllUsersAsync();//
    Task<AuthUserGetDto> GetAuthUserInfoAsync(string UserId);//
    Task UpdateUserAboutAsync(int Id,UserAboutPutDto userAboutPutDto);//
    Task<UserAboutGetDto> GetUserAboutAsync(string UserId);//
    Task CreateUserWorkAsync(UserWorkPostDto userWorkPostDto);//
    Task UpdateUserWorkAsync(int Id,UserWorkPutDto userWorkPutDto);//
    Task<UserWorkGetDto>GetUserWorkAsync(string UserId);//
    Task CreateUserEducationAsync(UserEducationPostDto userEducationPostDto);//
    Task UpdateUserEducationAsync(int Id,UserEducationPutDto userEducationPutDto);//
    Task<UserEducationGetDto>GetUserEducationAsync(string UserId);//
    Task CreateUserInterestAsync(UserInterestPostDto userInterestPostDto);//
    Task DeleteUserInterestAsync(int Id,UserInterestDeleteDto userInterestDeleteDto);//
    Task<List<UserInterestGetDto>> GetAllUserInterestsAsync(string UserId);//
    Task ChangePasswordAsync(string UserId,ChangePasswordDto changePasswordDto);//
    Task UpdateUserProfileImageAsync(int ImageId,ProfileImagePutDto profileImagePostDto);//
    Task DeleteUserProfileImageAsync(int ImageId, ProfileImageDeleteDto profileImageDeleteDto);//
    Task UpdateUserBacgroundImageAsync(int ImageId,BackgroundImagePutDto backgroundImagePutDto);//
    Task DeleteUserBackgroundImageAsync(int ImageId,BackgroundImageDeleteDto backgroundImageDeleteDto);//
}
    