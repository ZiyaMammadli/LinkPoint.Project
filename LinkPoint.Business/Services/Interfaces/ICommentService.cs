using LinkPoint.Business.DTOs.CommentDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface ICommentService
{
    Task<List<CommentGetDto>> GetAllCommentForPostAsync(int PostId);//1 posta aid butun commentleri qaytarir
    Task<CommentGetDto> CreateCommentAsync(CommentPostDto commentPostDto);//comment yaratmaq ucundur 
    Task UpdateCommentAsync(int CommentId,CommentPutDto commentPutDto);//comment yenilemek ucundur 
    Task SoftDeleteCommentAsync(int CommentId,CommentDeleteDto commentDeleteDto);//comment-in isdelete deyerin true etmek ucundur 
}
