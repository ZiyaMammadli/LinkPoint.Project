using LinkPoint.Business.DTOs.CommentDTOs;
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
    private readonly ICommentRepository _commentRepository;

    public PostService(IPostRepository postRepository,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager,
        IImageRepository imageRepository,
        IVideoRepository videoRepository,
        ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _imageRepository = imageRepository;
        _videoRepository = videoRepository;
        _commentRepository = commentRepository;
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
        //if (!await _postRepository.IsExist(p => p.UserId == user.Id && p.IsDeleted == false)) throw new PostNotFoundException(404, "Post is not found"); 
        var UserPosts=await _postRepository.GetAllAsync(p=>p.UserId==user.Id && p.IsDeleted==false,"Image","Video", "Comments.User.Images");
        List<PostGetDto> posts = new List<PostGetDto>();
        if(UserPosts.Count > 0)
        {
            foreach (var UserPost in UserPosts)
            {
                PostGetDto Post = new PostGetDto()
                {
                    UserId = UserId,
                    PostId = UserPost.Id,
                    UserName = user.UserName,
                    LikeCount = UserPost.LikeCount,
                    Text = UserPost.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = UserPost.Image?.ImageUrl,
                    VideoUrl = UserPost.Video?.VideoUrl,
                    ElapsedTime = UserPost.CreatedDate.GetElapsedTime(),
                    UploadTime = UserPost.CreatedDate.ToString("MMMM d, yyyy"),
                    Comments = UserPost.Comments.Select(c => new CommentGetDto
                    {
                        UserId=c.UserId,
                        CommentId = c.Id,
                        Text = c.Text,
                        UserName = c.User.UserName,
                        UserProfileImage = c.User.Images.FirstOrDefault(i => i.IsPostImage == false).ImageUrl,
                        ElapsedTime = c.CreatedDate.GetElapsedTime(),
                    }).ToList()
                };
                posts.Add(Post);
            }
            return posts;
        }
        return posts;
    }

    public async Task<List<PostGetDto>> GetAllPostsAsync()
    {
        var posts=await _postRepository.GetAllAsync(p=>p.IsDeleted==false,"Image","Video","User", "Comments.User.Images");
        if(posts.Count==0) throw new PostNotFoundException(404,"Post is not found");
        List<PostGetDto> postGetDtos = new List<PostGetDto>();
        foreach (var Post in posts)
        {
            var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == Post.User.Id && i.IsPostImage == false);
            if (profileImage is null)
            {
                throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
            }
                PostGetDto postGetDto = new PostGetDto()
                {
                    UserId=Post.User.Id,
                    PostId=Post.Id,
                    UserName = Post.User.UserName,
                    LikeCount = Post.LikeCount,
                    Text = Post.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = Post.Image?.ImageUrl,
                    VideoUrl = Post.Video?.VideoUrl,
                    ElapsedTime = Post.CreatedDate.GetElapsedTime(),
                    Comments = Post.Comments.Select(c => new CommentGetDto
                    {
                        UserId = c.UserId,
                        CommentId = c.Id,
                        Text = c.Text,
                        UserName = c.User.UserName,
                        UserProfileImage =c.User.Images.FirstOrDefault(i=>i.IsPostImage==false).ImageUrl,
                        ElapsedTime = c.CreatedDate.GetElapsedTime(),
                    }).ToList()
                };
                postGetDtos.Add(postGetDto);
        }
        return postGetDtos;
    }

    public async Task<PostGetDto> GetByIdPostAsync(int PostId)
    {
        var post=await _postRepository.GetSingleAsync(p=>p.Id==PostId && p.IsDeleted==false,"User","Video","Image", "Comments.User.Images");
        if (post is null) throw new PostNotFoundException(404,"Post is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == post.User.Id && i.IsPostImage == false);
        if (profileImage is null)
        {
            throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        }
        PostGetDto postGetDto = new PostGetDto()
        {
            UserId = post.User.Id,
            PostId = PostId,
            UserName = post.User.UserName,
            LikeCount = post.LikeCount,
            Text = post.Text,
            UserProfileImage = profileImage.ImageUrl,
            ImageUrl = post.Image?.ImageUrl,
            VideoUrl = post.Video?.VideoUrl,
            ElapsedTime = post.CreatedDate.GetElapsedTime(),
            Comments = post.Comments.Select(c => new CommentGetDto
            {
                UserId = c.UserId,
                CommentId = c.Id,
                Text = c.Text,
                UserName = c.User.UserName,
                UserProfileImage = c.User.Images.FirstOrDefault(i => i.IsPostImage == false).ImageUrl,
                ElapsedTime = c.CreatedDate.GetElapsedTime(),
            }).ToList()
        };                  
        return postGetDto;      
    }
    public async Task CreatePostWithImageAsync(PostCreateWithImageDto postCreateWithImageDto)
    {
        var user = await _userManager.FindByIdAsync(postCreateWithImageDto.UserId);
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
        var user = await _userManager.FindByIdAsync(postCreateWithTextDto.UserId);
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
        var user = await _userManager.FindByIdAsync(postCreateWithVideoDto.UserId);
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
        var postImage = await _imageRepository.GetSingleAsync(i=>i.PostId==postDeleteDto.Id && i.IsDeleted==false);
        if (postImage is not null) postImage.IsDeleted = true;
        var postComments=await _commentRepository.GetAllAsync(c=>c.PostId==postDeleteDto.Id && c.IsDeleted==false);
        if(postComments.Count > 0)
        {
            foreach (var postComment in postComments)
            {
                postComment.IsDeleted = true;
            }
        }
        currentPost.IsDeleted = true;
        await _postRepository.CommitAsync();
    }

    public async Task<List<PostGetDto>> GetAllPostsForVideoAsync()
    {
        var posts = await _postRepository.GetAllAsync(p => p.IsDeleted == false && p.Image==null && p.Video!=null, "Image", "Video", "User", "Comments.User.Images");
        if (posts.Count == 0) throw new PostNotFoundException(404, "Post is not found");
        List<PostGetDto> postGetDtos = new List<PostGetDto>();
        foreach (var Post in posts)
        {
            var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == Post.User.Id && i.IsPostImage == false);
            if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                PostGetDto postGetDto = new PostGetDto()
                {
                    UserId = Post.User.Id,
                    PostId = Post.Id,
                    UserName = Post.User.UserName,
                    LikeCount = Post.LikeCount,
                    Text = Post.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = null,
                    VideoUrl = Post.Video.VideoUrl,
                    ElapsedTime = Post.CreatedDate.GetElapsedTime(),
                    Comments = Post.Comments.Select(c => new CommentGetDto
                    {
                        UserId = c.UserId,
                        CommentId = c.Id,
                        Text = c.Text,
                        UserName = c.User.UserName,
                        UserProfileImage = c.User.Images.FirstOrDefault(i => i.IsPostImage == false).ImageUrl,
                        ElapsedTime = c.CreatedDate.GetElapsedTime(),
                    }).ToList()
                };
                postGetDtos.Add(postGetDto);
        }
        return postGetDtos;
    }

    public async Task<List<PostGetDto>> GetAllPostsForImageAsync()
    {
        var posts = await _postRepository.GetAllAsync(p => p.IsDeleted == false && p.Video==null && p.Image!=null, "Image", "Video", "User", "Comments.User.Images");
        if (posts.Count == 0) throw new PostNotFoundException(404, "Post is not found");
        List<PostGetDto> postGetDtos = new List<PostGetDto>();
        foreach (var Post in posts)
        {
            var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == Post.User.Id && i.IsPostImage == false);
            if (profileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
                PostGetDto postGetDto = new PostGetDto()
                {
                    UserId = Post.User.Id,
                    PostId = Post.Id,
                    UserName = Post.User.UserName,
                    LikeCount = Post.LikeCount,
                    Text = Post.Text,
                    UserProfileImage = profileImage.ImageUrl,
                    ImageUrl = Post.Image.ImageUrl,
                    VideoUrl = null,
                    ElapsedTime = Post.CreatedDate.GetElapsedTime(),
                    Comments = Post.Comments.Select(c => new CommentGetDto
                    {
                        UserId = c.UserId,
                        CommentId = c.Id,
                        Text = c.Text,
                        UserName = c.User.UserName,
                        UserProfileImage = c.User.Images.FirstOrDefault(i => i.IsPostImage == false).ImageUrl,
                        ElapsedTime = c.CreatedDate.GetElapsedTime(),
                    }).ToList()
                };
                postGetDtos.Add(postGetDto);
        }
        return postGetDtos;
    }

    public async Task DeleteAsync(int PostId)
    {
        var currentPost = await _postRepository.GetByIdAsync(PostId);
        if (currentPost is null) throw new PostNotFoundException(404, "Post is not found");
        var postImage = await _imageRepository.GetSingleAsync(i => i.PostId == PostId);
        if (postImage is not null)
        {
            _imageRepository.Delete(postImage);
        }
        var postComments = await _commentRepository.GetAllAsync(c => c.PostId == PostId);
        if (postComments.Count > 0)
        {
            foreach (var postComment in postComments)
            {
                _commentRepository.Delete(postComment);
            }
        }
        _postRepository.Delete(currentPost);
        await _postRepository.CommitAsync();
    }
}
