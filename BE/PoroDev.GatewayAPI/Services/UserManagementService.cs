using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRequestClient<UserCreateRequestGatewayToService> _createRequestClient;
        private readonly IRequestClient<UserDeleteRequestGatewayToService> _deleteRequestClient;
        private readonly IRequestClient<UserReadByEmailRequestGatewayToService> _readUserByEmailRequestClient;

        public UserManagementService(IRequestClient<UserCreateRequestGatewayToService> createRequestClient,
            IRequestClient<UserDeleteRequestGatewayToService> deleteRequestClient,
            IRequestClient<UserReadByEmailRequestGatewayToService> readUserByEmailRequestClient)
        {
            _createRequestClient = createRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _readUserByEmailRequestClient = readUserByEmailRequestClient;
        }

        public async Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createRequestClient.GetResponse<UserCreateResponseServiceToGateway>(createModel);

            if (requestReturnContext.Message.ExceptionName != null)
                ThrowException(requestReturnContext.Message.ExceptionName, requestReturnContext.Message.HumanReadableMessage);         

            var returnModel = requestReturnContext.Message.Entity;

            return returnModel;
        }

        public async Task<DeleteUserModel> DeleteUser(UserDeleteRequestGatewayToService deleteModel)
        {
            if(string.IsNullOrEmpty(deleteModel.Email.Trim()))
            {
                ThrowException(nameof(EmailFormatException), "Email can't be empty.");
            }    

            var requestReturnContext = await _deleteRequestClient.GetResponse<UserDeleteResponseServiceToGateway>(deleteModel);

            if (requestReturnContext.Message.ExceptionName != null)
                ThrowException(requestReturnContext.Message.ExceptionName, requestReturnContext.Message.HumanReadableMessage);

            return requestReturnContext.Message.Entity;
        }

        public async Task<DataUserModel> ReadUserByEmail(string email)
        {
            var readUserByEmail = new UserReadByEmailRequestGatewayToService()
            {
                Email = email
            };

            var requestResponseContext = await _readUserByEmailRequestClient.GetResponse<UserReadByEmailResponseServiceToGateway>(readUserByEmail);

            if (requestResponseContext.Message.ExceptionName != null)
                ThrowException(requestResponseContext.Message.ExceptionName, requestResponseContext.Message.HumanReadableMessage);

            var returnUser = requestResponseContext.Message.Entity;

            return returnUser;
        }
    }
}