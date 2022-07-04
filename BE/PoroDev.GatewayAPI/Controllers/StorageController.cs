using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.GatewayAPI.Services.Contracts;
using System.Net.Http.Headers;

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
        public async Task<ActionResult<FileDownloadRequestGatewayToService>> Download(string fileId)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var returnModel = new FileDownloadRequestGatewayToService(fileId, userId);
            var file = await _storageService.DownloadFile(returnModel);

            // GET api/storage/fileId
            // POST api/storage

            return File(file.File, "image/jpeg", "slika1.jpg");
        }

        [HttpGet("Read")]
        public async Task<ActionResult<FileReadRequestGatewayToService>> Read()
        {
            //Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            Guid userId = new Guid("2081a08d-d3a4-4a22-be97-0c940f28438b");
            var returnModel = new FileReadRequestGatewayToService(userId);

            var file = await _storageService.ReadFiles(returnModel);

            return returnModel;
        }
    }
}