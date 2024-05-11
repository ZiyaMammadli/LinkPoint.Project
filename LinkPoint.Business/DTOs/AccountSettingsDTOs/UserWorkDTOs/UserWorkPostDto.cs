namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.UserWorkDTOs;

public class UserWorkPostDto
{
    public string UserId { get; set; }
    public int? FromDate { get; set; }
    public int? ToDate { get; set; }
    public string? Company { get; set; }
    public string? Description { get; set; }
    public string? Designation { get; set; }
}
