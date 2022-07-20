using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Contracts.SharedSpace.Create;
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

        public SharedSpaceService(IRequestClient<CreateSharedSpaceRequestServiceToDatabase> createSharedSpaceRequestClient,
                                IRequestClient<AddUserToSharedSpaceRequestServiceToDatabase> addUserToSharedSpaceRequestClient)
        {
            _createSharedSpaceRequestClient = createSharedSpaceRequestClient;
            _addUserToSharedSpaceRequestClient = addUserToSharedSpaceRequestClient;
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
    }
}
