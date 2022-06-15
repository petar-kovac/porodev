using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.LoginUser;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        public UserController(IUserManagementService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<DataUserModel>> CreateUser([FromBody] UserCreateRequestGatewayToService model)
        {
            var returnModel = await _userService.CreateUser(model);
            return Ok(returnModel);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<DeleteUserModel>> DeleteUser([FromBody] UserDeleteRequestGatewayToService model)
        {
            var returnModel = await _userService.DeleteUser(model);
            return Ok(returnModel);
        }

        [HttpGet("ReadUserByEmail")]
        public async Task<ActionResult<DataUserModel>> ReadUserByEmail([FromQuery] string email)
        {
            var returnModel = await _userService.ReadUserByEmail(email);
            return Ok(returnModel);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<DataUserModel>> UpdateUser([FromBody] UserUpdateRequestGatewayToService model)
        {
            var returnModel = await _userService.UpdateUser(model);
            return Ok(returnModel);
        }


        [HttpPost("register/user")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser([FromBody] RegisterUserRequestGatewayToService registerModel)
        {
            return Ok(await _userService.RegisterUser(registerModel));
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<LoginUserModel>> LoginUser([FromBody] UserLoginRequestGatewayToService model)
        {
            var returnModel = await _userService.LoginUser(model);
            return Ok(returnModel);
        }
    }
}