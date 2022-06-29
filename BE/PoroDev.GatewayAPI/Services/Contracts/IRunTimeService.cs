using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.RunTime.ParametersExecute;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.Common.Models.RuntimeModels.Data;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IRunTimeService
    {
        Task<RuntimeData> ExecuteProgram(ExecuteProjectRequestClientToGateway model);

        Task<RuntimeData> ExecuteProgramWithArguments(ArgumentListWithJwt model);
    }
}
