using LinkPoint.Business.DTOs.CommentDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface ICommentService
{
    Task<List<CommentGetDto>> GetAllCommentForPostAsync(int PostId);
    Task CreateCommentAsync(CommentPostDto commentPostDto);
    Task UpdateCommentAsync(int CommentId,CommentPutDto commentPutDto);
    Task SoftDeleteCommentAsync(int CommentId,CommentDeleteDto commentDeleteDto);
}
