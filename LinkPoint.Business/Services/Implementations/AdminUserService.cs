using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LinkPoint.Business.Services.Implementations;

public class AdminUserService:IAdminUserService
{
    private readonly IUserEducationRepository _userEducationRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserInterestRepository _userInterestRepository;
    private readonly IUserWorkRepository _userWorkRepository;
    private readonly IUserAboutRepository _userAboutRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageRepository _imageRepository;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFriendShipService _friendShipService;
    private readonly IAppUserRepository _appUserRepository;

    public AdminUserService(IUserEducationRepository userEducationRepository,
        UserManager<AppUser> userManager,
        IMapper mapper,
        IUserInterestRepository userInterestRepository,
        IUserWorkRepository userWorkRepository,
        IUserAboutRepository userAboutRepository,
        IHttpContextAccessor httpContextAccessor,
        IImageRepository imageRepository,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment,
        IFriendShipService friendShipService,
        IAppUserRepository appUserRepository)
    {
        _userEducationRepository = userEducationRepository;
        _userManager = userManager;
        _mapper = mapper;
        _userInterestRepository = userInterestRepository;
        _userWorkRepository = userWorkRepository;
        _userAboutRepository = userAboutRepository;
        _httpContextAccessor = httpContextAccessor;
        _imageRepository = imageRepository;
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
        _friendShipService = friendShipService;
        _appUserRepository = appUserRepository;
    }
    public async Task<PaginatedUsersDto> GetAllUsersWithPagesAsync(int pageNumber, int pageSize)
    {
        var Users = await _appUserRepository.GetAllAsync(u => u.EmailConfirmed == true);
        var users = Users.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        //var users = await _userManager.Users
        //    .Skip((pageNumber - 1) * pageSize)
        //    .Take(pageSize)
        //    .ToListAsync();

        var totalUsers = await _userManager.Users.CountAsync();

        List<UserGetDto> userGetDtos = new List<UserGetDto>();
        if (users.Count > 0)
        {
            foreach (var user in users)
            {
                var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
                if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                UserGetDto userGetDto = new UserGetDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    ProfileImage = profileImage.ImageUrl
                };
                userGetDtos.Add(userGetDto);
            }
        }
        return new PaginatedUsersDto
        {
            Users = userGetDtos,
            TotalUsers = totalUsers,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize)
        };
    }
    public async Task<List<UserGetDto>> GetAllUsersAsync(string query)
    {
        var users = await _appUserRepository.GetAllAsync(u => u.EmailConfirmed == true);
        List<UserGetDto> usersDto = new List<UserGetDto>();
        if (users.Count > 0)
        {
            foreach (var user in users)
            {
                var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
                if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                UserGetDto userGetDto = new UserGetDto()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    ProfileImage = profileImage.ImageUrl
                };
                usersDto.Add(userGetDto);
            }
        }
        var userss = usersDto.Where(u => u.UserName.Contains(query)).ToList();
        return userss;
    }
    public async Task<AuthUserGetDto> GetUserByIdAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
        if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        var backgroundImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == null);
        if (backgroundImage is null) throw new ProfileImageNotFoundException(404, "BackgroundImage is not found");
        var followings = await _friendShipService.GetAllAcceptedFollowingUsersAsync(UserId);
        var followers = await _friendShipService.GetAllAcceptedFollowerUsersAsync(UserId);
        AuthUserGetDto authUserGetDto = new AuthUserGetDto()
        {
            UserId = user.Id,
            UserName = user.UserName,
            ProfileImage = profileImage.ImageUrl,
            BackgroundImage = backgroundImage.ImageUrl,
            FollowersCount = followers.Count,
            FollowingsCount = followings.Count,
            ProfileImageId = profileImage.Id,
            BackgroundImageId = backgroundImage.Id,
        };
        return authUserGetDto;
    }
    public async Task<UserAboutGetDto> GetUserAboutAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userAbout = await _userAboutRepository.GetSingleAsync(ua => ua.UserId == user.Id);
        if (userAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
        var userAboutGetDto = _mapper.Map<UserAboutGetDto>(userAbout);
        userAboutGetDto.UserAboutId = userAbout.Id;
        return userAboutGetDto;
    }
    public async Task<UserWorkGetDto> GetUserWorkAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userWork = await _userWorkRepository.GetSingleAsync(uw => uw.UserId == user.Id);
        UserWorkGetDto userWorkGetDto = new UserWorkGetDto();
        userWorkGetDto = null;
        if (userWork is not null)
        {
            var userwork = _mapper.Map<UserWorkGetDto>(userWork);
            userwork.UserWorkId = userWork.Id;
            return userwork;
        }
        return userWorkGetDto;
    }
    public async Task<UserEducationGetDto> GetUserEducationAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userEducation = await _userEducationRepository.GetSingleAsync(ue => ue.UserId == user.Id);
        UserEducationGetDto userEducationGetDto1 = new UserEducationGetDto();
        userEducationGetDto1 = null;
        if (userEducation is not null)
        {
            var userEducationGetDto = _mapper.Map<UserEducationGetDto>(userEducation);
            userEducationGetDto.UserEducationId = userEducation.Id;
            return userEducationGetDto;
        }
        return userEducationGetDto1;
    }
    public async Task<List<UserInterestGetDto>> GetAllUserInterestsAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        List<UserInterestGetDto> userInterestGetDtos = new List<UserInterestGetDto>();
        List<UserInterest> userInterests = await _userInterestRepository.GetAllAsync(ui => ui.UserId == user.Id);
        if (userInterests.Count > 0)
        {
            foreach (var userInterest in userInterests)
            {
                var userInterestGetDto = _mapper.Map<UserInterestGetDto>(userInterest);
                userInterestGetDto.InterestId = userInterest.Id;
                userInterestGetDtos.Add(userInterestGetDto);
            }
        }
        return userInterestGetDtos;
    }
}
