using LinkPoint.Business.DTOs.AdminPostDTOs;
using LinkPoint.Business.DTOs.CommentDTOs;
using LinkPoint.Business.DTOs.PostDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using LinkPoint.Business.Utilities.Extentions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using LinkPoint.Data.Repositories;

namespace LinkPoint.Business.Services.Implementations;

public class AdminPostService:IAdminPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IImageRepository _imageRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IVideoRepository _videoRepository;

    public AdminPostService(IPostRepository postRepository,IImageRepository imageRepository,ICommentRepository commentRepository,IVideoRepository videoRepository)
    {
        _postRepository = postRepository;
        _imageRepository = imageRepository;
        _commentRepository = commentRepository;
        _videoRepository = videoRepository;
    }
    public async Task<List<GetPostDto>> GetAllPostsAsync()
    {
        var posts = await _postRepository.GetAllAsync(null, "Image", "Video", "User", "Comments.User.Images");
        if (posts.Count == 0) throw new PostNotFoundException(404, "Post is not found");
        List<GetPostDto> postGetDtos = new List<GetPostDto>();
        foreach (var Post in posts)
        {
            var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == Post.User.Id && i.IsPostImage == false);
            if (profileImage is null)
            {
                throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
            }
            GetPostDto postGetDto = new GetPostDto()
            {
                UserId = Post.User.Id,
                PostId = Post.Id,
                UserName = Post.User.UserName,
                LikeCount = Post.LikeCount,
                Text = Post.Text,
                UserProfileImage = profileImage.ImageUrl,
                ImageUrl = Post.Image?.ImageUrl,
                VideoUrl = Post.Video?.VideoUrl,
                ElapsedTime = Post.CreatedDate.GetElapsedTime(),
                UploadTime = Post.CreatedDate.ToString(),
                IsDelete=Post.IsDeleted,
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
    public async Task<GetPostDto> GetByIdPostAsync(int PostId)
    {
        var post = await _postRepository.GetSingleAsync(p => p.Id == PostId, "User", "Video", "Image", "Comments.User.Images");
        if (post is null) throw new PostNotFoundException(404, "Post is not found");
        var profileImage = await _imageRepository.GetSingleAsync(i => i.UserId == post.User.Id && i.IsPostImage == false);
        if (profileImage is null)
        {
            throw new ProfileImageNotFoundException(404, "ProfileImage is not found");
        }
        GetPostDto postGetDto = new GetPostDto()
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
            UploadTime = post.CreatedDate.ToString(),
            IsDelete =post.IsDeleted,
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
    public async Task SoftDeletePostAsync(int PostId)
    {
        var currentPost = await _postRepository.GetByIdAsync(PostId);
        if (currentPost is null) throw new PostNotFoundException(404, "Post is not found");
        var postImage = await _imageRepository.GetSingleAsync(i => i.PostId == PostId && i.IsDeleted == false);
        if (postImage is not null) postImage.IsDeleted = true;
        var postComments = await _commentRepository.GetAllAsync(c => c.PostId == PostId && c.IsDeleted == false);
        if (postComments.Count > 0)
        {
            foreach (var postComment in postComments)
            {
                postComment.IsDeleted = true;
            }
        }
        currentPost.IsDeleted = true;
        await _postRepository.CommitAsync();
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
        var postVideo = await _videoRepository.GetByIdAsync(PostId);
        if (postVideo is not null)
        {
            _videoRepository.Delete(postVideo);
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

    public async Task ActivatePostAsync(int PostId)
    {
        var currentPost = await _postRepository.GetSingleAsync(p=>p.Id==PostId && p.IsDeleted==true);
        if (currentPost is null) throw new PostNotFoundException(404, "Post is not found");
        var postImage = await _imageRepository.GetSingleAsync(i => i.PostId == PostId && i.IsDeleted == true);
        if (postImage is not null) postImage.IsDeleted = false;
        var postComments = await _commentRepository.GetAllAsync(c => c.PostId == PostId && c.IsDeleted == true);
        if (postComments.Count > 0)
        {
            foreach (var postComment in postComments)
            {
                postComment.IsDeleted = false;
            }
        }
        currentPost.IsDeleted = false;
        await _postRepository.CommitAsync();
    }
}
