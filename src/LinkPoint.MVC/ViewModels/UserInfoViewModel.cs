namespace LinkPoint.MVC.ViewModels
{
    public class UserInfoViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }
        public string ProfileImage { get; set; }
    }
}
