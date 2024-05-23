using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LinkPoint.Business.DTOs.PostDTOs;

public class PostCreateWithImageDto
{
    public string UserId { get; set; }
    public string? Text { get; set; }
    [Required]
    public IFormFile PostImageFile { get; set; }
}
