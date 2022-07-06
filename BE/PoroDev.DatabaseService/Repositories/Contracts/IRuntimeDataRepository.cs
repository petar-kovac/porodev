using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.UnitOfWorkResponse;

namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IRuntimeDataRepository : IGenericRepository<RuntimeData>
    {
        Task<UnitOfWorkResponseModel<List<RuntimeData>>> GetRuntimeDatasByUserId(RuntimeQueryRequestGatewayToDatabase queryRequest);
    }
}