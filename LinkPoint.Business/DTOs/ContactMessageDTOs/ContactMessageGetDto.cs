using LinkPoint.Core.Enums;

namespace LinkPoint.Business.DTOs.ContactMessageDTOs;

public class ContactMessageGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Status { get; set; }
}
