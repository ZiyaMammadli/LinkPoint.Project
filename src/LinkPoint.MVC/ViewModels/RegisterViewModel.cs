using System.ComponentModel.DataAnnotations;

namespace LinkPoint.MVC.ViewModels;

public class RegisterViewModel
{
    [Required]
    [DataType(DataType.Text)]
    [StringLength(25)]
    public string FirstName { get; set; }//
    [Required]
    [DataType(DataType.Text)]
    [StringLength(25)]
    public string LastName { get; set; }//
    [Required]
    [DataType(DataType.Text)]
    [StringLength(25)]
    public string UserName { get; set; }//
    [Required]
    [DataType(DataType.EmailAddress)]
    [StringLength(100)]
    public string Email { get; set; }//
    [Required]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; }//
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }//
    public bool Male { get; set; }//
    public bool Female { get; set; }//
    public string? callbackUrl { get; set; }
}
