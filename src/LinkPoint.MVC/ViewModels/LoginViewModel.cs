using System.ComponentModel.DataAnnotations;

namespace LinkPoint.MVC.ViewModels;

public class LoginViewModel
{
    [DataType(DataType.Text)]
    [StringLength(25)]
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
