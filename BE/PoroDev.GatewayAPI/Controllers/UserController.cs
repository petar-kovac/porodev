﻿using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using System.Net;

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




        //private readonly IUserService _userService;

        //public UserController(IUserService userService, IMapper mapper) : base(mapper)
        //{
        //    _userService = userService;
        //}

        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesErrorResponseType(typeof(UserNotFoundException))]
        //[HttpDelete("delete")]
        //public async Task<IActionResult> DeleteUser([FromQuery] string email)
        //{
        //    var result = await _userService.DeleteUser(email);
        //    return Ok(result);
        //}

        //[ProducesResponseType((int)HttpStatusCode.Created)]
        //[ProducesErrorResponseType(typeof(AppException))]
        //[HttpPost("create")]
        //public async Task<IActionResult> CreateUser([FromBody] UserCreateRequestModel createReqBody)
        //{
        //    /*var createdId =*/ await _userService.CreateUser(createReqBody);
        //    return Ok(/*createdId*/);
        //}

        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[HttpPut("Update")]
        //public async Task<IActionResult> UpdateUser([FromBody] UserCreateRequestModel model)
        //{
        //    var result = await _userService.UpdateUser(model);
        //    return Ok(result);
        //}

        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[HttpGet("GetUserByEmail")]
        //public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        //{
        //    var user = await _userService.GetUserByMail(email);
        //    return Ok(user);
        //}

        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesErrorResponseType(typeof(AppException))]
        //[HttpPost("register/user")]
        //public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel registerModel)
        //{
        //    return Ok(await _userService.Register(registerModel, Enums.UserRole.User));
        //}

        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesErrorResponseType(typeof(AppException))]
        //[HttpPost("register/admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterRequestModel registerModel)
        //{
        //    var modelReturn = await _userService.Register(registerModel, Enums.UserRole.SuperAdmin);
        //    return Ok(modelReturn);
        //}

        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesErrorResponseType(typeof(AppException))]
        //[HttpPost("login")]
        //public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestModel loginModel)
        //{
        //    return Ok(await _userService.Login(loginModel));
        //}
    }
}