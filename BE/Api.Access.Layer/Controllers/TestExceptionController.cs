using AutoMapper;
using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using Microsoft.AspNetCore.Mvc;

namespace Api.Access.Layer.Controllers
{
    [ApiController]
    public class TestExceptionController : BaseController
    {
        public TestExceptionController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet("CustomAppException")]
        public void CustomAppException()
        {
            throw new Helpers.GlobalExceptionHandler.AppException("Custom app exception");
        }

        [HttpGet("NotFoundException")]
        public void NotFoundException()
        {
            throw new KeyNotFoundException();
        }

        [HttpGet("BusinessLayerException")]
        public void BusinessLayerException()
        {
            TestGlobalException.TestException("Business layer Exception");
        }
    }
}