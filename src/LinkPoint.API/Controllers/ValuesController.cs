using LinkPoint.Business.Utilities.Extentions;
using LinkPoint.Core.Entities;
using LinkPoint.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IImageRepository _imageRepository;

        public ValuesController(IConfiguration configuration,IImageRepository imageRepository)
        {
            _configuration = configuration;
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> SetUserDefalutImages(string UserId)
        {
            string apiKey = _configuration["GoogleCloud:ApiKey"];
            string DefautlImagePath = "C:\\Users\\user\\source\\repos\\LinkPoint\\src\\LinkPoint.API\\wwwroot\\UserDefaultProfileImage\\DefaultPerson.jpg";
            string DefautlBackgroundImagePath = "C:\\Users\\user\\source\\repos\\LinkPoint\\src\\LinkPoint.API\\wwwroot\\UserDefaultProfileImage\\DefaultBackgraoundImage.jpg";
            string DefaultImageName = "DefaultPerson.jpg";
            string DefaultBackgroundImageName = "DefaultBackgraoundImage.jpg";
            var DefaultProfileImage = FileManager.CreateIFormFile(DefautlImagePath, DefaultImageName);
            var DefaultBackgroundImage = FileManager.CreateIFormFile(DefautlBackgroundImagePath, DefaultBackgroundImageName);
            Image ProfileImage = new()
            {
                UserId = UserId,
                PostId = null,
                IsPostImage = false,
                ImageUrl = DefaultProfileImage.SaveFile(apiKey, "Images"),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };
            Image BackgroundImage = new()
            {
                UserId = UserId,
                PostId = null,
                IsPostImage = null,
                ImageUrl = DefaultBackgroundImage.SaveFile(apiKey, "Images"),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };
            await _imageRepository.InsertAsync(ProfileImage);
            await _imageRepository.InsertAsync(BackgroundImage);
            await _imageRepository.CommitAsync();
            return Ok();
        }
    }
}
