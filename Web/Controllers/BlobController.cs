﻿using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly IBlobStorageService blobStorageService;
        public BlobController(IBlobStorageService blobStorageService)
        {
            this.blobStorageService = blobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            using var stream = file.OpenReadStream();
            await blobStorageService.UploadAsync(stream, file.FileName);

            return Ok("Blob uploaded successfully");
        }

        [HttpGet("download/{blobName}")]
        public async Task<IActionResult> Download(string blobName)
        {
            var stream = await blobStorageService.DownloadAsync(blobName);

            if (stream == null)
                return NotFound("Blob not found");

            var contentType = GetContentType(blobName);

            return File(stream, contentType, blobName);
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> Delete(string fileName)
        {
            await blobStorageService.DeleteAsync(fileName);
            return Ok(new { message = "File deleted successfully" });
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
