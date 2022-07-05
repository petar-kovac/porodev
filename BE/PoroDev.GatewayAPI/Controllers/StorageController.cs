using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
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

        [HttpGet("Download")]
        public async Task<ActionResult<FileDownloadMessage>> Download(string fileId)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var returnModel = new FileDownloadRequestGatewayToService(fileId, userId);

            var file = await _storageService.DownloadFile(returnModel);

            // GET api/storage/fileId
            // POST api/storage

            return File(file.File, file.ContentType, file.FileName);
        }

        [HttpGet("Read")]
        public async Task<ActionResult<FileReadModel>> Read()
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            //Guid userId = new Guid("21566439-04ea-40f2-b649-924cd1bf410a");
            var returnModel = new FileReadRequestGatewayToService(userId);

            var response = await _storageService.ReadFiles(returnModel);

            return Ok(response);
        }
    }
}