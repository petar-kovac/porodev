using PoroDev.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Extensions
{
    public static class CreateResponse<ResponseModel, CreatedEntity> where ResponseModel : CommunicationModel<CreatedEntity>, new() where CreatedEntity : class, new()
    {
        public static ResponseModel CreateResponseModel(CreatedEntity entity)
        {
            ResponseModel response = new ResponseModel()
            {
                Entity = entity,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return response;
        }

        public static ResponseModel CreateResponseModel(string exceptionName, string humanReadableMessage)
        {
            ResponseModel response = new ResponseModel()
            {
                Entity = null,
                ExceptionName = exceptionName,
                HumanReadableMessage = humanReadableMessage
            };

            return response;
        }
    }
}
