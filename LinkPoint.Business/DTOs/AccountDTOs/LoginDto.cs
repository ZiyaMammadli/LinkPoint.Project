using System.ComponentModel;

namespace LinkPoint.Business.DTOs.AccountDTOs;

public class LoginDto
{
    [DefaultValue("ziya__memmedli")]
    public string UserName { get; set; }
    [DefaultValue("Salam1234@")]
    public string Password { get; set; }
}
