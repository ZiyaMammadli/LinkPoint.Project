namespace LinkPoint.MVC.ViewModels;

public class InterestsViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<UserInterestGetViewModel> UserInterests { get; set; }
}
