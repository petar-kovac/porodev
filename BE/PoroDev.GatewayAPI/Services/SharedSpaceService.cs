using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class SharedSpaceService : ISharedSpaceService
    {
        private readonly IRequestClient<CreateSharedSpaceRequestGatewayToService> _createSharedSpaceRequestClient;
        private readonly IRequestClient<AddUserToSharedSpaceRequestGatewayToService> _addUserToSharedSpaceRequestGatewayToService;

        public SharedSpaceService(IRequestClient<CreateSharedSpaceRequestGatewayToService> createSharedSpaceRequestClient,
                                    IRequestClient<AddUserToSharedSpaceRequestGatewayToService> addUserToSharedSpaceRequestGatewayToService)
        {
            _createSharedSpaceRequestClient = createSharedSpaceRequestClient;
            _addUserToSharedSpaceRequestGatewayToService = addUserToSharedSpaceRequestGatewayToService;
        }

        public async Task<CommunicationModel<SharedSpacesUsers>> AddUserToSharedSpace(AddUserToSharedSpaceRequestGatewayToService addModel)
        {
            var requestReturnContext = await _addUserToSharedSpaceRequestGatewayToService.GetResponse<CommunicationModel<SharedSpacesUsers>>(addModel, CancellationToken.None, RequestTimeout.After(m: 5));
            return requestReturnContext.Message;
        }

        public async Task<CommunicationModel<SharedSpace>> Create(CreateSharedSpaceRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createSharedSpaceRequestClient.GetResponse<CommunicationModel<SharedSpace>>(createModel, CancellationToken.None, RequestTimeout.After(m: 5));
            return requestReturnContext.Message;
        }
    }
}
