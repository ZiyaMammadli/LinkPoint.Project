﻿namespace LinkPoint.MVC.ViewModels;

public class AlbumViewModel
{
    public string Token { get; set; }
    public UserInfoViewModel UserInfo { get; set; }
    public List<PostGetViewModel> Posts { get; set; }
}