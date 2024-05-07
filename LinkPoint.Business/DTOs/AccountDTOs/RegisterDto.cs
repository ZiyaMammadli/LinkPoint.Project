﻿namespace LinkPoint.Business.DTOs.AccountDTOs;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }   
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public bool Male { get; set; }
    public bool Female { get; set; }    
}