using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Access.Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;

        protected BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}