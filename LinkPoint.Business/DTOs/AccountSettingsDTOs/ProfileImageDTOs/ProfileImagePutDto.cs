using Microsoft.AspNetCore.Http;

namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.ProfileImageDTOs;

public class ProfileImagePutDto
{
    public int ImageId { get; set; }
    public IFormFile ProfileImage { get; set; }
}
