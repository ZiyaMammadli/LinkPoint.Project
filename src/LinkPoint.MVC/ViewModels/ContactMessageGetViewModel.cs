namespace LinkPoint.MVC.ViewModels;

public class ContactMessageGetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Status { get; set; }
}
