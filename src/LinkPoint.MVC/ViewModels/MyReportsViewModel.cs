namespace LinkPoint.MVC.ViewModels;

public class MyReportsViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<ContactMessageGetViewModel> contactMessages { get; set; }
}
