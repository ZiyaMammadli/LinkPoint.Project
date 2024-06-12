using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAdminUserService
{
    Task<PaginatedUsersDto> GetAllUsersWithPagesAsync(int pageNumber, int pageSize);
    Task<AuthUserGetDto> GetUserByIdAsync(string UserId);//
    Task<List<UserGetDto>> GetAllUsersAsync(string query);//
    Task<UserAboutGetDto> GetUserAboutAsync(string UserId);//
    Task<UserWorkGetDto> GetUserWorkAsync(string UserId);//
    Task<UserEducationGetDto> GetUserEducationAsync(string UserId);//
    Task<List<UserInterestGetDto>> GetAllUserInterestsAsync(string UserId);//
}
