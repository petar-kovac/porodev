using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Models.RuntimeModels.Data;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IRuntimeQueryService
    {
        Task<List<RuntimeData>> Query(RuntimeQueryRequestGatewayToDatabase query);
    }
}
