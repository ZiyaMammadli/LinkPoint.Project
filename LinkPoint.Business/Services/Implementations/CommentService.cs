using LinkPoint.Business.DTOs.CommentDTOs;
using LinkPoint.Business.Services.Interfaces;
using LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;
using LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;
using LinkPoint.Business.Utilities.Extentions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace LinkPoint.Business.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IImageRepository _imageRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentService(ICommentRepository commentRepository,
        IPostRepository postRepository,
        UserManager<AppUser> userManager,
        IImageRepository imageRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userManager = userManager;
        _imageRepository = imageRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<List<CommentGetDto>> GetAllCommentForPostAsync(int PostId)
    {
        if (!await _commentRepository.IsExist(c => c.PostId == PostId && c.IsDeleted == false)) throw new CommentNotFoundException(404, "Comment is not found");
        var comments= await _commentRepository.GetAllAsync(c=>c.PostId==PostId && c.IsDeleted == false, "User");
        List<CommentGetDto> commentGetDtos = new List<CommentGetDto>();
        foreach (var comment in comments)
        {
            var userProfileImage=await _imageRepository.GetSingleAsync(i=>i.UserId==comment.UserId && i.IsDeleted == false);
            if (userProfileImage is null) throw new ProfileImageNotFoundException(404, "ProfileImage is not found");

            CommentGetDto commentGetDto = new CommentGetDto()
            {
                CommentId = comment.Id,
                Text = comment.Text,
                UserName = comment.User.UserName,
                UserProfileImage = userProfileImage.ImageUrl,
                ElapsedTime=comment.CreatedDate.GetElapsedTime(),
            };
            commentGetDtos.Add(commentGetDto);
        }
        return commentGetDtos;
    }

    public async Task CreateCommentAsync(CommentPostDto commentPostDto)
    {
        if (!await _postRepository.IsExist(p => p.Id == commentPostDto.PostId && p.IsDeleted==false)) throw new PostNotFoundException(404, "Post is not found");
        var user=await _userManager.FindByIdAsync(commentPostDto.UserId);
        Comment comment = new Comment()
        {
            PostId = commentPostDto.PostId,
            UserId=user.Id,
            Text = commentPostDto.Text,
            CreatedDate=DateTime.UtcNow,
            UpdatedDate=DateTime.UtcNow,
        };
        await _commentRepository.InsertAsync(comment);
        await _commentRepository.CommitAsync();
    }

    public async Task UpdateCommentAsync(int CommentId, CommentPutDto commentPutDto)
    {
        if (CommentId != commentPutDto.CommentId) throw new IdNotValidException(404, "CommentId is not valid");
        var currentComment=await _commentRepository.GetByIdAsync(commentPutDto.CommentId);
        if (currentComment is null) throw new CommentNotFoundException(404, "Comment is not found");
        currentComment.Text = commentPutDto.Text;
        currentComment.UpdatedDate = DateTime.UtcNow;
        await _commentRepository.CommitAsync();
    }

    public async Task SoftDeleteCommentAsync(int CommentId, CommentDeleteDto commentDeleteDto)
    {
        if (CommentId != commentDeleteDto.CommentId) throw new IdNotValidException(404, "CommentId is not valid");
        var currentComment = await _commentRepository.GetByIdAsync(commentDeleteDto.CommentId);
        if (currentComment is null) throw new CommentNotFoundException(404, "Comment is not found");
        currentComment.IsDeleted = true;
        await _commentRepository.CommitAsync();
    }
}
