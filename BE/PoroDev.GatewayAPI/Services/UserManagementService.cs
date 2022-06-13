using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;

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
                throw new Exception(requestReturnContext.Message.HumanReadableMessage);

            var returnModel = requestReturnContext.Message.Entity;

            return returnModel;
        }

        public async Task DeleteUser(UserDeleteRequestGatewayToService deleteModel)
        {
            await _deleteRequestClient.GetResponse<UserDeleteResponseServiceToGateway>(deleteModel);
        }

        public async Task<DataUserModel> ReadUserByEmail(string email)
        {
            var readUserByEmail = new UserReadByEmailRequestGatewayToService()
            {
                Email = email
            };
            var requestResponseContext = await _readUserByEmailRequestClient.GetResponse<UserReadByEmailResponseServiceToGateway>(readUserByEmail);

            if (requestResponseContext.Message.ExceptionName != null) 
                throw new Exception("Error");

            var returnUser = requestResponseContext.Message.Entity;

            return returnUser;
        }
    }
}