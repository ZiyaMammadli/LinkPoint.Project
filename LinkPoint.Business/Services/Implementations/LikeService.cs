using Google.Api.Gax;
using LinkPoint.Business.DTOs.LikeDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.CommonExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Repositories;
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
    private readonly IImageRepository _imageRepository;

    public LikeService(IPostRepository postRepository,
        ILikeRepository likeRepository,
        IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager,
        IImageRepository imageRepository)
    {
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _imageRepository = imageRepository;
    }

    public async Task<List<LikeGetDto>> GetAllUsersLikedPostAsync(int PostId)
    {
        var post = await _postRepository.GetByIdAsync(PostId);
        if (post is null) throw new PostNotFoundException(404, "Post is not found");
        if (!await _likeRepository.IsExist(l => l.PostId == PostId)) throw new LikeNotFoundException(404, "Like is not found");
        var likes = await _likeRepository.GetAllAsync(l => l.PostId == PostId);
        List<LikeGetDto> likeGetDtos = new List<LikeGetDto>();
        foreach (var like in likes)
        {
            var user =await _userManager.FindByIdAsync(like.UserId);
            if (user is null) throw new UserNotFoundException(404, "User is not found");
            var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
            if (profileImage is null)
            {
                throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
            }
            LikeGetDto likeGetDto = new LikeGetDto()
            {
                UserName=user.UserName,
                UserProfilImage= profileImage.ImageUrl,
            };
            likeGetDtos.Add(likeGetDto);
        }
        return likeGetDtos;
    }
    public async Task AddLikeToPostAsync(int PostId)
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var post=await _postRepository.GetByIdAsync(PostId);
        if (post is null) throw new PostNotFoundException(404, "Post is not found");
        if (await _likeRepository.IsExist(l => l.UserId == user.Id && l.PostId == post.Id)) throw new AlreadyExistException(403,"Like is already exist");
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
    public async Task RemoveLikeFromPostAsync(int PostId)
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
