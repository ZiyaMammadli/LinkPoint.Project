namespace LinkPoint.MVC.ViewModels;

public class ImagesViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<LikeGetAllViewModel> LikeList { get; set; }
    public List<PostGetViewModel> OnlyImagePosts { get; set; }
    public List<DontFollowingUsersViewModel> DontFollowingUsers { get; set; }
    public List<AcceptedFollowingUsersGetViewModel> AcceptedFollowingUsers { get; set; }
}
