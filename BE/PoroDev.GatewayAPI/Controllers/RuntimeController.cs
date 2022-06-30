using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PoroDev.Common.Contracts.RunTime.ParametersExecute;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Models.Runtime;
using PoroDev.GatewayAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuntimeController : ControllerBase
    {
        private readonly IRunTimeService _runTimeService;
        private readonly IMapper _mapper;
        private readonly IJwtValidatorService _jwtValidatorService;

        public RuntimeController(IRunTimeService runTimeService, IMapper mapper, IJwtValidatorService jwtValidatorService)
        {
            _mapper = mapper;
            _runTimeService = runTimeService;
            _jwtValidatorService = jwtValidatorService;
        }

        [HttpPost("ExecuteProject")]
        public async Task<ActionResult<RuntimeData>> Execute([FromBody] ArgumentListRuntime model)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var returnModel = await _runTimeService.ExecuteProgram(new ArgumentListWithUserId(userId, model));

            return Ok(returnModel);
        }

       
    }
}