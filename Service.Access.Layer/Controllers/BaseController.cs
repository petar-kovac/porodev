using Microsoft.AspNetCore.Mvc;

namespace Service.Access.Layer.Controllers
{
    [Route("api/[ontroller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController()
        {
        }
    }
}
