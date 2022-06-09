using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRequestClient<UserCreateRequestGatewayToService> _createRequestClient;

        public UserManagementService(IRequestClient<UserCreateRequestGatewayToService> createRequestClient)
        {
            _createRequestClient = createRequestClient;
        }

        public async Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createRequestClient.GetResponse<UserCreateResponseServiceToGateway>(createModel);

            if (requestReturnContext.Message.ExceptionName != null)
                throw new Exception("Error");

            var returnModel = requestReturnContext.Message.Entity;

            return returnModel;

        }
    }
}
