using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRequestClient<IUserCreateRequestGatewayToService> _createRequestClient;

        public UserManagementService(IRequestClient<IUserCreateRequestGatewayToService> createRequestClient)
        {
            _createRequestClient = createRequestClient;
        }

        public async Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createRequestClient.GetResponse<IUserCreateResponseServiceToGateway>(createModel);

            if (requestReturnContext.Message.ErrorName != null)
                throw new Exception("Error");

            var returnModel = requestReturnContext.Message.Entity;

            return returnModel;

        }
    }
}
