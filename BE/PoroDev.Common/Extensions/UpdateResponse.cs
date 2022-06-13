﻿using PoroDev.Common.Contracts;

namespace PoroDev.Common.Extensions
{
    public static class UpdateResponse<ResponseModel, CreatedEntity> where ResponseModel : CommunicationModel<CreatedEntity>, new() where CreatedEntity : class, new()
    {
        public static ResponseModel UpdateResponseModel(CreatedEntity entity)
        {
            ResponseModel response = new ResponseModel()
            {
                Entity = entity,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return response;
        }

        public static ResponseModel UpdateResponseModel(string exceptionName, string humanReadableMessage)
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