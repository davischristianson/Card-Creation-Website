using Azure.Storage.Blobs;

namespace Card_Creation_Website.Services
{
    public class AzureBlobService
    {
        private readonly string _connectionString;
        private readonly string _contrainerName;

        public AzureBlobService(string connectionString, string contrainerName)
        {
            _connectionString = connectionString;
            _contrainerName = contrainerName;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_contrainerName);

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }
    }
}
