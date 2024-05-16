namespace LinkPoint.Business.Services.Interfaces;

public interface ILikeService
{
    Task AddLikeToPost(int PostId);
    Task RemoveLikeFromPost(int PostId);
}
