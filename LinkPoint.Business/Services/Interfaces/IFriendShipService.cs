using LinkPoint.Business.DTOs.FriendShipDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.Services.Interfaces;

public interface IFriendShipService
{
    Task AddToFriendShipAsync(string followingUserId);//
    Task AcceptFriendShipRequestAsync(int friendShipId);//
    Task RejectFriendShipRequestAsync(int friednShipId);//
    Task<List<AcceptedFollowingUserDto>> GetAllAcceptedFollowingUsersAsync();//
    Task<List<AcceptedFollowerUserDto>> GetAllAcceptedFollowerUsersAsync();//
    Task<List<PendingFollowerUserDto>> GetAllPendingFollowerUsersAsync();//
}
