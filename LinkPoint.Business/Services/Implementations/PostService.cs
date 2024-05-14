using LinkPoint.Business.DTOs.PostDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LinkPoint.Business.Services.Implementations;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public PostService(IPostRepository postRepository,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager)
    {
        _postRepository = postRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    public async Task<List<PostGetDto>> GetAllOneUserPostsAsync(string UserId)
    {
        var user=await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        if (!await _postRepository.IsExist(p => p.UserId == user.Id)) throw new PostNotFoundException(404, "Post is not found"); 
        var UserPosts=await _postRepository.GetAllAsync(p=>p.UserId==user.Id,"Image","Video");
        List<PostGetDto> posts = new List<PostGetDto>();
        foreach (var UserPost in UserPosts)
        {
            PostGetDto Post = new PostGetDto()
            {
                UserId=UserPost.UserId,
                LikeCount=UserPost.LikeCount,
                Text=UserPost.Text,
                ImageUrl=UserPost.Image.ImageUrl,
                VideoUrl=UserPost.Video.VideoUrl
            };
            posts.Add(Post);
        }
        return posts;
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
