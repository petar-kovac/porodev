using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
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
            //Guid userId = Guid.Parse("f8cc4055-75a8-4573-8a7d-5f9d31778559");
            var returnModel = new FileUploadRequestGatewayToService(file, userId);
            var response = await _storageService.UploadFile(returnModel);
            return Ok(response);
        }

        [HttpPost("Download")]
        public async Task<ActionResult<FileDownloadMessage>> Download([FromBody] FileDownloadRequestGatewayToService downloadRequest)
        {
           // Guid userId = Guid.Parse("f8cc4055-75a8-4573-8a7d-5f9d31778559");
            downloadRequest.UserId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);       

            FileDownloadMessage file = await _storageService.DownloadFile(downloadRequest);

            // GET api/storage/fileId
            // POST api/storage

            return Ok(file);
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] string fileId)
        {
            await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var returnModel = new FileDeleteRequestGatewayToService(fileId);

            await _storageService.DeleteFile(returnModel);

            return Ok();
        }
    }
}