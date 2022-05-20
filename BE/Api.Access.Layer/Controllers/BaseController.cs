using Microsoft.AspNetCore.Mvc;

namespace Api.Access.Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController()
        {
        }
    }
}
