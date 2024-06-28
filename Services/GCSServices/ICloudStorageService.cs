using Microsoft.AspNetCore.Http;

namespace MongoTestPro.Services.GCSServices
{
    public interface ICloudStorageService
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);

    }
}
