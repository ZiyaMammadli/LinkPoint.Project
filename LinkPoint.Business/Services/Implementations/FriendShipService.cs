﻿using AutoMapper;
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
    public async Task AddToFriendShipAsync(string UserId,string followingUserId)
    {
        var followingUser= await _userManager.FindByIdAsync(followingUserId);
        if (followingUser is null) throw new UserNotFoundException(404, "User is not found");
        var user = await _userManager.FindByIdAsync(UserId);
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

    public async Task<List<AcceptedFollowingUserDto>> GetAllAcceptedFollowingUsersAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");                   
        //if (!await _friendShipRepository.IsExist(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Accepted)) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Accepted);
        List<AcceptedFollowingUserDto> followingUserDtos = new List<AcceptedFollowingUserDto>();
        if (FriendShips.Count > 0)
        {
            foreach (var friendShip in FriendShips)
            {
                var followingUser = await _userManager.FindByIdAsync(friendShip.FollowingUserId);
                var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == followingUser.Id && i.IsPostImage == false);
                if (profileImage is null)
                {
                    throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                }

                AcceptedFollowingUserDto followingUserDto = new AcceptedFollowingUserDto()
                {
                    UserId = followingUser.Id,
                    UserName = followingUser.UserName,
                    ProfileImageUrl = profileImage.ImageUrl
                };
                followingUserDtos.Add(followingUserDto);
            }
        }    
        return followingUserDtos;
    }

    public async Task<List<AcceptedFollowerUserDto>> GetAllAcceptedFollowerUsersAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        //if (!await _friendShipRepository.IsExist(fs => fs.FollowingUserId == user.Id && fs.Status == FollowStatus.Accepted)) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.FollowingUserId == user.Id && fs.Status == FollowStatus.Accepted);
        List<AcceptedFollowerUserDto> followerUserDtos = new List<AcceptedFollowerUserDto>();
        if(FriendShips.Count > 0)
        {
            foreach (var friendShip in FriendShips)
            {
                var followerUser = await _userManager.FindByIdAsync(friendShip.UserId);
                var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == followerUser.Id && i.IsPostImage == false);
                if (profileImage is null)
                {
                    throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                }
                AcceptedFollowerUserDto followerUserDto = new AcceptedFollowerUserDto()
                {
                    UserId = followerUser.Id,
                    UserName = followerUser.UserName,
                    ProfileImageUrl = profileImage.ImageUrl
                };
                followerUserDtos.Add(followerUserDto);
            }
        }        
        return followerUserDtos;
    }

    public async Task<List<PendingFollowerUserDto>> GetAllPendingFollowerUsersAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        //if (!await _friendShipRepository.IsExist(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Pending)) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Pending);
        List<PendingFollowerUserDto> PendingFollowerUserDtos = new List<PendingFollowerUserDto>();
        if(FriendShips.Count > 0)
        {
            foreach (var friendShip in FriendShips)
            {
                var PendingFollowerUser = await _userManager.FindByIdAsync(friendShip.FollowingUserId);
                var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == PendingFollowerUser.Id && i.IsPostImage == false);
                if (profileImage is null)
                {
                    throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                }
                PendingFollowerUserDto pendingFollowerUserDto = new PendingFollowerUserDto()
                {
                    FriendShipId=friendShip.Id,
                    UserId = PendingFollowerUser.Id,
                    UserName = PendingFollowerUser.UserName,
                    ProfileImageUrl = profileImage.ImageUrl
                };
                PendingFollowerUserDtos.Add(pendingFollowerUserDto);
            }
        }        
        return PendingFollowerUserDtos;
    }
}
