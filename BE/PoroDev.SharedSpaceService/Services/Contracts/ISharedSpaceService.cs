using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
using PoroDev.Common.Models.SharedSpaces;

namespace PoroDev.SharedSpaceService.Services.Contracts
{
    public interface ISharedSpaceService
    {
        Task<CommunicationModel<SharedSpace>> Create(CreateSharedSpaceRequestGatewayToService createModel);

        Task<CommunicationModel<SharedSpacesFiles>> AddFile(AddFileToSharedSpaceGatewayToService requestModel);
    }
}
