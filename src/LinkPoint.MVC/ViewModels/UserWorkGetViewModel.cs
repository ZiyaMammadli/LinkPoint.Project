namespace LinkPoint.MVC.ViewModels;

public class UserWorkGetViewModel
{
    public string UserId { get; set; }
    public int? FromDate { get; set; }
    public int? ToDate { get; set; }
    public string? Company { get; set; }
    public string? Description { get; set; }
    public string? Designation { get; set; }
}
