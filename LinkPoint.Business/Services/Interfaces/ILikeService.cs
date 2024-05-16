using LinkPoint.Business.DTOs.LikeDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface ILikeService
{
    Task<List<LikeGetDto>> GetAllUsersLikedPostAsync(int PostId);//Postu like-layan butun userleri gosterir
    Task AddLikeToPostAsync(int PostId);
    Task RemoveLikeFromPostAsync(int PostId);
}
