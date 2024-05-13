using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LinkPoint.Business.Utilities.Extentions;

public static class FileManager
{
    public static async Task<string> SaveFile(this IFormFile File,string ApiKey)
    {
        var credential = GoogleCredential.FromFile(ApiKey);
        var client = StorageClient.Create(credential);
        var fileUrl = string.Empty;
        using (var memoryStream = new MemoryStream())
        {
            await File.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var objectName = $"Images/{Guid.NewGuid()}_{File.FileName}";
            var bucketName = "link_point";
            await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
            var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";
            fileUrl = url;
        }
        return fileUrl;
    }
    public static async Task DeleteFile(string FileName,string ApiKey)
    {
        var credential = GoogleCredential.FromFile(ApiKey);
        var client = StorageClient.Create(credential);
        var objectName = $"Images/{FileName}";
        var bucketName = "link_point";
        await client.DeleteObjectAsync(bucketName, objectName, null);
    } 
}
