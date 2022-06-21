using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Models.StorageModels.UploadFile;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : BaseController
    {
        private readonly IStorageService _service;
        public StorageController(IStorageService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult> Upload(IFormFile file, [FromForm] Guid UserId)
        {
            var returnModel = new FileUploadModel(file, UserId);
           // _service.Upload(returnModel);
           // await _service.Download();
            return Ok();
        }
    }
}
