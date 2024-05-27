using LinkPoint.Business.DTOs.LikeDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface ILikeService
{
    Task<List<LikeGetAllDto>> GetAllLikesAsync();
    Task<List<LikeGetDto>> GetAllUsersLikedPostAsync(int PostId);//Postu like-layan butun userleri gosterir
    Task<int> AddLikeToPostAsync(string UserId,int PostId);
    Task<int> RemoveLikeFromPostAsync(string UserId, int PostId);
}
