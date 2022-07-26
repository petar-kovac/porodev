using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.GatewayAPI.Models.StorageService;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers.Query
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageQueryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJwtValidatorService _jwtValidatorService;
        private readonly IStorageService _storageService;

        public StorageQueryController(IJwtValidatorService jwtValidatorService, IMapper mapper, IStorageService storageService)
        {
            _jwtValidatorService = jwtValidatorService;
            _storageService = storageService;
            _mapper = mapper;
        }

        [HttpGet("files")]
        public async Task<ActionResult<List<FileQueryModel>>> QueryFiles([FromQuery] FileQueryRequest queryRequest)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var queryRequestWithId = _mapper.Map<FileQueryGatewayToService>(queryRequest);

            queryRequestWithId.UserId = userId;

            var response = await _storageService.QueryFiles(queryRequestWithId);

            return response;
        }
    }
}