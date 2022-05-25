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
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] string email)
        {
            var result = await _userService.Delete(email);

            if(result == null)
                return NotFound();

            return Ok(result);
            

        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        feature/updateUserOperation
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] BusinessUserModel model)
        {
            var result = await _userService.Update(model);
            return Ok(result);
        }


        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("read")]
        public async Task<IActionResult> Read([FromBody] string email)
        {
            var user = await _userService.GetByMail(email);
            return Ok(user);
        }
        dev

    }
}
