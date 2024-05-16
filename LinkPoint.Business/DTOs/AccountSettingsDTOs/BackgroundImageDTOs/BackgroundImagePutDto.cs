using Microsoft.AspNetCore.Http;

namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.BackgroundImageDTOs;

public class BackgroundImagePutDto
{
    public int ImageId { get; set; }
    public IFormFile BackgroundImage { get; set; }
}
