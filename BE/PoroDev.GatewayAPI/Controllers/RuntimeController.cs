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

        public RuntimeController(IRunTimeService runTimeService, IMapper mapper)
        {
            _mapper = mapper;
            _runTimeService = runTimeService;
        }

        [HttpPost("ExecuteProject")]
        public async Task<ActionResult<RuntimeData>> Execute([FromBody] ArgumentListRuntime model)
        {
            if (Request.Headers["authorization"].Count == 0)
                ThrowException(nameof(NoHeaderWithJwtException), "There is no JWT in request's header.");

            string jwtFromHeader = Request.Headers["authorization"];
            string accessTokenWithoutBearerPrefix = jwtFromHeader.Substring("Bearer ".Length);

            var modelJwtArugments = new ArgumentListWithJwt(accessTokenWithoutBearerPrefix, model);

            var returnModel = await _runTimeService.ExecuteProgram(modelJwtArugments);

            return Ok(returnModel);
        }

       
    }
}