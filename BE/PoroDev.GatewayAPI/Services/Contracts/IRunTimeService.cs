using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.GatewayAPI.Models.Runtime;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IRunTimeService
    {
        Task<RuntimeData> ExecuteProgram(ExecuteProjectRequestClientToGateway model);

        Task<RuntimeData> ExecuteProgramWithArguments(ArgumentListWithJwt model);
    }
}
