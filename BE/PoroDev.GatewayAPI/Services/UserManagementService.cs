﻿using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRequestClient<UserCreateRequestGatewayToService> _createRequestClient;
        private readonly IRequestClient<UserDeleteRequestGatewayToService> _deleteRequestClient;

        public UserManagementService(IRequestClient<UserCreateRequestGatewayToService> createRequestClient, IRequestClient<UserDeleteRequestGatewayToService> deleteRequestClient)
        {
            _createRequestClient = createRequestClient;
            _deleteRequestClient = deleteRequestClient;
        }

        public async Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createRequestClient.GetResponse<UserCreateResponseServiceToGateway>(createModel);

            if (requestReturnContext.Message.ExceptionName != null)
                throw new Exception("Error");

            var returnModel = requestReturnContext.Message.Entity;

            return returnModel;
        }

        public async Task<DeleteUserModel> DeleteUser(UserDeleteRequestGatewayToService deleteModel)
        {
            var requestReturnContext = await _deleteRequestClient.GetResponse<UserDeleteResponseServiceToGateway>(deleteModel);
            if (requestReturnContext.Message.ExceptionName != null)
                throw new Exception("Error");

            return requestReturnContext.Message.Entity;
        }
    }
}
