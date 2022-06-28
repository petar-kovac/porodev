﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        [HttpPost("Download")]
        public async Task<ActionResult<FileDownloadRequestGatewayToService>> Download(ObjectId fileId)
        {
            var returnModel = new FileDownloadRequestGatewayToService(fileId);
            await _storageService.DownloadFile(returnModel);
            return Ok(returnModel);
        }
    }
}
