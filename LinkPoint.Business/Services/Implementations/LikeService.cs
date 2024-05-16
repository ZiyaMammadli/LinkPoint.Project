using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace LinkPoint.Business.Services.Implementations;

public class LikeService : ILikeService
{
    private readonly IPostRepository _postRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public LikeService(IPostRepository postRepository,
        ILikeRepository likeRepository,
        IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager)
    {
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    public async Task AddLikeToPost(int PostId)
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var post=await _postRepository.GetByIdAsync(PostId);
        if (post is null) throw new PostNotFoundException(404, "Post is not found");
        Like like = new Like()
        {
            UserId=user.Id,
            PostId=PostId,
            CreatedDate=DateTime.UtcNow,
            UpdatedDate=DateTime.UtcNow,
        };
        post.LikeCount++;
        await _likeRepository.InsertAsync(like);
        await _likeRepository.CommitAsync();
    }

    public async Task RemoveLikeFromPost(int PostId)
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var post = await _postRepository.GetByIdAsync(PostId);
        if (post is null) throw new PostNotFoundException(404, "Post is not found");
        var like = await _likeRepository.GetSingleAsync(l=>l.UserId==user.Id && l.PostId==PostId);
        _likeRepository.Delete(like);
        post.LikeCount--;
        await _likeRepository.CommitAsync();
    }
}
