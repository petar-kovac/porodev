using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions.Contract;

namespace PoroDev.Common.Extensions
{
    public static class CreateResponseExtension
    {
        public static ResponseModel CreateResponseModel<ResponseModel, CreatedEntity>(CreatedEntity entity) where ResponseModel : CommunicationModel<CreatedEntity>, new() where CreatedEntity : class, new()
        {
            ResponseModel response = new ResponseModel()
            {
                Entity = entity,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return response;
        }

        public static ResponseModel CreateResponseModel<ResponseModel, CreatedEntity>(string exceptionName, string humanReadableMessage) where ResponseModel : CommunicationModel<CreatedEntity>, new() where CreatedEntity : class, new()
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