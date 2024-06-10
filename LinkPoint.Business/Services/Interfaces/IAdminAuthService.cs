using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAdminAuthService
{
    Task<TokenDto> LoginAsync(LoginDto loginDto);
    Task<TokenDto> GenerateTokenAsync(AppUser user);
    string GenerateRefreshToken();
    Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
    Task LogOutAsync();
    Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}
