using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuntimeController : ControllerBase
    {
        private readonly IRunTimeService _runTimeService;
        public RuntimeController(IRunTimeService runTimeService)
        {
            _runTimeService = runTimeService;
        }

        [HttpPost("ExecuteProject")]
        public async Task<ActionResult<RuntimeData>> Execute([FromBody] ExecuteProjectRequestClientToGateway model)
        {
            var returnModel = await _runTimeService.ExecuteProgram(model);
            return Ok(returnModel);
        }
    }
}
