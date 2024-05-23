namespace LinkPoint.Business.DTOs.FriendShipDTOs;

public class PendingFollowerUserDto
{
    public int FriendShipId { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string? ProfileImageUrl { get; set; }
}
