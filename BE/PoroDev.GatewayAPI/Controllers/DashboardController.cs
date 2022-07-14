using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers;
using PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;
        private readonly IJwtValidatorService _jwtValidatorService;

        public DashboardController(IDashBoardService dashBoardService, IJwtValidatorService jwtValidatorService)
        {
            _dashBoardService = dashBoardService;
            _jwtValidatorService = jwtValidatorService;
        }

        [HttpGet("TotalRunTimeForAllUsers")]
        public async Task<ActionResult<TotalRunTimeForAllUsersModel>> GetTotalRunTimeForAllUsers()
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            return Ok();
        }

        [HttpGet("TotalNumberOfUploadedFiles")]
        public async Task<ActionResult<TotalNumberOfUploadedFilesModel>> GetTotalNumberOfUploadedFiles()
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var returnModel = new TotalNumberOfUploadedFilesRequestGatewayToService(userId);
            var response = await _dashBoardService.GetTotalNumberOfUploadedFiles(returnModel);

            return Ok(response);
        }

        [HttpGet("TotalNumberOfDeletedFiles")]
        public async Task<ActionResult<TotalNumberOfDeletedFilesModel>> GetTotalNumberOfDeletedFiles()
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var returnModel = new TotalNumberOfDeletedFilesRequestGatewayToService(userId);

            var response = await _dashBoardService.GetTotalNumberOfDeletedFiles(returnModel);

            return Ok(response);
        }

        [HttpGet("TotalNumberOfUsers")]
        public async Task<ActionResult<TotalNumberOfUsersModel>> GetTotalNumberOfUsers()
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var returnModel = new TotalNumberOfUsersRequestGatewayToService(userId);

            var response = await _dashBoardService.GetTotalNumberOfUsers(returnModel);
           
            return Ok(response);
        }

        [HttpGet("TotalRunTimePerMonth")]
        [HttpGet("TotalMemoryLimitUsedForDownloadPerMonth")]
        [HttpGet("TotalMemoryLimitUsedForUploadPerMonth")]
    }
}
