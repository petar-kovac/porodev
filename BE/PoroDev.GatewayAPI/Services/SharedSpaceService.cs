using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class SharedSpaceService : ISharedSpaceService
    {
        private readonly IRequestClient<CreateSharedSpaceRequestGatewayToService> _createSharedSpaceRequestClient;
        private readonly IRequestClient<AddFileToSharedSpaceGatewayToService> _addFileRequestClient;

        public SharedSpaceService(IRequestClient<CreateSharedSpaceRequestGatewayToService> createSharedSpaceRequestClient,
                                  IRequestClient<AddFileToSharedSpaceGatewayToService> addFileRequestClient)
        {
            _createSharedSpaceRequestClient = createSharedSpaceRequestClient;
            _addFileRequestClient = addFileRequestClient;
        }

        public async Task AddFile(AddFileToSharedSpaceGatewayToService requestModel)
        {
            var responseContext = await _addFileRequestClient.GetResponse<CommunicationModel<SharedSpacesFiles>>(requestModel);

            if (responseContext.Message.ExceptionName is not null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return;
        }

        public async Task<CommunicationModel<SharedSpace>> Create(CreateSharedSpaceRequestGatewayToService createModel)
        {
            var requestReturnContext = await _createSharedSpaceRequestClient.GetResponse<CommunicationModel<SharedSpace>>(createModel, CancellationToken.None, RequestTimeout.After(m: 5));

            if (requestReturnContext.Message.ExceptionName != null)
                ThrowException(requestReturnContext.Message.ExceptionName, requestReturnContext.Message.HumanReadableMessage);

            var returnModel = requestReturnContext.Message;
            return returnModel;
        }
    }
}
