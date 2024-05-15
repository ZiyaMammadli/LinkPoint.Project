namespace LinkPoint.Business.DTOs.PostDTOs;

public class PostGetDto
{
    public int LikeCount { get; set; }
    public string UserName { get; set; }
    public string? Text { get; set; }
    public string UserProfileImage { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string ElapsedTime { get; set; }
}
