namespace LinkPoint.Business.DTOs.AccountDTOs;

public class ResetPasswordDto
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
}
