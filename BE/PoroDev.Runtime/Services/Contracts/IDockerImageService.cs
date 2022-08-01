using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;

namespace PoroDev.Runtime.Services.Contracts
{
    public interface IDockerImageService
    {
        Task<DockerRuntimeException> CreateDockerImage(string imageName);

        Task<CommunicationModel<RuntimeData>> CreateAndRunDockerImage(Guid userId, string projectId);

        Task<CommunicationModel<RuntimeData>> CreateAndRunDockerImageWithParameteres(List<string> argumentList, Guid userId, string projectId);

        Task<DockerRuntimeException> CreateDockerfile();

        Task<DockerRuntimeException> CreateDockerfile(List<string> argumentList);

        Task<DockerRuntimeException> GetProcessedOutput(Guid userId);

        Task<string> RunDockerImageUnsafe(string imageName);

        Task<string> RunDockerImageUnsafeWithArguments(string imageName, List<string> args);

        Task<DockerRuntimeException> DeleteDockerImage(string imageName);

        DockerRuntimeException Initialize(string runtimeFolderPath);
    }
}