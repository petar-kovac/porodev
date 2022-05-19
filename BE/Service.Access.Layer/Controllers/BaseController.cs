using Microsoft.AspNetCore.Mvc;

namespace Service.Access.Layer.Controllers
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
