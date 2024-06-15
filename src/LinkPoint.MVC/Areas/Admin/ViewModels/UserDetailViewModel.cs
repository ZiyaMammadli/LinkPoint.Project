using LinkPoint.MVC.ViewModels;

namespace LinkPoint.MVC.Areas.Admin.ViewModels;

public class UserDetailViewModel
{
    public string Token { get; set; }
    public GetUserViewModel UserInfo { get; set; }
    public UserInfoViewModel AuthUserInfo { get; set; }
    public UserAboutGetViewModel UserAbout { get; set; }
    public UserWorkGetViewModel UserWork { get; set; }
    public UserEducationGetViewModel UserEducation { get; set; }
    public List<UserInterestGetViewModel> UserInterests { get; set; }
    public List<ConversationGetViewModel> AllConversations { get; set; }
    public List<AcceptedFollowerUsersGetViewModel> AcceptedFollowerUsers { get; set; }
}
