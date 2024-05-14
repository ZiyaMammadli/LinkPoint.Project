using Microsoft.AspNetCore.Http;

namespace LinkPoint.Business.DTOs.PostDTOs;

public class PostCreateWithImageDto
{
    public string? Text { get; set; }
    public IFormFile PostImageFile { get; set; }
}
