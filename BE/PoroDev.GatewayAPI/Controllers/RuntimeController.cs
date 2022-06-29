using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.RunTime;
using PoroDev.Common.Contracts.RunTime.ParametersExecute;
using Microsoft.IdentityModel.Tokens;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static PoroDev.Common.Constants.Constants;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using PoroDev.Common.Exceptions;

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
            string jwtFromHeader = Request.Headers["authorization"];
            string accessTokenWithoutBearerPrefix = jwtFromHeader.Substring("Bearer ".Length);

            if (model.Arguments.Count == 0)
            {
                var modelWithJWT = new ExecuteProjectRequestClientToGateway(accessTokenWithoutBearerPrefix, model.ProjectId);

                var returnModel = await _runTimeService.ExecuteProgram(modelWithJWT);

                return Ok(returnModel);
            }
            else
            {
                var modelJwtArugments = new ArgumentListWithJwt(jwtFromHeader, model);

                var returnModel = await _runTimeService.ExecuteProgramWithArguments(modelJwtArugments);

                return Ok(returnModel);
            }
          
        }
    }
}
