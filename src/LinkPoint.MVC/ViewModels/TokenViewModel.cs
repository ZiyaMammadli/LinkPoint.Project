﻿namespace LinkPoint.MVC.ViewModels;

public class TokenViewModel
{
    public string AccesToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }

}
