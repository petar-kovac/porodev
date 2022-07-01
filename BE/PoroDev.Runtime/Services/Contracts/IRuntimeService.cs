using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;

namespace PoroDev.Runtime.Services.Contracts
{
    public interface IRuntimeService
    {
        Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId);

        Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId, List<string> argumentList);
    }
}
