using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface ISharedSpaceService
    {
        Task<CommunicationModel<SharedSpace>> Create(CreateSharedSpaceRequestGatewayToService createModel);

        Task<CommunicationModel<SharedSpacesUsers>> AddUserToSharedSpace(AddUserToSharedSpaceRequestGatewayToService addModel);

        Task AddFile (AddFileToSharedSpaceGatewayToService requestModel);

        Task<List<QueryFilesResponse>> QueryFiles(QueryFilesGatewayToService requestModel);
    }
}
