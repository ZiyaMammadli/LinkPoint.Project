using System.ComponentModel.DataAnnotations;

namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.PasswordDTOs;

public class ChangePasswordDto
{
    public string UserId { get; set; }
    public string OldPassword { get; set; }
    [Compare("ConfirmNewPassword")]
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
