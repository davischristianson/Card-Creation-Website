using Card_Creation_Website.Services;
using EllipticCurve.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Card_Creation_Website.Controllers
{
    public class BlobController : Controller
    {
        private readonly AzureBlobService _azureBlobService;
        private readonly IConfiguration _configuration;

        public BlobController(AzureBlobService azureBlobService, IConfiguration configuration)
        {
            _azureBlobService = azureBlobService;
            _configuration = configuration;
        }

        public async Task<string> Upload(IFormFile file)
        {
            string imgUrl = "";
            var secret = _configuration["ContainerName"];

            if (file == null)
            {
                var imageUrl = await _azureBlobService.UploadFileAsync(secret, file);
                ViewBag.ImageUrl = imageUrl;
                imgUrl = imageUrl;
            }
            return imgUrl;
        }

        
        public IActionResult GetImageUrl(string blobName)
        {
            var secret = _configuration["ContainerName"];

            var imageUrl = _azureBlobService.GetBlobUrl(secret, blobName);
            // return Json(new { imageUrl });
            return Content(imageUrl);
        }


        public async Task<IActionResult> Delete(string blobName, string RTAName)
        {
            var secret = _configuration["ContainerName"];

            await _azureBlobService.DeleteFileAsync(secret, blobName);
            return RedirectToAction(RTAName);
        }
    }
}
