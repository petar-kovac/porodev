using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> TotalUpload(Guid userId)
        {
            Guid adminId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var returnModel = new TotalUploadRequestGatewayToService(adminId, userId);

            return Ok(returnModel);
        }
    }
}