using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Enums;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LinkPoint.Business.Services.Implementations;

public class FriendShipService : IFriendShipService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFriendShipRepository _friendShipRepository;

    public FriendShipService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor,IFriendShipRepository friendShipRepository)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _friendShipRepository = friendShipRepository;
    }
    public async Task AddToFriendShip(string followingUserId)
    {
        var followingUser= await _userManager.FindByIdAsync(followingUserId);
        if (followingUser is null) throw new UserNotFoundException(404, "User is not found");
        var user =await  _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
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

    public async Task AcceptFriendShipRequest(int friendShipId)
    {
        var friendShip=await _friendShipRepository.GetByIdAsync(friendShipId);
        if (friendShip is null) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        friendShip.Status = FollowStatus.Accepted;
        friendShip.UpdatedDate = DateTime.UtcNow;
        await _friendShipRepository.CommitAsync();
    }
    public async Task RejectFriendShipRequest(int friendShipId)
    {
        var friendShip = await _friendShipRepository.GetByIdAsync(friendShipId);
        if (friendShip is null) throw new FriendShipNotFoundException(404, "FriendShip is not found");
        friendShip.Status = FollowStatus.Rejected;
        friendShip.UpdatedDate = DateTime.UtcNow;
        await _friendShipRepository.CommitAsync();
        
    }

    public async Task<List<AppUser>> GetAllAcceptedFollowingUsers()
    {
        var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.UserId == user.Id && fs.Status == FollowStatus.Accepted);
        List<AppUser> followingUsers = new List<AppUser>();
        foreach (var friendShip in FriendShips)
        {
            var followingUser = await _userManager.FindByIdAsync(friendShip.FollowingUserId);
            followingUsers.Add(followingUser);
        }
        return followingUsers;
    }

    public async Task<List<AppUser>> GetAllAcceptedFollowerUsers()
    {
        var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.FollowingUserId == user.Id && fs.Status == FollowStatus.Accepted);
        List<AppUser> followerUsers = new List<AppUser>();
        foreach (var friendShip in FriendShips)
        {
            var followerUser = await _userManager.FindByIdAsync(friendShip.FollowingUserId);
            followerUsers.Add(followerUser);
        }
        return followerUsers;
    }

    public async Task<List<AppUser>> GetAllPendingFollowerUsers()
    {
        var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
        var FriendShips = await _friendShipRepository.GetAllAsync(fs => fs.FollowingUserId == user.Id && fs.Status == FollowStatus.Pending);
        List<AppUser> PendingFollowerUsers = new List<AppUser>();
        foreach (var friendShip in FriendShips)
        {
            var PendingFollowerUser = await _userManager.FindByIdAsync(friendShip.FollowingUserId);
            PendingFollowerUsers.Add(PendingFollowerUser);
        }
        return PendingFollowerUsers;
    }
}
