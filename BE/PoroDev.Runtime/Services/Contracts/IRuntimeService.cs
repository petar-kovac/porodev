using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;

namespace PoroDev.Runtime.Services.Contracts
{
    public interface IRuntimeService
    {
        Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid projectId);
    }
}
