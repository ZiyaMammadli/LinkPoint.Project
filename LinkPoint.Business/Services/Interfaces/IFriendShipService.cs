using LinkPoint.Core.Entities;

namespace LinkPoint.Business.Services.Interfaces;

public interface IFriendShipService
{
    Task AddToFriendShip(string followingUserId);
    Task AcceptFriendShipRequest(int friendShipId);
    Task RejectFriendShipRequest(int FriednShipId);
    Task<List<AppUser>> GetAllAcceptedFollowingUsers();
    Task<List<AppUser>> GetAllAcceptedFollowerUsers();
    Task<List<AppUser>> GetAllPendingFollowerUsers();
}
