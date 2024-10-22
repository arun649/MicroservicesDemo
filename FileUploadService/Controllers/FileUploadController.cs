using FileUploadService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private readonly BlobStorageService _blobStorageService;

        public FileUploadController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var result = await _blobStorageService.UploadFileAsync(file.OpenReadStream(), file.FileName);
            return Ok(new { fileUrl = result });
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            var result = await _blobStorageService.DeleteFileAsync(fileName);
            if (!result)
                return NotFound("File not found");

            return Ok("File deleted successfully");
        }
    }
}
