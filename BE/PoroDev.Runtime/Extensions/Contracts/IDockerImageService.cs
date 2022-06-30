using PoroDev.Common.Exceptions;

namespace PoroDev.Runtime.Extensions.Contracts
{
    public interface IDockerImageService
    {
        Task<DockerRuntimeException> CreateDockerImage(string imageName);

        Task<DockerRuntimeException> CreateDockerfile();

        Task<string> RunDockerImageUnsafe(string imageName);

        Task<DockerRuntimeException> DeleteDockerImage(string imageName);

        DockerRuntimeException Initialize(string runtimeFolderPath);
    }
}
