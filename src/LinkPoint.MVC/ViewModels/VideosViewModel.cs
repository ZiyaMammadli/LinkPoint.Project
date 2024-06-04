namespace LinkPoint.MVC.ViewModels;

public class VideosViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<LikeGetAllViewModel> LikeList { get; set; }
    public List<PostGetViewModel> OnlyVideoPosts { get; set; }
    public List<DontFollowingUsersViewModel> DontFollowingUsers { get; set; }
    public List<AcceptedFollowingUsersGetViewModel> AcceptedFollowingUsers { get; set; }
}
