namespace LinkPoint.MVC.ViewModels;

public class MessagesViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<DontFollowingUsersViewModel> DontFollowingUsers { get; set; }
    public List<AcceptedFollowingUsersGetViewModel> AcceptedFollowingUsers { get; set; }
    public List<ConversationGetViewModel> AllConversations { get; set; }
}
