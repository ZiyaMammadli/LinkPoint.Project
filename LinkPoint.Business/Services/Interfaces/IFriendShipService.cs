using LinkPoint.Business.DTOs.FriendShipDTOs;
using LinkPoint.Core.Entities;

namespace LinkPoint.Business.Services.Interfaces;

public interface IFriendShipService
{
    Task AddToFriendShipAsync(string UserId,string followingUserId);//dostluq isteyi gondermek
    Task AcceptFriendShipRequestAsync(int friendShipId);//dostluq isteyin qebul etmek
    Task RejectFriendShipRequestAsync(int friednShipId);//dostluq isteyin redd etmek
    Task<List<AcceptedFollowingUserDto>> GetAllAcceptedFollowingUsersAsync(string UserId);//dostlugu qebul etmis butun izlediyin userleri gosterir
    Task<List<AcceptedFollowerUserDto>> GetAllAcceptedFollowerUsersAsync(string UserId);//dostlugu qebul etdiyin butun seni izleyen userleri gosterir
    Task<List<PendingFollowerUserDto>> GetAllPendingFollowerUsersAsync(string UserId);//dostlugu qebul olunmamis, gozlemede olan, seni izlemek isteyen userleri gosterir
}
