
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace MongoTestPro.Services.GCSServices
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;
        public CloudStorageService(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GCSAuthFile"));
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GetValue<string>("GCSBucketName");
        }
        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
        {
            var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0; // MemoryStream'in pozisyonunu sıfırla
            try
            {
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
            catch (Exception ex)
            {
                // Hata yönetimi: Hata mesajını döndürebilir veya kaydedebilirsiniz
                Console.WriteLine($"Hata: {ex.Message}");
                throw; // veya hata mesajını döndürebilirsiniz: return $"Hata: {ex.Message}";
            }
        }
        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
        }
    }
}


