using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.GatewayAPI.Services.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using PoroDev.GatewayAPI.Models.SharedSpace;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.Common.Contracts.SharedSpace.GetAllUsers;

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
        public async Task<ActionResult<SharedSpace>> Create([FromBody] CreateSharedSpaceRequestModel model)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var response = await _sharedSpaceService.Create(new CreateSharedSpaceRequestGatewayToService { Name = model.Name, OwnerId = userId });

            if (response.ExceptionName != null)
                ThrowException(nameof(response.ExceptionName), response.HumanReadableMessage);

            return Ok(response.Entity);
        }

        [HttpPost("AddUserToSharedSpace")]
        public async Task<ActionResult<SharedSpacesUsers>> AddUserToSharedSpace([FromBody] AddUserToSharedSpaceRequestGatewayToService model)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var response = await _sharedSpaceService.AddUserToSharedSpace(model);

            if (response.ExceptionName != null)
                ThrowException(nameof(response.ExceptionName), response.HumanReadableMessage);

            return Ok(response.Entity);

        }

        [HttpGet("GetAllUsersInSharedSpace")]
        public async Task<ActionResult<List<DataUserModel>>> GetAllUsersInSharedspace([FromQuery] GetAllUsersFromSharedSpaceRequestGatewayToService model)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var response = await _sharedSpaceService.GetAllUsersFromSharedSpace(model);

            if (response.ExceptionName != null)
                ThrowException(nameof(response.ExceptionName), response.HumanReadableMessage);
            return Ok(response.Entity);
        }

        [HttpPost("AddFile")]
        public async Task<IActionResult> AddFile([FromBody] AddFileToSharedSpaceRequest requestModel)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var requestWithId = new AddFileToSharedSpaceGatewayToService(requestModel.SharedSpaceId, requestModel.FileId, userId);

            await _sharedSpaceService.AddFile(requestWithId);

            return Ok();
        }

        [HttpGet("GetAllFiles")]
        public async Task<ActionResult<List<QueryFilesResponse>>> GetAllFiles([FromQuery] string spaceId)
        {
            await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var spaceIdGuid = Guid.Parse(spaceId);

            return Ok();
        }
    }
}
