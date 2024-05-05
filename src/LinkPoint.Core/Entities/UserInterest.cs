namespace LinkPoint.Core.Entities;

public class UserInterest:BaseEntity
{
    public string UserId { get; set; }
    public string Interest {  get; set; }
    public AppUser User { get; set; }
}
