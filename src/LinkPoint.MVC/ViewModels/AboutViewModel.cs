namespace LinkPoint.MVC.ViewModels;

public class AboutViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public UserInfoViewModel AuthUserInfo { get; set; }
    public UserAboutGetViewModel UserAbout { get; set; }
    public UserWorkGetViewModel UserWork { get; set; }
    public UserEducationGetViewModel UserEducation { get; set; }
    public List<UserInterestGetViewModel> UserInterests { get; set; }
    public List<AcceptedFollowerUsersGetViewModel> AcceptedFollowerUsers { get; set; }

}
