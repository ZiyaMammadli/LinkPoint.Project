
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace LinkPoint.Business.Utilities.Extentions;

public static class FileManager
{
    public static string SaveFile(this IFormFile File, string ApiKey,string Folder)
    {
        var credential = GoogleCredential.FromFile(ApiKey);
        var client = StorageClient.Create(credential);
        var fileUrl = string.Empty;
        using (var memoryStream = new MemoryStream())
        {
            File.CopyTo(memoryStream);
            memoryStream.Position = 0;
            var objectName = $"{Folder}/{Guid.NewGuid()}_{File.FileName}";
            var bucketName = "link_point";
            client.UploadObject(bucketName, objectName, null, memoryStream);
            var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";
            fileUrl = url;
        }
        return fileUrl;
    }
    public static async Task DeleteFile(string FileName, string ApiKey)
    {
        var credential = GoogleCredential.FromFile(ApiKey);
        var client = StorageClient.Create(credential);
        var objectName = $"Images/{FileName}";
        var bucketName = "link_point";
        await client.DeleteObjectAsync(bucketName, objectName, null);
    }
    public static IFormFile CreateIFormFile(string DefaultImagePath,string DefaultImageName)
    {
        byte[] imageData = null;
        using (var stream = new FileStream(DefaultImagePath, FileMode.Open))
        {
            using (var binaryReader = new BinaryReader(stream))
            {
                imageData = binaryReader.ReadBytes((int)stream.Length);
            }
        }
        IFormFile defaultImageFile = new FormFile(new MemoryStream(imageData), 0, imageData.Length, DefaultImageName, DefaultImageName);
        return defaultImageFile;
    }
}
