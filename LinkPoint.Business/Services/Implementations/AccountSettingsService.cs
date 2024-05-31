using AutoMapper;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.BackgroundImageDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.ProfileImageDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserEducationDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserInterestDTOs;
using LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using LinkPoint.Business.Utilities.Extentions;
using LinkPoint.Business.Utilities.Helpers;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LinkPoint.Business.Services.Implementations;

public class AccountSettingsService : IAccountSettingsService
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

    public AccountSettingsService(IUserEducationRepository userEducationRepository,
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
    public async Task UpdateUserEducationAsync(int Id ,UserEducationPutDto userEducationPutDto)
    {   
        var userEducation=await _userEducationRepository.GetSingleAsync(ue=>ue.UserId== userEducationPutDto.UserId);
        if (userEducation is null) throw new UserNotFoundException(404,"UserId is not found");
        if (Id != userEducationPutDto.Id) throw new IdNotValidException(400, "Id is not valid");
        var userEdu=_mapper.Map(userEducationPutDto, userEducation);
        userEdu.UpdatedDate = DateTime.UtcNow;
        await _userEducationRepository.CommitAsync();
    }
    public async Task<UserEducationGetDto> GetUserEducationAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userEducation=await _userEducationRepository.GetSingleAsync(ue=>ue.UserId==user.Id);
        UserEducationGetDto userEducationGetDto1 = new UserEducationGetDto();
        userEducationGetDto1 = null;
        if (userEducation is not null)
        {
            var userEducationGetDto = _mapper.Map<UserEducationGetDto>(userEducation);
            userEducationGetDto.UserEducationId=userEducation.Id;
            return userEducationGetDto;
        }
        return userEducationGetDto1;
    }
    public async Task CreateUserInterestAsync(UserInterestPostDto userInterestPostDto)
    {
        var user = await _userManager.FindByIdAsync(userInterestPostDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userInterest = _mapper.Map<UserInterest>(userInterestPostDto);
        await _userInterestRepository.InsertAsync(userInterest);
        await _userInterestRepository.CommitAsync();
    }
    public async Task DeleteUserInterestAsync(int Id, UserInterestDeleteDto userInterestDeleteDto)
    {
        if (Id != userInterestDeleteDto.Id) throw new IdNotValidException(400, "Id is not Valid");
        var UserInterest=await _userInterestRepository.GetByIdAsync(Id);
        _userInterestRepository.Delete(UserInterest);
        await _userInterestRepository.CommitAsync();
    }
    public async Task<List<UserInterestGetDto>> GetAllUserInterestsAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        List<UserInterestGetDto> userInterestGetDtos = new List<UserInterestGetDto>();
        List<UserInterest> userInterests= await _userInterestRepository.GetAllAsync(ui=>ui.UserId==user.Id);
        if (userInterests.Count > 0)
        {
            foreach (var userInterest in userInterests)
            {
                var userInterestGetDto = _mapper.Map<UserInterestGetDto>(userInterest);
                userInterestGetDtos.Add(userInterestGetDto);
            }
        }       
        return userInterestGetDtos;
    }
    public async Task UpdateUserWorkAsync(int Id,UserWorkPutDto userWorkPutDto)
    {
        if (Id != userWorkPutDto.Id) throw new IdNotValidException(400, "Id is not valid");
        var userwork= await _userWorkRepository.GetSingleAsync(uw=>uw.UserId==userWorkPutDto.UserId);
        if(userwork is null)
        {
            var userworkk=_mapper.Map<UserWork>(userWorkPutDto);
            userworkk.CreatedDate = DateTime.UtcNow;
            userworkk.UpdatedDate = DateTime.UtcNow;
            await _userWorkRepository.InsertAsync(userworkk);
            await _userWorkRepository.CommitAsync();
        }
        else
        {
            var userWork = _mapper.Map(userWorkPutDto, userwork);
            userWork.UpdatedDate = DateTime.UtcNow;
            await _userWorkRepository.CommitAsync();
        }
    }
    public async Task<UserWorkGetDto> GetUserWorkAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userWork=await _userWorkRepository.GetSingleAsync(uw=>uw.UserId==user.Id);
        UserWorkGetDto userWorkGetDto = new UserWorkGetDto();
        userWorkGetDto = null;
        if (userWork is not null)
        {
            var userwork = _mapper.Map<UserWorkGetDto>(userWork);
            userwork.UserWorkId=userWork.Id;
            return userwork;
        }
        return userWorkGetDto;
    }
    public async Task<UserAboutGetDto> GetUserAboutAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userAbout= await _userAboutRepository.GetSingleAsync(ua=>ua.UserId==user.Id);
        if (userAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
        var userAboutGetDto=_mapper.Map<UserAboutGetDto>(userAbout);
        userAboutGetDto.UserAboutId=userAbout.Id;
        return userAboutGetDto;
    }
    public async Task UpdateUserAboutAsync(int Id,UserAboutPutDto userAboutPutDto)
    {
        if (Id != userAboutPutDto.Id) throw new IdNotValidException(400, "Id is not valid");
        var CurrentUserAbout=await _userAboutRepository.GetSingleAsync(ua=>ua.UserId== userAboutPutDto.UserId);
        if (CurrentUserAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
        var UserAbout = _mapper.Map(userAboutPutDto, CurrentUserAbout);
        UserAbout.UpdatedDate = DateTime.UtcNow;
        await _userAboutRepository.CommitAsync();
    }
    public async Task ChangePasswordAsync(string UserId,ChangePasswordDto changePasswordDto)
    {
        if (UserId != changePasswordDto.UserId) throw new IdNotValidException(400, "UserId is not valid");
        var user=await _userManager.FindByIdAsync(changePasswordDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "UserId is not found");
        var result=await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);
        if(result is false) throw new InvalidCredentialsException(401, "Incorrect password");
        await _userManager.ChangePasswordAsync(user,changePasswordDto.OldPassword,changePasswordDto.NewPassword);
    }

    public async Task CreateUserWorkAsync(UserWorkPostDto userWorkPostDto)
    {
        var user=await _userManager.FindByIdAsync(userWorkPostDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "UserId is not found");
        var userWork=_mapper.Map<UserWork>(userWorkPostDto);
        userWork.CreatedDate = DateTime.UtcNow;
        userWork.UpdatedDate = DateTime.UtcNow;
        await _userWorkRepository.InsertAsync(userWork);
        await _userWorkRepository.CommitAsync();
    }

    public async Task CreateUserEducationAsync(UserEducationPostDto userEducationPostDto)
    {
        var user = await _userManager.FindByIdAsync(userEducationPostDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "UserId is not found");
        var userEducation=_mapper.Map<UserEducation>(userEducationPostDto);
        userEducation.CreatedDate = DateTime.UtcNow;
        userEducation.UpdatedDate = DateTime.UtcNow;
        await _userEducationRepository.InsertAsync(userEducation);
        await _userEducationRepository.CommitAsync();   
    }

    public async Task UpdateUserProfileImageAsync(int ImageId, ProfileImagePutDto profileImagePostDto)
    {
       
        if (ImageId != profileImagePostDto.ImageId) throw new IdNotValidException(400, "ImageId is not Valid");
        var currentImage=await _imageRepository.GetByIdAsync(profileImagePostDto.ImageId);
        if (currentImage is null) throw new ImageNotFoundException(404, "Image is not found");
        string apiKey = _configuration["GoogleCloud:ApiKey"];
        //int startIndex = currentImage.ImageUrl.Length-55;
        //int lentgh = 55;
        string fileName=currentImage.ImageUrl.Substring(49);
        await FileManager.DeleteFile(fileName, apiKey, "Images");
        currentImage.ImageUrl = profileImagePostDto.ProfileImage.SaveFile(apiKey, "Images");
        currentImage.UpdatedDate = DateTime.UtcNow;
        await _imageRepository.CommitAsync();
    }

    public async Task DeleteUserProfileImageAsync(int ImageId, ProfileImageDeleteDto profileImageDeleteDto)
    {
        if (ImageId != profileImageDeleteDto.ImageId) throw new IdNotValidException(400, "ImageId is not Valid");
        var currentImage = await _imageRepository.GetByIdAsync(profileImageDeleteDto.ImageId);
        if (currentImage is null) throw new ImageNotFoundException(404, "Image is not found");
        string apiKey = _configuration["GoogleCloud:ApiKey"];
        string fileName = currentImage.ImageUrl.Substring(49);
        await FileManager.DeleteFile(fileName, apiKey, "Images");        
        var user = await _userManager.FindByIdAsync(profileImageDeleteDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        string DefaultImageName = "DefaultPerson.jpg";
        string DefautlImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "UserDefaultProfileImage", DefaultImageName);
        var DefaultProfileImage = FileManager.CreateIFormFile(DefautlImagePath, DefaultImageName);
        Image ProfileImage = new()
        {
            UserId = user.Id,
            PostId = null,
            IsPostImage = false,
            ImageUrl = DefaultProfileImage.SaveFile(apiKey, "Images"),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        _imageRepository.Delete(currentImage);
        await _imageRepository.InsertAsync(ProfileImage);
        await _imageRepository.CommitAsync();
    }

    public async Task UpdateUserBacgroundImageAsync(int ImageId, BackgroundImagePutDto backgroundImagePutDto)
    {
        if (ImageId != backgroundImagePutDto.ImageId) throw new IdNotValidException(400, "ImageId is not Valid");
        var currentImage = await _imageRepository.GetByIdAsync(backgroundImagePutDto.ImageId);
        if (currentImage is null) throw new ImageNotFoundException(404, "Image is not found");
        string apiKey = _configuration["GoogleCloud:ApiKey"];
        string fileName = currentImage.ImageUrl.Substring(49);
        await FileManager.DeleteFile(fileName, apiKey, "Images");
        currentImage.ImageUrl = backgroundImagePutDto.BackgroundImage.SaveFile(apiKey, "Images");
        currentImage.UpdatedDate = DateTime.UtcNow;
        await _imageRepository.CommitAsync();
    }

    public async Task DeleteUserBackgroundImageAsync(int ImageId, BackgroundImageDeleteDto backgroundImageDeleteDto)
    {
        if (ImageId != backgroundImageDeleteDto.ImageId) throw new IdNotValidException(400, "ImageId is not Valid");
        var currentImage = await _imageRepository.GetByIdAsync(backgroundImageDeleteDto.ImageId);
        if (currentImage is null) throw new ImageNotFoundException(404, "Image is not found");
        string apiKey = _configuration["GoogleCloud:ApiKey"];
        string fileName = currentImage.ImageUrl.Substring(49);
        await FileManager.DeleteFile(fileName, apiKey, "Images");
        var user = await _userManager.FindByIdAsync(backgroundImageDeleteDto.UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        string DefaultImageName = "DefaultBackgraoundImage.jpg";
        string DefautlImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "UserDefaultProfileImage", DefaultImageName);
        var DefaultBackgroundImage = FileManager.CreateIFormFile(DefautlImagePath, DefaultImageName);
        Image BackgroundImage = new()
        {
            UserId = user.Id,
            PostId = null,
            IsPostImage = null,
            ImageUrl = DefaultBackgroundImage.SaveFile(apiKey, "Images"),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        _imageRepository.Delete(currentImage);
        await _imageRepository.InsertAsync(BackgroundImage);
        await _imageRepository.CommitAsync();
    }

    public async Task<AuthUserGetDto> GetAuthUserInfoAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
        if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        var backgroundImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == null);
        if (backgroundImage is null) throw new ProfileImageNotFoundException(404, "BackgroundImage is not found");
        var followings=await _friendShipService.GetAllAcceptedFollowingUsersAsync(UserId);
        var followers=await _friendShipService.GetAllAcceptedFollowerUsersAsync(UserId);
        AuthUserGetDto authUserGetDto = new AuthUserGetDto()
        {
            UserId = user.Id,
            UserName = user.UserName,
            ProfileImage= profileImage.ImageUrl,
            BackgroundImage= backgroundImage.ImageUrl,
            FollowersCount= followers.Count,
            FollowingsCount= followings.Count,
            ProfileImageId=profileImage.Id,
            BackgroundImageId=backgroundImage.Id,
        };
        return authUserGetDto;
    }

    public async Task<List<UserGetDto>> GetAllUsersAsync(string query)
    {
        var users=await _appUserRepository.GetAllAsync(u=>u.EmailConfirmed==true);
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

    public async Task<List<UserGetDto>> GetAllDontFollowingUsersAsync(string UserId,int count)
    {
        var followingUsers = await _friendShipService.GetAllAcceptedFollowingUsersAsync(UserId);
        List<UserGetDto> userGetDtos = new List<UserGetDto>();
        if (followingUsers.Count>0)
        {
            foreach (var user in followingUsers)
            {
                UserGetDto userGetDto = new UserGetDto()
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    ProfileImage = user.ProfileImageUrl
                };
                userGetDtos.Add(userGetDto);
            }
        }
        var users = await _appUserRepository.GetAllAsync(u => u.EmailConfirmed == true && u.Id !=UserId);
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
        var notFollowedUsers = usersDto.Except(userGetDtos, new UserGetDtoComparer()).ToList();
        var random = new Random();
        var randomUsers=notFollowedUsers.OrderBy(u => random.Next()).Take(count).ToList();
        return randomUsers;
    }
}
