namespace LinkPoint.MVC.ViewModels;

public class UserEducationGetViewModel
{
    public int UserEducationId { get; set; }
    public string UserId { get; set; }
    public int? FromDate { get; set; }
    public int? ToDate { get; set; }
    public string? University { get; set; }
    public string? Description { get; set; }
    public bool Graduated { get; set; }
}
