namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;

public class AuthUserGetDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingsCount { get; set; }
    public int ProfileImageId { get; set; }
    public int BackgroundImageId { get; set; }
    public string ProfileImage { get; set; }
    public string BackgroundImage { get; set; }


}
