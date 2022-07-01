using PoroDev.Common.Exceptions;

namespace PoroDev.Runtime.Extensions.Contracts
{
    public interface IDockerImageService
    {
        Task<DockerRuntimeException> CreateDockerImage(string imageName);

        Task<List<string>> CheckAndCreateDockerfile(List<string> argumentList, IDockerImageService _dockerImageService, IZipManipulator _zipManipulator);

        Task<DockerRuntimeException> CreateDockerfile();

        Task<DockerRuntimeException> CreateDockerfile(List<string> argumentList);

        Task<DockerRuntimeException> GetProcessedOutput(); 

        Task<string> RunDockerImageUnsafe(string imageName);

        Task<string> RunDockerImageUnsafeWithArguments(string imageName, List<string> args);

        Task<DockerRuntimeException> DeleteDockerImage(string imageName);

        DockerRuntimeException Initialize(string runtimeFolderPath);
    }
}
