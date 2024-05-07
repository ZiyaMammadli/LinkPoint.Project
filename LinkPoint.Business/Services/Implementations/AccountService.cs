﻿using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Core.Entities;
using LinkPoint.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LinkPoint.Business.Services.Implementations;

public class AccountService:IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly LinkPointDbContext _context;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _conf;

    public AccountService(UserManager<AppUser> userManager, LinkPointDbContext context, SignInManager<AppUser> signInManager, IConfiguration conf)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _conf = conf;
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

    public async Task<TokenDto> GenerateTokenAsync(AppUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JWT:securityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> Claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
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

    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user is null) throw new InvalidCredentialsException(401,"Incorrect password or username");
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result is false) throw new InvalidCredentialsException(401,"Incorrect password or username");
        var result1 = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (!result1.Succeeded) throw new InvalidCredentialsException(401,"incorrect password or username");
        return await GenerateTokenAsync(user);

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
            throw new RefreshTokenNotFoundException(404,"RefreshToken is not found");
        }
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == registerDto.Email);
        if (user is not null) throw new AlreadyExistException(403,"Email already exist");
        var user1 = _context.Users.FirstOrDefault(u => u.UserName == registerDto.UserName);
        if (user1 is not null) throw new AlreadyExistException(403,"Username already exist");
        AppUser appUser = new()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            RefreshToken="token",
            CreatedDate = DateTime.UtcNow,

        };
        var result = await _userManager.CreateAsync(appUser, registerDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                throw new Exception(error.Description);
            }
        }
        var result1 = await _userManager.AddToRoleAsync(appUser, "Member");
        if (!result1.Succeeded)
        {
            foreach (var error in result1.Errors)
            {
                throw new Exception(error.Description);
            }
        }
        UserAbout userAbout = new()
        {
            UserId=appUser.Id,
            Male = registerDto.Male,
            Female = registerDto.Female,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        _context.UserAbouts.Add(userAbout);
        await _context.SaveChangesAsync();
    }
}