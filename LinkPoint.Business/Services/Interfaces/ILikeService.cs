namespace LinkPoint.Business.Services.Interfaces;

public interface ILikeService
{
    Task AddLikeToPostAsync(int PostId);
    Task RemoveLikeFromPostAsync(int PostId);
}
