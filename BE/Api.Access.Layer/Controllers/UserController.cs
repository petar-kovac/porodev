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


    }
}
