using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
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
        private readonly IRequestClient<AddFileToSharedSpaceGatewayToService> _addFileRequestClient;
        private readonly IRequestClient<QueryFilesGatewayToService> _queryFilesClient;

        public SharedSpaceService(IRequestClient<CreateSharedSpaceRequestGatewayToService> createSharedSpaceRequestClient,
                                  IRequestClient<AddFileToSharedSpaceGatewayToService> addFileRequestClient,
                                  IRequestClient<AddUserToSharedSpaceRequestGatewayToService> addUserToSharedSpaceRequestGatewayToService,
                                  IRequestClient<QueryFilesGatewayToService> queryFilesClient)
        {
            _createSharedSpaceRequestClient = createSharedSpaceRequestClient;
            _addUserToSharedSpaceRequestGatewayToService = addUserToSharedSpaceRequestGatewayToService;
            _addFileRequestClient = addFileRequestClient;
            _queryFilesClient = queryFilesClient;

        }

        public async Task<CommunicationModel<SharedSpacesUsers>> AddUserToSharedSpace(AddUserToSharedSpaceRequestGatewayToService addModel)
        {
            var requestReturnContext = await _addUserToSharedSpaceRequestGatewayToService.GetResponse<CommunicationModel<SharedSpacesUsers>>(addModel, CancellationToken.None, RequestTimeout.After(m: 5));
            return requestReturnContext.Message;  
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
            return requestReturnContext.Message;
        }

        public async Task<List<QueryFilesResponse>> QueryFiles(QueryFilesGatewayToService requestModel)
        {
            var responseContext = await _queryFilesClient.GetResponse<CommunicationModel<List<QueryFilesResponse>>>(requestModel);

            if (responseContext.Message.ExceptionName is not null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return responseContext.Message.Entity;
        }
    }
}
