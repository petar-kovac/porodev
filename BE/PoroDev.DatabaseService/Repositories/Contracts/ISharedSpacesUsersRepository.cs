using PoroDev.Common.Models.SharedSpaces;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface ISharedSpacesUsersRepository : IGenericRepository<SharedSpacesUsers>
    {
        Task<List<SharedSpacesUsers>> GetSharedSpacesByUserId(Guid userId);

        Task<List<SharedSpacesUsers>> GetAllUsersBySharedSpaceId(Guid sharedSpaceId);
    }
}