using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using Data.Access.Layer.Helpers.GlobalExceptionHandler;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Service.Access.Layer.Helpers.GlobalExceptionHandler;

namespace Service.Access.Layer.Controllers
{

    [ApiController]
    public class TestExceptionController : BaseController
    {
        [HttpGet("CustomAppException")]
        public void CustomAppException()
        {
            throw new AppException("Custom app exception");

            
        }
        [HttpGet("NotFoundException")]
        public void NotFoundException()
        {
            throw new KeyNotFoundException("Key not found exception.");
        }

        [HttpGet("BusinessLayerException")]
        public void BusinessLayerException()
        {
            Business.Access.Layer.Helpers.GlobalExceptionHandler.TestGlobalException.TestException("Business layer Exception");
        }

        [HttpGet("DataLayerException")]
        public void DataLayerException()
        {
            Data.Access.Layer.Helpers.GlobalExceptionHandler.TestGlobalException.TestException("Data layer exception");
        }
    }
}


