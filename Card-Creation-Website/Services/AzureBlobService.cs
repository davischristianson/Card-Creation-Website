using Azure.Storage.Blobs;

namespace Card_Creation_Website.Services
{
    // Great youtube video the help explain azure blob storage better
    // https://www.youtube.com/watch?v=GhlMa3jx_XA
    // https://www.youtube.com/watch?v=DzQ7CNnb9yM
    public class AzureBlobService
    {
        private readonly BlobServiceClient _azureBlobService;

        public AzureBlobService(BlobServiceClient azureBlobService)
        {
            _azureBlobService = azureBlobService;
        }

        public async Task<string> UploadFileAsync(string containerName, IFormFile file)
        {
            var containerClient = _azureBlobService.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return blobClient.Uri.ToString();
        }


        public async Task DownloadFileAsync(string containerName, string blobName, string downloadFilePath)
        {
            var containerClient = _azureBlobService.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DownloadToAsync(downloadFilePath);
        }

        public async Task DeleteFileAsync(string containerName, string blobName)
        {
            var containerClient = _azureBlobService.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
