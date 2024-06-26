﻿using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAccountService
{
    Task<TokenDto> LoginAsync(LoginDto loginDto);
    Task RegisterAsync(RegisterDto registerDto);
    Task<TokenDto> GenerateTokenAsync(AppUser user);
    string GenerateRefreshToken();
    Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
    Task EmailConfirmAsync(string UserId, string code);
    Task LogOutAsync();
    Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}
