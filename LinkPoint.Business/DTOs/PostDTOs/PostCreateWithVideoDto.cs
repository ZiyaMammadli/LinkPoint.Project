using Microsoft.AspNetCore.Http;

namespace LinkPoint.Business.DTOs.PostDTOs;

public class PostCreateWithVideoDto
{
    public string? Text { get; set; }
    public IFormFile PostVideoFile { get; set; }
}
