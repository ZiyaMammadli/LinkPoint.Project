namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.UserDTOs;

public class PaginatedUsersDto
{
    public List<UserGetDto> Users { get; set; }
    public int TotalUsers { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
