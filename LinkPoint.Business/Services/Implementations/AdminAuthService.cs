using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LinkPoint.Business.Services.Implementations;

public class AdminAuthService : IAdminAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly LinkPointDbContext _context;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _conf;
    private readonly IEmailService _emailService;

    public AdminAuthService(UserManager<AppUser> userManager,
        LinkPointDbContext context,
        SignInManager<AppUser> signInManager,
        IConfiguration conf,
        IEmailService emailService)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _conf = conf;
        _emailService = emailService;
    }
    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user is null) throw new InvalidCredentialsException(401, "Incorrect password or username");
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result is false) throw new InvalidCredentialsException(401, "Incorrect password or username");
        var result1 = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (!result1.Succeeded) throw new InvalidCredentialsException(401, "incorrect password or username");
        var tokens = await GenerateTokenAsync(user);
        tokens.UserId = user.Id;
        return tokens;
    }
    public async Task<TokenDto> GenerateTokenAsync(AppUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JWT:securityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> Claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var roles = await _userManager.GetRolesAsync(user);
        Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var token = new JwtSecurityToken(
            _conf["JWT:issuer"],
            _conf["JWT:audience"],
            claims: Claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: credentials);

        var AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var RefreshToken = GenerateRefreshToken();

        TokenDto tokenDto = new TokenDto()
        {
            AccesToken = AccessToken,
            RefreshToken = RefreshToken,
            Expiration = DateTime.UtcNow.AddHours(5)
        };

        user.RefreshToken = tokenDto.RefreshToken;
        user.RefreshTokenEndDate = tokenDto.Expiration;
        await _context.SaveChangesAsync();
        return tokenDto;
    }
    public string GenerateRefreshToken()
    {
        byte[] number = new byte[32];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(number);

            return Convert.ToBase64String(number);
        }
    }
    public async Task<TokenDto> RefreshTokenLoginAsync(string refreshToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user is not null && user.RefreshTokenEndDate > DateTime.UtcNow)
        {
            return await GenerateTokenAsync(user);
        }
        else
        {
            throw new RefreshTokenNotFoundException(404, "RefreshToken is not found");
        }
    }
    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user is null /*|| !(await _userManager.IsEmailConfirmedAsync(user))*/) throw new EmailNotFoundException(404, "Email is not found");
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebUtility.UrlEncode(token);
        var link = $"{forgotPasswordDto.callbackUrl}?token={encodedToken}&email={forgotPasswordDto.Email}";
        _emailService.SendEmail(forgotPasswordDto.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{link}'>link</a>");
    }

    public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user is null /*|| !(await _userManager.IsEmailConfirmedAsync(user))*/) throw new EmailNotFoundException(404, "Email is not found");

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                throw new Exception(err.Description);
            }
        }
    }
}
