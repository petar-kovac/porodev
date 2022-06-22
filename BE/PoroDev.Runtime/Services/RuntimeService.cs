using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Services.Contracts;

namespace PoroDev.Runtime.Services
{
    public class RuntimeService : IRuntimeService
    {


        public Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid projectId)
        {
            throw new NotImplementedException();
        }


    }
}
