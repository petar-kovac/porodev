using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.SharedSpaceService.Services.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using static PoroDev.SharedSpaceService.Constants.Constants;

namespace PoroDev.SharedSpaceService.Services
{
    public class SharedSpaceService : ISharedSpaceService
    {
        private readonly IRequestClient<CreateSharedSpaceRequestServiceToDatabase> _createSharedSpaceRequestClient;

        public SharedSpaceService(IRequestClient<CreateSharedSpaceRequestServiceToDatabase> createSharedSpaceRequestClient)
        {
            _createSharedSpaceRequestClient = createSharedSpaceRequestClient;
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
