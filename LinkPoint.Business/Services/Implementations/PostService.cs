using LinkPoint.Business.DTOs.PostDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using LinkPoint.Business.Utilities.Extentions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LinkPoint.Business.Services.Implementations;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IImageRepository _imageRepository;
    private readonly IVideoRepository _videoRepository;

    public PostService(IPostRepository postRepository,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager,
        IImageRepository imageRepository,
        IVideoRepository videoRepository)
    {
        _postRepository = postRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _imageRepository = imageRepository;
        _videoRepository = videoRepository;
    }
    public async Task<List<PostGetDto>> GetAllOneUserPostsAsync(string UserId)
    {
        var user=await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
        if (profileImage is null)
        {
            throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        }
        if (!await _postRepository.IsExist(p => p.UserId == user.Id)) throw new PostNotFoundException(404, "Post is not found"); 
        var UserPosts=await _postRepository.GetAllAsync(p=>p.UserId==user.Id,"Image","Video");
        List<PostGetDto> posts = new List<PostGetDto>();
        foreach (var UserPost in UserPosts)
        {
            if (UserPost.Image is null && UserPost.Video is null && UserPost.IsDeleted==false)
            {
                PostGetDto Post = new PostGetDto()
                {
                    UserName = user.UserName,
                    LikeCount = UserPost.LikeCount,
                    Text = UserPost.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = null,
                    VideoUrl = null
                };
                posts.Add(Post);
                continue;
            }
            if (UserPost.Image is null && UserPost.IsDeleted == false)
            {
                PostGetDto Post = new PostGetDto()
                {
                    UserName = user.UserName,
                    LikeCount = UserPost.LikeCount,
                    Text = UserPost.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = null,
                    VideoUrl = UserPost.Video.VideoUrl
                };
                posts.Add(Post);
            }
            if (UserPost.Video is null && UserPost.IsDeleted == false)
            {
                PostGetDto Post = new PostGetDto()
                {
                    UserName = user.UserName,
                    LikeCount = UserPost.LikeCount,
                    Text = UserPost.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = UserPost.Image.ImageUrl,
                    VideoUrl = null
                };
                posts.Add(Post);
            }
        }
        return posts;
    }

    public async Task<List<PostGetDto>> GetAllPostsAsync()
    {
        var posts=await _postRepository.GetAllAsync(null,"Image","Video","User");
        if(posts.Count==0) throw new PostNotFoundException(404,"Post is not found");
        List<PostGetDto> postGetDtos = new List<PostGetDto>();
        foreach (var Post in posts)
        {
            var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == Post.User.Id && i.IsPostImage == false);
            if (profileImage is null)
            {
                throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
            }
            if (Post.Image is null && Post.Video is null && Post.IsDeleted == false)
            {
                PostGetDto postGetDto = new PostGetDto()
                {
                    UserName = Post.User.UserName,
                    LikeCount = Post.LikeCount,
                    Text = Post.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = null,
                    VideoUrl = null
                };
                postGetDtos.Add(postGetDto);
                continue;
            }
            if (Post.Image is null && Post.IsDeleted == false)
            {
                PostGetDto postGetDto = new PostGetDto()
                {
                    UserName = Post.User.UserName,
                    LikeCount = Post.LikeCount,
                    Text = Post.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = null,
                    VideoUrl = Post.Video.VideoUrl
                };
                postGetDtos.Add(postGetDto);
            }
            if (Post.Video is null && Post.IsDeleted == false)
            {
                PostGetDto postGetDto = new PostGetDto()
                {
                    UserName = Post.User.UserName,
                    LikeCount = Post.LikeCount,
                    Text = Post.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = Post.Image.ImageUrl,
                    VideoUrl = null
                };
                postGetDtos.Add(postGetDto);
            }
        }
        return postGetDtos;
    }

    public async Task<PostGetDto> GetByIdPostAsync(int PostId)
    {
        var post=await _postRepository.GetSingleAsync(p=>p.Id==PostId,"User","Video","Image");
        if (post is null) throw new PostNotFoundException(404,"Post is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == post.User.Id && i.IsPostImage == false);
        if (profileImage is null)
        {
            throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        }
        PostGetDto postGetDto = new PostGetDto();
        if (post.Image is null && post.Video is null && post.IsDeleted == false)
        {
            postGetDto.UserName = post.User.UserName;
            postGetDto.LikeCount = post.LikeCount;
            postGetDto.Text = post.Text;
            postGetDto.UserProfileImage = profileImage.ImageUrl;
            postGetDto.ImageUrl = null;
            postGetDto.VideoUrl = null;
            return postGetDto;
        }
        if (post.Image is null && post.IsDeleted == false)
        {
            postGetDto.UserName = post.User.UserName;
            postGetDto.LikeCount = post.LikeCount;
            postGetDto.Text = post.Text;
            postGetDto.UserProfileImage = profileImage.ImageUrl;
            postGetDto.ImageUrl = null;
            postGetDto.VideoUrl = post.Video.VideoUrl;
        }
        if (post.Video is null && post.IsDeleted == false)
        {
            postGetDto.UserName = post.User.UserName;
            postGetDto.LikeCount = post.LikeCount;
            postGetDto.Text = post.Text;
            postGetDto.UserProfileImage = profileImage.ImageUrl;
            postGetDto.ImageUrl = post.Image.ImageUrl;
            postGetDto.VideoUrl = null;
        }
        return postGetDto;      
    }
    public async Task<List<PostGetDto>> GetAllAuthUserPostsAsync()
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user=await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == user.Id && i.IsPostImage == false);
        if (profileImage is null)
        {
            throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        }
        if (!await _postRepository.IsExist(p => p.UserId == user.Id)) throw new PostNotFoundException(404, "Post is not found");
        var UserPosts = await _postRepository.GetAllAsync(p => p.UserId == user.Id, "Image", "Video");
        List<PostGetDto> posts = new List<PostGetDto>();
        foreach (var UserPost in UserPosts)
        {
            if (UserPost.Image is null && UserPost.Video is null && UserPost.IsDeleted == false)
            {
                PostGetDto Post = new PostGetDto()
                {
                    UserName = user.UserName,
                    LikeCount = UserPost.LikeCount,
                    Text = UserPost.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = null,
                    VideoUrl = null
                };
                posts.Add(Post);
                continue;
            }
            if (UserPost.Image is null && UserPost.IsDeleted == false)
            {
                PostGetDto Post = new PostGetDto()
                {
                    UserName = user.UserName,
                    LikeCount = UserPost.LikeCount,
                    Text = UserPost.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = null,
                    VideoUrl = UserPost.Video.VideoUrl
                };
                posts.Add(Post);
            }
            if (UserPost.Video is null && UserPost.IsDeleted == false)
            {
                PostGetDto Post = new PostGetDto()
                {
                    UserName = user.UserName,
                    LikeCount = UserPost.LikeCount,
                    Text = UserPost.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = UserPost.Image.ImageUrl,
                    VideoUrl = null
                };
                posts.Add(Post);
            }
        }
        return posts;

    }
    public async Task CreatePostWithImageAsync(PostCreateWithImageDto postCreateWithImageDto)
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        Post post = new Post()
        {
            UserId= user.Id,
            Text = postCreateWithImageDto.Text,
            LikeCount = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _postRepository.InsertAsync(post);
        await _postRepository.CommitAsync();
        string apiKey = _configuration["GoogleCloud:ApiKey"];
        Image image = new Image()
        {
            UserId=null,
            PostId=post.Id,
            IsPostImage=true,
            ImageUrl=postCreateWithImageDto.PostImageFile.SaveFile(apiKey,"Images"),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _imageRepository.InsertAsync(image);
        await _imageRepository.CommitAsync();
    }

    public async Task CreatePostWithTextAsync(PostCreateWithTextDto postCreateWithTextDto)
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        Post post = new Post()
        {
            UserId = user.Id,
            Text = postCreateWithTextDto.Text,
            LikeCount = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _postRepository.InsertAsync(post);
        await _postRepository.CommitAsync();
    }

    public async Task CreatePostWithVideoAsync(PostCreateWithVideoDto postCreateWithVideoDto)
    {
        var userName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        string username = JsonConvert.DeserializeObject<string>(userName);
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        Post post = new Post()
        {
            UserId = user.Id,
            Text = postCreateWithVideoDto.Text,
            LikeCount = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _postRepository.InsertAsync(post);
        await _postRepository.CommitAsync();
        string apiKey = _configuration["GoogleCloud:ApiKey"];
        Video video = new Video()
        {
            PostId = post.Id,
            VideoUrl=postCreateWithVideoDto.PostVideoFile.SaveFile(apiKey,"Videos"),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _videoRepository.InsertAsync(video);
        await _videoRepository.CommitAsync();
    }
    public async Task UpdatePostWithTextAsync(int PostId, PostUpdateWithTextDto postUpdateWithTextDto)
    {
        if(PostId!=postUpdateWithTextDto.Id) throw new IdNotValidException(400, "Id is not Valid");
        var currentPost=await _postRepository.GetByIdAsync(postUpdateWithTextDto.Id);
        if (currentPost is null) throw new PostNotFoundException(404, "Post is not found");
        currentPost.Text = postUpdateWithTextDto.Text;
        currentPost.UpdatedDate = DateTime.UtcNow;
        await _postRepository.CommitAsync();
    }
    public async Task SoftDeletePostAsync(int PostId, PostDeleteDto postDeleteDto)
    {
        if (PostId != postDeleteDto.Id) throw new IdNotValidException(400, "Id is not Valid");
        var currentPost = await _postRepository.GetByIdAsync(postDeleteDto.Id);
        if (currentPost is null) throw new PostNotFoundException(404, "Post is not found");
        currentPost.IsDeleted = true;
        await _postRepository.CommitAsync();
    }

}
