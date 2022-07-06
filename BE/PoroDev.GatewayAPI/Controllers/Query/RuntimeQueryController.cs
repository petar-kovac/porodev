using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Models.Runtime;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers.Query
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuntimeQueryController : ControllerBase
    {
        private readonly IRuntimeQueryService _runTimeService;
        private readonly IMapper _mapper;
        private readonly IJwtValidatorService _jwtValidatorService;

        public RuntimeQueryController(IRuntimeQueryService runTimeService, IJwtValidatorService jwtValidatorService, IMapper mapper)
        {
            _runTimeService = runTimeService;
            _jwtValidatorService = jwtValidatorService;
            _mapper = mapper;
        }

        [HttpGet("Runtime")]
        public async Task<ActionResult<List<RuntimeData>>> QueryRuntimeAsync([FromQuery] RuntimeQueryRequest queryRequest)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var query = _mapper.Map<RuntimeQueryRequestGatewayToDatabase>(queryRequest);
            query.UserId = userId;

            var queryResult = await _runTimeService.Query(query);

            return Ok(queryResult);
        }
    }
}
