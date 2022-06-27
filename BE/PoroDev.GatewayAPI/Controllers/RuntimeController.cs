using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<RuntimeData>> Execute([FromBody] ExecuteProjectRequestClientToGatewayWithHeader model)
        {
            var requestExecute = _mapper.Map<ExecuteProjectRequestClientToGateway>(model);
            ValidateRecievedToken(requestExecute);
            var returnModel = await _runTimeService.ExecuteProgram(requestExecute);
            return Ok(returnModel);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public void ValidateRecievedToken(ExecuteProjectRequestClientToGateway requestExecute)
        {
            if (Request.Headers["Bearer"].Count == 0)
            {
                ThrowException(nameof(NoHeaderWithJwtException), "There is no JWT in request's header.");
            }

            requestExecute.Jwt = Request.Headers["Bearer"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            try
            {
                tokenHandler.ValidateToken(requestExecute.Jwt = Request.Headers["Bearer"].ToString(),
                                                                new TokenValidationParameters
                                                                {
                                                                    ValidateIssuerSigningKey = true,
                                                                    IssuerSigningKey = new SymmetricSecurityKey(key),
                                                                    ValidateIssuer = false,
                                                                    ValidateAudience = false,
                                                                    ClockSkew = TimeSpan.Zero
                                                                }, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                JWTValidationException jWTValidationException = new JWTValidationException()
                {
                    HumanReadableErrorMessage = ex.Message,
                };
                ThrowException(nameof(JWTValidationException), jWTValidationException.HumanReadableErrorMessage);
            }
        }
    }
}
