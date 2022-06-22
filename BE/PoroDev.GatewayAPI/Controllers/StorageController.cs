using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Models.StorageModels.UploadFile;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost("Upload")]
        public async Task<ActionResult<FileUploadRequestGatewayToService>> Upload(IFormFile file, [FromForm] Guid UserId)
        {
            var returnModel = new FileUploadRequestGatewayToService(file, UserId);
            await _storageService.UploadFile(returnModel);
            return Ok();
        }
    }
}
