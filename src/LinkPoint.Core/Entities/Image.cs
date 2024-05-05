namespace LinkPoint.Core.Entities;

public class Image:BaseEntity
{
    public string? UserId { get; set; }
    public bool IsPostImage { get; set; } //true = postImage, false = ProfileImage, null = ProfileBackgroundImage
    public string ImageUrl { get; set; }
    public AppUser User { get; set; }
    public List<Post> posts { get; set; }
}
