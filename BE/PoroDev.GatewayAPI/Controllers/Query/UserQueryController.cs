using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers.Query
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQueryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJwtValidatorService _jwtValidatorService;
        private readonly IUserManagementService _userManagementService;

        public UserQueryController(IJwtValidatorService jwtValidatorService, IMapper mapper, IUserManagementService userManagementService)
        {
            _jwtValidatorService = jwtValidatorService;
            _userManagementService = userManagementService;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<DataUserModel>>> GetAllUsers()
        {
            await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var userList = await _userManagementService.QueryAll();

            return userList;
        }
    }
}