using LinkPoint.Core.Enums;

namespace LinkPoint.Business.DTOs.ContactMessageDTOs;

public class ContactMessageGetDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public string Message { get; set; }
    public ContactMessageStatus Status { get; set; }
}
