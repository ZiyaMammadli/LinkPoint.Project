using LinkPoint.Core.Entities;

namespace LinkPoint.Business.Services.Interfaces;

public interface IFriendShipService
{
    Task AddToFriendShipAsync(string followingUserId);//
    Task AcceptFriendShipRequestAsync(int friendShipId);//
    Task RejectFriendShipRequestAsync(int friednShipId);//
    Task<List<AppUser>> GetAllAcceptedFollowingUsersAsync();//
    Task<List<AppUser>> GetAllAcceptedFollowerUsersAsync();//
    Task<List<AppUser>> GetAllPendingFollowerUsersAsync();//
}
