using LinkPoint.Business.DTOs.FriendShipDTOs;
using LinkPoint.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.Business.Services.Interfaces;

public interface IFriendShipService
{
    Task CancelFriendShipAsync(string userId, string followingUserId);// takip isteyin legv etmek ucun
    Task<bool> CheckFriendShipStatusAsync(string userId, string followingUserId);//dostlugun statusun yoclayir buttonun veziyyetini bilmek ucun
    Task AddToFriendShipAsync(string UserId,string followingUserId);//dostluq isteyi gondermek
    Task AcceptFriendShipRequestAsync(int friendShipId);//dostluq isteyin qebul etmek
    Task RejectFriendShipRequestAsync(int friednShipId);//dostluq isteyin redd etmek
    Task<List<AcceptedFollowingUserDto>> GetAllAcceptedFollowingUsersAsync(string UserId);//dostlugu qebul etmis butun izlediyin userleri gosterir
    Task<List<AcceptedFollowerUserDto>> GetAllAcceptedFollowerUsersAsync(string UserId);//dostlugu qebul etdiyin butun seni izleyen userleri gosterir
    Task<List<MyFriendsDto>> GetAllMyFriendsAsync(string UserId);//burada qarsiliqli bir-birimizi izlediyimiz userleri mene getirir
    Task<List<PendingFollowerUserDto>> GetAllPendingFollowerUsersAsync(string UserId);//dostlugu qebul olunmamis, gozlemede olan, seni izlemek isteyen userleri gosterir
}
