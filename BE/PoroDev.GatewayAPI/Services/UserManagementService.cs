using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.HelpConstants;

namespace PoroDev.GatewayAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRequestClient<UserCreateRequestGatewayToService> _createRequestClient;
        private readonly IRequestClient<UserDeleteRequestGatewayToService> _deleteRequestClient;
        private readonly IRequestClient<UserUpdateRequestGatewayToService> _updateRequestClient;

        public UserManagementService(IRequestClient<UserCreateRequestGatewayToService> createRequestClient, IRequestClient<UserDeleteRequestGatewayToService> deleteRequestClient, IRequestClient<UserUpdateRequestGatewayToService> updateRequestClient)
        {
            _createRequestClient = createRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _updateRequestClient = updateRequestClient;
        }



        public async Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createRequestClient.GetResponse<UserCreateResponseServiceToGateway>(createModel);

            if (requestReturnContext.Message.ExceptionName != null)
                throw new Exception("Error");

            var returnModel = requestReturnContext.Message.Entity;

            return returnModel;
        }
     
        public async Task DeleteUser(UserDeleteRequestGatewayToService deleteModel)
        {
            await _deleteRequestClient.GetResponse<UserDeleteResponseServiceToGateway>(deleteModel);
        }

        public async Task<DataUserModel> UpdateUser(UserUpdateRequestGatewayToService updateModel)
        {
            var requestReturnContext = await _updateRequestClient.GetResponse<UserUpdateResponseServiceToGateway>(updateModel);

            if (string.IsNullOrEmpty(updateModel.Email.Trim()))
            {
                ThrowException(nameof(EmailFormatException), EmptyEmail);
            }
           

                if (requestReturnContext.Message.ExceptionName != null)
            {
                throw new Exception("Error");
            }

            var returnModel = requestReturnContext.Message.Entity;
            return returnModel;
        }
    }
}