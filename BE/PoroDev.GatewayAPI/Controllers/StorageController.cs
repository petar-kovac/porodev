﻿using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.GatewayAPI.Models.StorageService;
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

        [RequireHttps]
        [HttpPost("Upload")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<ActionResult<FileUploadResponse>> Upload(IFormFile file)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            FileUploadRequest fileUploadRequest = new(file, userId);

            FileUploadResponse fileUploadResponse = await _storageService.UploadFile(fileUploadRequest);

            return Ok(fileUploadResponse);
        }

        [RequireHttps]
        [HttpGet("Download")]
        public async Task<ActionResult<FileDownloadResponse>> Download([FromQuery] string fileId)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
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