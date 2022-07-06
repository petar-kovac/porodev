﻿using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.DeleteAllUsers;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime;
using PoroDev.Common.Contracts.UserManagement.Update;
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
    }
}