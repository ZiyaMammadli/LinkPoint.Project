using LinkPoint.Business.DTOs.PostDTOs;
using LinkPoint.Business.Services.Interfaces;

namespace LinkPoint.Business.Services.Implementations;

public class PostService : IPostService
{
    public Task<List<PostGetDto>> GetAllOneUserPostsAsync(string UserId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PostGetDto>> GetAllPostsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PostGetDto> GetByIdPostAsync(int PostId)
    {
        throw new NotImplementedException();
    }
    public Task CreatePostWithImageAsync(PostCreateWithImageDto postCreateWithImageDto)
    {
        throw new NotImplementedException();
    }

    public Task CreatePostWithTextAsync(PostCreateWithTextDto postCreateWithImageDto)
    {
        throw new NotImplementedException();
    }

    public Task CreatePostWithVideoAsync(PostCreateWithVideoDto postCreateWithVideoDto)
    {
        throw new NotImplementedException();
    }
    public Task SoftDeletePostAsync(int PostId, PostDeleteDto postDeleteDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePostWithTextAsync(int PostId, PostUpdateWithTextDto postUpdateWithTextDto)
    {
        throw new NotImplementedException();
    }
}
