﻿namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;

public class AuthUserGetDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingsCount { get; set; }
    public string ProfileImage { get; set; }

}