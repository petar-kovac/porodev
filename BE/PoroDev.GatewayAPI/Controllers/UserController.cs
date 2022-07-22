using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.DeleteAllUsers;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.ReadAllSharedSpacesForUser;
using PoroDev.Common.Contracts.UserManagement.ReadAllUsers;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime;
using PoroDev.Common.Contracts.UserManagement.SetMonthlyReportTime;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Contracts.UserManagement.Verify;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Exceptions.Contract;
using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.Constats;

namespace PoroDev.GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userService;
        private readonly IJwtValidatorService _jwtValidatorService;
        private readonly IMapper _mapper;

        public UserController(IUserManagementService userService, IJwtValidatorService jwtValidatorService, IMapper mapper)
        {
            _userService = userService;
            _jwtValidatorService = jwtValidatorService;
            _mapper = mapper;
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

        [HttpDelete("DeleteAllUsers")]
        public async Task<ActionResult> DeleteAllUsers([FromBody] UserDeleteAllRequestGatewayToService model)
        {
            await _userService.DeleteAllUsers(model);
            return Ok();
        }

        [HttpGet("ReadUserByEmail")]
        public async Task<ActionResult<DataUserModel>> ReadUserByEmail([FromQuery] string email)
        {
            var returnModel = await _userService.ReadUserByEmail(email);
            return Ok(returnModel);
        }

        [HttpGet("ReadUserById")]
        public async Task<ActionResult<DataUserModel>> ReadUserById([FromQuery] UserReadByIdRequestGatewayToService model)
        {
            var returnModel = await _userService.ReadUserById(model);
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

        [HttpPost("Verify")]
        public async Task<ActionResult<DataUserModel>> Verify([FromQuery] VerifyEmailRequestGatewayToService tokenModel)
        {
            return Ok(await _userService.VerifyEmail(tokenModel));
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<LoginUserModel>> LoginUser([FromBody] UserLoginRequestGatewayToService model)
        {
            var returnModel = await _userService.LoginUser(model);
            return Ok(returnModel);
        }

        [HttpPost("ReadUserByIdWithRuntimeData")]
        public async Task<ActionResult<DataUserModel>> ReadUserByIdWithRuntimeData([FromBody] UserReadByIdWithRuntimeRequestGatewayToService model)
        {
            var returnModel = await _userService.ReadUserByIdWithRuntimeData(model);
            return Ok(returnModel);
        }

        [HttpGet("ReadAllUsers")]
        public async Task<ActionResult<List<DataUserModel>>> ReadAllUsers([FromQuery] ReadAllUsersRequestGatewayToService model)
        {
            var returnModel = await _userService.ReadAllUsers(model);
            return Ok(returnModel);
        }

        [HttpGet("ReadAllSharedSpacesForUser")]
        public async Task<ActionResult<List<SharedSpace>>> ReadAllSharedSpacesForUser([FromQuery] ReadAllSharedSpacesRequest model)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);
            var modelToPass = new ReadAllSharedSpacesForUserRequestGatewayToService() { UserId = userId };
            var returnModel = await _userService.ReadAllSharedSpacesForUser(modelToPass);
            return Ok(returnModel);
        }


        [HttpPut("SetMonthlyReportTime")]
        public async Task<ActionResult<NotificationDataModel>> SetMonthlyReportTime([FromQuery] SetMonthlyReportTimeRequest model)
        {
            Guid userId = await _jwtValidatorService.ValidateRecievedToken(Request.Headers["authorization"]);

            var modelToPass = _mapper.Map<SetMonthlyReportTimeRequestGatewayToService>(model);
            modelToPass.UserId = userId;

            var returnModel = await _userService.SetMonthlyReportTime(modelToPass);
            return Ok(returnModel);
        }

        
    }
}