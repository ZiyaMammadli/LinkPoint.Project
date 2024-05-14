namespace LinkPoint.Business.DTOs.PostDTOs;

public class PostGetDto
{
    public string UserId { get; set; }
    public int LikeCount { get; set; }
    public string? Text { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
}
