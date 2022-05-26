using Api.Access.Layer.Helpers.GlobalExceptionHandler;
using Api.Access.Layer.Models.UserModels;
using AutoMapper;
using Business.Access.Layer.Models.UserModels;
using Business.Access.Layer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Access.Layer.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesErrorResponseType(typeof(AppException))]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] string email)
        {
            var result = await _userService.DeleteUser(email);
            return Ok(result);
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(AppException))]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel createReqBody)
        {
            var BLModel = _mapper.Map<UserCreateRequestModel>(createReqBody);
            var createdId = await _userService.CreateUser(BLModel);

            return Ok(createdId);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserCreateRequestModel model)
        {
            var result = await _userService.UpdateUser(model);
            return Ok(result);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await _userService.GetUserByMail(email);
            return Ok(user);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(AppException))]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel registerModel)
        {
            return Ok(await _userService.Register(registerModel));
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(AppException))]
        [HttpGet("login")]
        public async Task<IActionResult> LoginUser([FromQuery] UserLoginRequestModel loginModel)
        {
            return Ok(await _userService.Login(loginModel));
        }
    }
}