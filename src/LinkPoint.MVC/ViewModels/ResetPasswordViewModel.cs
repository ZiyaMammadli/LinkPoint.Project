﻿using System.ComponentModel.DataAnnotations;

namespace LinkPoint.MVC.ViewModels;

public class ResetPasswordViewModel
{
    public string Email { get; set; }
    public string Token { get; set; }
    [DataType(DataType.Password)]
    [Compare("NewConfirmPassword")]
    public string NewPassword { get; set; }
    [DataType(DataType.Password)]
    public string NewConfirmPassword { get; set; }
}
