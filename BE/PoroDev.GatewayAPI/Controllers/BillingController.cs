using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.BillingReport.TotalDownload;
using PoroDev.Common.Contracts.BillingReport.TotalRuntime;
using PoroDev.Common.Contracts.BillingReport.TotalUpload;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingReportService _billingReportService;
        private readonly IJwtValidatorService _jwtValidatorService;

        public BillingController(IBillingReportService billingReportService, IJwtValidatorService jwtValidatorService)
        {
            _billingReportService = billingReportService;
            _jwtValidatorService = jwtValidatorService;
        }

        [HttpGet("TotalUpload")]
        public async Task<ActionResult> TotalUpload([FromQuery] Guid userId, string month)
        {
            Guid adminId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var returnModel = new TotalUploadRequestGatewayToService(adminId, userId, month);
            var uploadCount = await _billingReportService.TotalUpload(returnModel);

            return Ok(uploadCount);
        }

        [HttpGet("TotalDownload")]
        public async Task<ActionResult> TotalDownload([FromQuery] Guid userId, string month)
        {
            Guid adminId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var returnModel = new TotalDownloadRequestGatewayToService(adminId, userId, month);
            var downloadCount = await _billingReportService.TotalDownload(returnModel);

            return Ok(downloadCount);
        }

        [HttpGet("TotalRuntime")]
        public async Task<ActionResult> TotalRuntime([FromQuery] Guid userId, string month)
        {
            Guid adminId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var returnModel = new TotalRuntimeRequestGatewayToService(adminId, userId, month);
            var runtimeCount = await _billingReportService.TotalRuntime(returnModel);

            return Ok(runtimeCount);
        }
    }
}