namespace LinkPoint.MVC.ViewModels;

public class PostGetViewModel
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
    public List<CommentGetViewModel> Comments { get; set; }
}
