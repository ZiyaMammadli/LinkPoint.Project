using System.Globalization;

namespace LinkPoint.Business.DTOs.AccountDTOs;

public class ForgotPasswordDto
{
    public string callbackUrl {  get; set; }   
    public string Email { get; set; }
}
