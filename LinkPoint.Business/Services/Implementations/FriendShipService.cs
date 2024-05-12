using AutoMapper;
using Azure.Core;
using LinkPoint.Business.DTOs.FriendShipDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Enums;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace LinkPoint.Business.Services.Implementations;

public class FriendShipService : IFriendShipService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFriendShipRepository _friendShipRepository;
    private readonly IMapper _mapper;
    private readonly IImageRepository _imageRepository;

    public FriendShipService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor,IFriendShipRepository friendShipRepository,IMapper mapper,IImageRepository imageRepository)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _friendShipRepository = friendShipRepository;
        _mapper = mapper;
        _imageRepository = imageRepository;
    }
    public async Task AddToFriendShipAsync(string followingUserId)
    {
        var followingUser= await _userManager.FindByIdAsync(followingUserId);
        if (followingUser is null) throw new UserNotFoundException(404, "User is not found");
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        FriendShip friendShip = new FriendShip()
        {
            UserId = user.Id,
            FollowingUser = followingUser,
            Status = FollowStatus.Pending,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _friendShipRepository.InsertAsync(friendShip);
        await _friendShipRepository.CommitAsync();
    }

    public async Task AcceptFriendShipRequestAsync(int friendShipId)
    {
        var friendShip=await _friendShipRepository.GetByIdAsync(friendShipId);
        if (friendShip is null) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        friendShip.Status = FollowStatus.Accepted;
        friendShip.UpdatedDate = DateTime.UtcNow;
        await _friendShipRepository.CommitAsync();
    }
    public async Task RejectFriendShipRequestAsync(int friendShipId)
    {
        var friendShip = await _friendShipRepository.GetByIdAsync(friendShipId);
        if (friendShip is null) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        friendShip.Status = FollowStatus.Rejected;
        friendShip.UpdatedDate = DateTime.UtcNow;
        await _friendShipRepository.CommitAsync();
        
    }

    public async Task<List<AcceptedFollowingUserDto>> GetAllAcceptedFollowingUsersAsync()
    {
        
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");                   
        if (!await _friendShipRepository.IsExist(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Accepted)) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Accepted);
        List<AcceptedFollowingUserDto> followingUserDtos = new List<AcceptedFollowingUserDto>();
        foreach (var friendShip in FriendShips)
        {
            var followingUser = await _userManager.FindByIdAsync(friendShip.FollowingUserId);
            //var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == followingUser.Id&&i.IsPostImage==false);
            //if(profileImage is null) 
            //{
                
            //}
            AcceptedFollowingUserDto followingUserDto = new AcceptedFollowingUserDto()
            {
                UserId = followingUser.Id,
                UserName = followingUser.UserName,
                //ProfileImageUrl = profileImage.ImageUrl
            };
            followingUserDtos.Add(followingUserDto);
        }
        
        return followingUserDtos;
    }

    public async Task<List<AcceptedFollowerUserDto>> GetAllAcceptedFollowerUsersAsync()
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        if (!await _friendShipRepository.IsExist(fs => fs.FollowingUserId == user.Id && fs.Status == FollowStatus.Accepted)) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.FollowingUserId == user.Id && fs.Status == FollowStatus.Accepted);
        List<AcceptedFollowerUserDto> followerUserDtos = new List<AcceptedFollowerUserDto>();
        foreach (var friendShip in FriendShips)
        {
            var followerUser = await _userManager.FindByIdAsync(friendShip.UserId);
            AcceptedFollowerUserDto followerUserDto = new AcceptedFollowerUserDto()
            {
                UserId = followerUser.Id,
                UserName = followerUser.UserName,
                //ProfileImageUrl = profileImage.ImageUrl
            };
            followerUserDtos.Add(followerUserDto);
        }
        return followerUserDtos;
    }

    public async Task<List<PendingFollowerUserDto>> GetAllPendingFollowerUsersAsync()
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        if (!await _friendShipRepository.IsExist(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Pending)) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Pending);
        List<PendingFollowerUserDto> PendingFollowerUserDtos = new List<PendingFollowerUserDto>();
        foreach (var friendShip in FriendShips)
        {
            var PendingFollowerUser = await _userManager.FindByIdAsync(friendShip.FollowingUserId);
            PendingFollowerUserDto pendingFollowerUserDto = new PendingFollowerUserDto()
            {
                UserId = PendingFollowerUser.Id,
                UserName = PendingFollowerUser.UserName,
                //ProfileImageUrl = profileImage.ImageUrl
            };
            PendingFollowerUserDtos.Add(pendingFollowerUserDto);
        }
        return PendingFollowerUserDtos;
    }
}
