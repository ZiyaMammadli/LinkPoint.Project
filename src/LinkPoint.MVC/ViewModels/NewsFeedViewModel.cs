namespace LinkPoint.MVC.ViewModels;

public class NewsFeedViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<PostGetViewModel> Posts { get; set; }
    public List<LikeGetAllViewModel> LikeList { get; set; }
}
