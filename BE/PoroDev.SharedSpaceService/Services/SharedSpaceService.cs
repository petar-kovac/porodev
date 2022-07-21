using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.SharedSpaceService.Services.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using static PoroDev.SharedSpaceService.Constants.Constants;

namespace PoroDev.SharedSpaceService.Services
{
    public class SharedSpaceService : ISharedSpaceService
    {
        private readonly IRequestClient<CreateSharedSpaceRequestServiceToDatabase> _createSharedSpaceRequestClient;
        private readonly IRequestClient<AddUserToSharedSpaceRequestServiceToDatabase> _addUserToSharedSpaceRequestClient;
        private readonly IRequestClient<AddFileToSharedSpaceServiceToDatabase> _addFileClient;
        private readonly IRequestClient<QueryFilesServiceToDatabase> _queryFilesClient;

        public SharedSpaceService(IRequestClient<CreateSharedSpaceRequestServiceToDatabase> createSharedSpaceRequestClient, 
            IRequestClient<AddFileToSharedSpaceServiceToDatabase> addFileClient,
            IRequestClient<AddUserToSharedSpaceRequestServiceToDatabase> addUserToSharedSpaceRequestClient,
            IRequestClient<QueryFilesServiceToDatabase> queryFilesClient)
        {
            _createSharedSpaceRequestClient = createSharedSpaceRequestClient;
            _addFileClient = addFileClient;
            _addUserToSharedSpaceRequestClient = addUserToSharedSpaceRequestClient;
            _queryFilesClient = queryFilesClient;
        }

        public async Task<CommunicationModel<SharedSpacesFiles>> AddFile(AddFileToSharedSpaceGatewayToService requestModel)
        {
            return (await _addFileClient.GetResponse<CommunicationModel<SharedSpacesFiles>>(new AddFileToSharedSpaceServiceToDatabase(requestModel.SharedSpaceId,
                                                                                                                                      requestModel.FileId,
                                                                                                                                      requestModel.UserId))).Message;     
        }

        public async Task<CommunicationModel<SharedSpacesUsers>> AddUserToSharedSpace(AddUserToSharedSpaceRequestGatewayToService addModel)
        {
            var response = await _addUserToSharedSpaceRequestClient.GetResponse<CommunicationModel<SharedSpacesUsers>>(addModel);
            return response.Message;
        }

        public async Task<CommunicationModel<SharedSpace>> Create(CreateSharedSpaceRequestGatewayToService createModel)
        {
            if (String.IsNullOrEmpty(createModel.Name.Trim()))
            {
                var responseException = CreateResponseModel<CommunicationModel<SharedSpace>, SharedSpace>(nameof(SharedSpaceNameFormatException), SharedSpaceNameFormatExceptionMessage);
                return responseException;
            }
            var response = await _createSharedSpaceRequestClient.GetResponse<CommunicationModel<SharedSpace>>(createModel); //createModel flag

            return response.Message;
        }

        public async Task<CommunicationModel<List<QueryFilesResponse>>> QueryFiles(QueryFilesServiceToDatabase requestModel)
        {
            return (await _queryFilesClient.GetResponse<CommunicationModel<List<QueryFilesResponse>>>(requestModel)).Message;
        }
    }
}
