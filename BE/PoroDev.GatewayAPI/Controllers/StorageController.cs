using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Exceptions;
using PoroDev.GatewayAPI.Models.StorageService;
using PoroDev.GatewayAPI.Services.Contracts;
using System.Net;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly IJwtValidatorService _jwtValidatorService;
        private readonly ILimitValidatorService _limitValidatorService;

        public StorageController(IStorageService storageService, IJwtValidatorService jwtValidatorService, ILimitValidatorService limitValidatorService)
        {
            _storageService = storageService;
            _jwtValidatorService = jwtValidatorService;
            _limitValidatorService = limitValidatorService;
        }

        [RequireHttps]
        [HttpPost("Upload")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<ActionResult<FileUploadResponse>> Upload(IFormFile file)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            await _limitValidatorService.ValidateUpload(userId, file.Length);

            FileUploadRequest fileUploadRequest = new(file, userId);

            FileUploadResponse fileUploadResponse = await _storageService.UploadFile(fileUploadRequest);

            return Ok(fileUploadResponse);
        }

        [HttpPost("ChangeFileExecutable")]
        public async Task<IActionResult> ChangeFile([FromBody]FileExeReq request)
        {
            request.UserId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            await _storageService.ChangeFileEx(request);

            return Ok();
        } 

        [RequireHttps]
        [HttpGet("Download")]
        public async Task<ActionResult<FileDownloadResponse>> Download([FromQuery] string fileId)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            await _limitValidatorService.ValidateDownload(userId, fileId);

            var returnModel = new FileDownloadRequestGatewayToService(fileId, userId);

            var file = await _storageService.DownloadFile(returnModel);

            return File(file.File, file.ContentType, file.FileName);
        }

        [HttpGet("Read")]
        public async Task<ActionResult<FileReadModel>> Read()
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
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