using LinkPoint.Business.DTOs.CommentDTOs;

namespace LinkPoint.Business.DTOs.AdminPostDTOs;

public class GetPostDto
{
    public string UserId { get; set; }
    public int PostId { get; set; }
    public int LikeCount { get; set; }
    public string UserName { get; set; }
    public string? Text { get; set; }
    public string UserProfileImage { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string ElapsedTime { get; set; }
    public string UploadTime { get; set; }
    public bool IsDelete { get; set; }
    public List<CommentGetDto> Comments { get; set; }
}
