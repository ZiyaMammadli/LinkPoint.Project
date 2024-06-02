namespace LinkPoint.MVC.ViewModels;

public class AlbumViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public UserInfoViewModel AuthUserInfo { get; set; }
    public List<PostGetViewModel> Posts { get; set; }
    public List<AcceptedFollowerUsersGetViewModel> AcceptedFollowerUsers { get; set; }
}
