using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace;
using PoroDev.Common.Models.SharedSpaces;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface ISharedSpaceService
    {
        Task<CommunicationModel<SharedSpace>> Create(CreateSharedSpaceRequestGatewayToService createModel);

        Task AddFile(AddFileToSharedSpaceGatewayToService requestModel);
    }
}
