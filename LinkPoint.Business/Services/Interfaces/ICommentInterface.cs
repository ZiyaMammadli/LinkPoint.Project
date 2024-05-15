using LinkPoint.Business.DTOs.CommentDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface ICommentInterface
{
    Task CreateCommentAsync(CommentPostDto commentPostDto);
    Task UpdateCommentAsync(CommentPutDto commentPutDto);
    Task SoftDeleteComment(int CommentId,CommentDeleteDto commentDeleteDto);
}
