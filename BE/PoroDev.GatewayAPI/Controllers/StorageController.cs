using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly IJwtValidatorService _jwtValidatorService;
        public StorageController(IStorageService storageService, IJwtValidatorService jwtValidatorService)
        {
            _storageService = storageService;
            _jwtValidatorService = jwtValidatorService;
        }

        [HttpPost("Upload")]
        public async Task<ActionResult<FileUploadRequestGatewayToService>> Upload(IFormFile file)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var returnModel = new FileUploadRequestGatewayToService(file, userId);
            var response = await _storageService.UploadFile(returnModel);
            return Ok(response);
        }

        [HttpPost("Download")]
        public async Task<ActionResult<FileDownloadRequestGatewayToService>> Download(string fileId)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var returnModel = new FileDownloadRequestGatewayToService(fileId, userId);
            var response = await _storageService.DownloadFile(returnModel);

            return Ok(response);
        }
    }
}