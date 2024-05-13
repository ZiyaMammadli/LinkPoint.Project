namespace LinkPoint.Core.Entities;

public class Image:BaseEntity
{
    public string? UserId { get; set; }
    public int? PostId { get; set; }
    public bool IsPostImage { get; set; } //true = postImage, false = ProfileImage, null = ProfileBackgroundImage
    public string ImageUrl { get; set; }
    public AppUser User { get; set; }
    public Post Post { get; set; }
}
