using LinkPoint.Core.Enums;

namespace LinkPoint.Core.Entities;

public class ContactMessage:BaseEntity
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }   
    public int PhoneNumber { get; set; }
    public string Message { get; set; }
    public ContactMessageStatus Status { get; set; }
    public AppUser User { get; set; }
}
