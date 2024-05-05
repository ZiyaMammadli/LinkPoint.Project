using LinkPoint.Core.Enums;

namespace LinkPoint.Core.Entities;

public class FriendShip:BaseEntity
{
    public string UserId { get; set; }
    public string FollowingUserId { get; set; }
    public FollowStatus Status { get; set; }
    public AppUser User { get; set; }
    public AppUser FollowingUser { get; set; }
}
