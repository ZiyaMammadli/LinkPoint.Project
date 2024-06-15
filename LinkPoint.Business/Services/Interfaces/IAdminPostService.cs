using LinkPoint.Business.DTOs.AdminPostDTOs;
using LinkPoint.Business.DTOs.PostDTOs;

namespace LinkPoint.Business.Services.Interfaces;

public interface IAdminPostService
{
    Task<List<GetPostDto>> GetAllPostsAsync();//
    Task<GetPostDto> GetByIdPostAsync(int PostId);//
    Task SoftDeletePostAsync(int PostId);//
    Task ActivatePostAsync(int PostId);
    Task DeleteAsync(int PostId);//
}
