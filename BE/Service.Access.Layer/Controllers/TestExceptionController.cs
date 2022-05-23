using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Service.Access.Layer.Helpers.GlobalExceptionHandler;

namespace Service.Access.Layer.Controllers
{

    [ApiController]
    public class TestExceptionController : BaseController
    {
        
        [HttpGet("CustomAppException")]
        //[ProducesResponseType(typeof(Business.Access.Layer.Helpers.GlobalExceptionHandler.AppException), StatusCodes.Status200OK)]
        //[ProducesErrorResponseType(typeof(ApiError))]
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


