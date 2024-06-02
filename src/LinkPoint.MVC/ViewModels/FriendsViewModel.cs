namespace LinkPoint.MVC.ViewModels;

public class FriendsViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<MyFriendsGetViewModel> MyFriends { get; set;}
    public List<DontFollowingUsersViewModel> DontFollowingUsers { get; set;}
}
