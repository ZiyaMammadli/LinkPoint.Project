using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using LinkPoint.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleCloudTestController : ControllerBase
    {
        //private readonly IConfiguration _configuration;

        //public GoogleCloudTestController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetImage(string ImageName)
        //{
        //    string apiKey = _configuration["GoogleCloud:ApiKey"];
        //    var credential=GoogleCredential.FromFile(apiKey);
        //    var client=StorageClient.Create(credential);
        //    var objectName = $"Images/{ImageName}";
        //    var stream=new MemoryStream();
        //    var obj = await client.DownloadObjectAsync("link_point", objectName, stream);
        //    stream.Position = 0;
        //    return File(stream,obj.ContentType,obj.Name);
        //}
        //[HttpPost]
        //public async Task<IActionResult>AddFileToCloud(IFormFile file)
        //{
        //    if (file is null || file.Length == 0) return BadRequest("file empty");
        //    string apiKey = _configuration["GoogleCloud:ApiKey"];
        //    var credential = GoogleCredential.FromFile(apiKey);
        //    var client = StorageClient.Create(credential);
        //    var fileUrl=string.Empty;
        //    using(var memoryStream=new MemoryStream())
        //    {
        //        await file.CopyToAsync(memoryStream);
        //        memoryStream.Position = 0;
        //        var objectName = $"Images/{Guid.NewGuid()}_{file.FileName}";
        //        var bucketName = "link_point";
        //        await client.UploadObjectAsync(bucketName,objectName,null,memoryStream);
        //        var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";
        //        fileUrl = url;
        //    }

        //    return Ok(fileUrl);
        //}
        //[HttpPost("[action]")]
        //public async Task<IActionResult> DeleteFile(string FileName)
        //{
        //    if (FileName is null || FileName.Length == 0) return BadRequest("file empty");
        //    string apiKey = _configuration["GoogleCloud:ApiKey"];
        //    var credential = GoogleCredential.FromFile(apiKey);
        //    var client = StorageClient.Create(credential);
        //    var objectName = $"Images/{FileName}";
        //    var bucketName = "link_point";
        //    await client.DeleteObjectAsync(bucketName, objectName, null);
        //    return Ok();
        //}
    }
}
