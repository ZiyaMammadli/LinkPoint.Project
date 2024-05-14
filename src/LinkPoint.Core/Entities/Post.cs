namespace LinkPoint.Core.Entities;

public class Post:BaseEntity
{
    public string UserId { get; set; }
    public int LikeCount { get; set; }
    public string? Text { get; set; }
    public Image? Image { get; set; }
    public Video? Video { get; set; }
    public AppUser User { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Like>? Likes { get; set; }
}
