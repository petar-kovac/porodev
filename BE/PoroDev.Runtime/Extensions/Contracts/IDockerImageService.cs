namespace PoroDev.Runtime.Extensions.Contracts
{
    public interface IDockerImageService
    {
        Task CreateDockerImage(string imageName, string runtimePath);

        Task<string> RunDockerImage(string imageName, string runtimePath);

        Task DeleteDockerImage(string imageName);

        Exception SetFolderPath(string runtimeFolderPath)
    }
}
