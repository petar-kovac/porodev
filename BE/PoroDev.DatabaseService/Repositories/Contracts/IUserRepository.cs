using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IUserRepository : IGenericRepository<DataUserModel>
    {
        Task<UnitOfWorkResponseModel<DataUserModel>> GetUserByIdWithRuntimeDatas(Guid id);
    }
}