using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IPostService
{
    Task<List<PostGetDto>> GetAllAuthUserPostsForImageAsync();//
    Task<List<PostGetDto>> GetAllAuthUserPostsForVideoAsync();//
    Task<List<PostGetDto>> GetAllPostsForVideoAsync();//
    Task<List<PostGetDto>> GetAllPostsForImageAsync();//
    Task<List<PostGetDto>> GetAllPostsAsync();//
    Task<List<PostGetDto>> GetAllAuthUserPostsAsync();//login olmus userin butun postlarin gosterir
    Task<List<PostGetDto>> GetAllOneUserPostsAsync(string UserId);//her hansi bir userin butun postlarin gosterir
    Task<PostGetDto> GetByIdPostAsync(int PostId);//
    Task CreatePostWithImageAsync(PostCreateWithImageDto postCreateWithImageDto);//
    Task CreatePostWithVideoAsync(PostCreateWithVideoDto postCreateWithVideoDto);//
    Task CreatePostWithTextAsync(PostCreateWithTextDto postCreateWithTextDto);//
    Task UpdatePostWithTextAsync(int PostId,PostUpdateWithTextDto postUpdateWithTextDto);//
    Task SoftDeletePostAsync(int PostId,PostDeleteDto postDeleteDto);//
}
