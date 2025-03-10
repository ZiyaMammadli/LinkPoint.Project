﻿using Google.Apis.Auth.OAuth2.Responses;
using LinkPoint.Business.DTOs.AccountDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.ConfirmedExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundException;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Extentions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUrlHelper _urlHelper;
    private readonly IImageRepository _imageRepository;
    private readonly IConfiguration _configuration;

    public AccountService(UserManager<AppUser> userManager, 
        LinkPointDbContext context, 
        SignInManager<AppUser> signInManager, 
        IConfiguration conf,
        IEmailService emailService,
        IHttpContextAccessor httpContextAccessor,
        IUrlHelper urlHelper,
        IImageRepository imageRepository,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _conf = conf;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _urlHelper = urlHelper;
        _imageRepository = imageRepository;
        _configuration = configuration;
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

    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user is null) throw new InvalidCredentialsException(401,"Incorrect password or username");
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result is false) throw new InvalidCredentialsException(401,"Incorrect password or username");
        if(user.EmailConfirmed==true)
        {
           
            var result1 = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result1.Succeeded) throw new InvalidCredentialsException(401,"incorrect password or username");
        }
        else
        {
            throw new EmailConfirmedException(401,"Email must be confirmed");
        }
        var tokens = await GenerateTokenAsync(user);
        tokens.UserId = user.Id;
        return tokens;
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
        string apiKey = _configuration["GoogleCloud:ApiKey"];
        string DefautlImagePath = "C:\\Users\\user\\source\\repos\\LinkPoint\\src\\LinkPoint.API\\wwwroot\\UserDefaultProfileImage\\DefaultPerson.jpg";
        string DefautlBackgroundImagePath = "C:\\Users\\user\\source\\repos\\LinkPoint\\src\\LinkPoint.API\\wwwroot\\UserDefaultProfileImage\\DefaultBackgraoundImage.jpg";
        string DefaultImageName = "DefaultPerson.jpg";
        string DefaultBackgroundImageName = "DefaultBackgraoundImage.jpg";
        var DefaultProfileImage=FileManager.CreateIFormFile(DefautlImagePath, DefaultImageName);
        var DefaultBackgroundImage=FileManager.CreateIFormFile(DefautlBackgroundImagePath, DefaultBackgroundImageName);
        Image ProfileImage = new()
        {
            UserId = appUser.Id,
            PostId = null,
            IsPostImage = false,
            ImageUrl = DefaultProfileImage.SaveFile(apiKey,"Images"),
            CreatedDate= DateTime.UtcNow,
            UpdatedDate= DateTime.UtcNow,
        }; 
        Image BackgroundImage = new()
        {
            UserId = appUser.Id,
            PostId = null,
            IsPostImage = null,
            ImageUrl = DefaultBackgroundImage.SaveFile(apiKey,"Images"),
            CreatedDate=DateTime.UtcNow,
            UpdatedDate= DateTime.UtcNow,
        };
        await _imageRepository.InsertAsync(ProfileImage);
        await _imageRepository.InsertAsync(BackgroundImage);
        _context.UserAbouts.Add(userAbout);
        await _context.SaveChangesAsync();
        string code=await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        var encodedCode = WebUtility.UrlEncode(code);
        string subject = "LinkPoint Register Succesfully";
        string html =string.Empty;
        string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "email", "Register.html");
        html = System.IO.File.ReadAllText(FilePath);
        //var Url = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + _urlHelper.Action("EmailConfirm", "Account", new { UserId = appUser.Id, code });
        var url= $"{registerDto.callbackUrl}?UserId={appUser.Id}&code={encodedCode}";
        html =html.Replace("{{Url}}",System.Text.Encodings.Web.HtmlEncoder.Default.Encode(url));
        _emailService.SendEmail(appUser.Email, subject, html);
    }

    public async Task EmailConfirmAsync(string UserId, string code)
    {
        if(UserId==null || code == null)
        {
            throw new ValueNullException("Value can't be null");
        }
        var user=await _userManager.FindByIdAsync(UserId);
        if(user == null)
        {
            throw new UserNotFoundException(404, "User is not found");
        }

        //code=Encoding.UTF8.GetString(Convert.FromBase64String(code));
        var result=await _userManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                throw new Exception(error.Description);
            }
        }
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user=await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if(user is null /*|| !(await _userManager.IsEmailConfirmedAsync(user))*/) throw new EmailNotFoundException(404,"Email is not found");
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
