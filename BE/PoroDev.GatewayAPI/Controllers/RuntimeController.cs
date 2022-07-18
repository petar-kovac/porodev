using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Models.Runtime;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuntimeController : ControllerBase
    {
        private readonly IRunTimeService _runTimeService;
        private readonly IMapper _mapper;
        private readonly IJwtValidatorService _jwtValidatorService;
        private readonly ILimitValidatorService _limitValidatorService;

        public RuntimeController(IRunTimeService runTimeService, IMapper mapper, IJwtValidatorService jwtValidatorService, ILimitValidatorService limitValidatorService)
        {
            _mapper = mapper;
            _runTimeService = runTimeService;
            _jwtValidatorService = jwtValidatorService;
            _limitValidatorService = limitValidatorService;
        }

        [HttpPost("ExecuteProject")]
        public async Task<ActionResult<RuntimeData>> Execute([FromBody] ArgumentListRuntime model)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            await _limitValidatorService.ValidateRuntime(userId);

            var returnModel = await _runTimeService.ExecuteProgram(new ArgumentListWithUserId(userId, model));

            return Ok(returnModel);
        }
    }
}