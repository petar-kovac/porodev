using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.GatewayAPI.Services.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedSpaceController : ControllerBase
    {
        private readonly IJwtValidatorService _jwtValidatorService;
        private readonly ISharedSpaceService _sharedSpaceService;

        public SharedSpaceController(IJwtValidatorService jwtValidatorService, ISharedSpaceService sharedSpaceService)
        {
            _jwtValidatorService = jwtValidatorService;
            _sharedSpaceService = sharedSpaceService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<SharedSpace>> Create([FromQuery] string name)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var response = await _sharedSpaceService.Create(new CreateSharedSpaceRequestGatewayToService { Name = name, OwnerId = userId });

            if (response.ExceptionName != null)
                ThrowException(nameof(response.ExceptionName), response.HumanReadableMessage);

            return Ok(response);
        }
    }
}
