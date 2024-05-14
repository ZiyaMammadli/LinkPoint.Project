using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IPostService
{
    Task<PostGetDto> GetByIdPostAsync(int PostId);
    Task<List<PostGetDto>> GetAllOneUserPostsAsync(string UserId);
    Task<List<PostGetDto>> GetAllPostsAsync();
    Task CreatePostWithImageAsync(PostCreateWithImageDto postCreateWithImageDto);
    Task CreatePostWithVideoAsync(PostCreateWithVideoDto postCreateWithVideoDto);
    Task CreatePostWithTextAsync(PostCreateWithTextDto postCreateWithImageDto);
    Task UpdatePostWithTextAsync(int PostId,PostUpdateWithTextDto postUpdateWithTextDto);
    Task SoftDeletePostAsync(int PostId,PostDeleteDto postDeleteDto);
}
