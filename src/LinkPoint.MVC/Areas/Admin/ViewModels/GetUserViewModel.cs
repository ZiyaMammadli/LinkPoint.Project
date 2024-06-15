namespace LinkPoint.MVC.Areas.Admin.ViewModels;

public class GetUserViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingsCount { get; set; }
    public int ProfileImageId { get; set; }
    public int BackgroundImageId { get; set; }
    public string ProfileImage { get; set; }
    public string BackgroundImage { get; set; }
    public bool IsDelete { get; set; }
}
