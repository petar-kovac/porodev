using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;

namespace PoroDev.Runtime.Services.Contracts
{
    public interface IRuntimeHelper
    {
        Task<CommunicationModel<RuntimeData>> InitializeAndExtract(string projectId, Guid userId);

        Task<CommunicationModel<List<String>>> InitializeFileArguments(List<string> argList, Guid userId);
    }
}