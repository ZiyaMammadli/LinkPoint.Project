using Microsoft.AspNetCore.Identity;
namespace LinkPoint.Core.Entities;

public class AppUser:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsDeleted { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenEndDate { get; set; }
    public DateTime CreatedDate { get; set; }   
    public List<Image>? Images { get; set; }
    public List<Post>? Posts { get; set; }   
    public List<Comment>? comments { get; set; }   
    public List<Like>? likes { get; set; }   
    public UserAbout UserAbout { get; set; }
    public UserWork? UserWork { get; set; }
    public List<UserInterest>? UserInterests { get; set; }
    public UserEducation? UserEducation { get; set; }
    public List<FriendShip>? Friendships { get; set; }
    public List<FriendShip>? FollowingFriendships { get; set; }

}
