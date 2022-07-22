using PoroDev.Common.Models.SharedSpaces;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface ISharedSpacesWithFilesRepository : IGenericRepository<SharedSpacesFiles>
    {
        Task<List<SharedSpacesFiles>> QueryFilesBySpaceId(Guid spaceId);
    }
}
