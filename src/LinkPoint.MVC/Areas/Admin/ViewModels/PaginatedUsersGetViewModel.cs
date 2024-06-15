using LinkPoint.MVC.ViewModels;

namespace LinkPoint.MVC.Areas.Admin.ViewModels;

public class PaginatedUsersGetViewModel
{
    public List<GetUserViewModel> Users { get; set; }
    public int TotalUsers { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
