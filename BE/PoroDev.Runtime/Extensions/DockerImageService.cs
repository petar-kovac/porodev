using PoroDev.Common.Exceptions;
using PoroDev.Runtime.Extensions.Contracts;
using System.Diagnostics;
using static PoroDev.Runtime.Constants.Consts;

namespace PoroDev.Runtime.Extensions
{
    public class DockerImageService : RuntimePathsAbstract, IDockerImageService
    {
        public DockerImageService() : base()
        {

        }

        public async Task<DockerRuntimeException> CreateDockerfile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(ProjectPath, "Dockerfile")))
                {
                    await writer.WriteLineAsync(@"FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env");
                    await writer.WriteLineAsync(@"WORKDIR /app");

                    await writer.WriteLineAsync(@"COPY . ./");
                    await writer.WriteLineAsync(@"RUN dotnet restore");
                    await writer.WriteLineAsync(@"RUN dotnet publish -c Release -o out");

                    await writer.WriteLineAsync(@"FROM mcr.microsoft.com/dotnet/aspnet:6.0");
                    await writer.WriteLineAsync(@"COPY --from=build-env /app/out .");
                    await writer.WriteLineAsync(@$"ENTRYPOINT [""dotnet"", ""{ZippedFileName}.dll""]");
                }
            }
            catch (Exception ex)
            {
                var createImageException = (DockerRuntimeException)ex;
                createImageException.HumanReadableErrorMessage = "Exception happened while creating docker image, check process path?";

                return createImageException;
            }

            return null;
        }

        public async Task<DockerRuntimeException> CreateDockerfile(List<string> argumentList)
        {
            
            try
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(ProjectPath, "Dockerfile")))
                {
                    await writer.WriteLineAsync(@"FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env");
                    await writer.WriteLineAsync(@"WORKDIR /app");

                    await writer.WriteLineAsync(@"COPY . ./");
                    await writer.WriteLineAsync(@"RUN dotnet restore");
                    await writer.WriteLineAsync(@"RUN dotnet publish -c Release -o out");

                    await writer.WriteLineAsync(@"FROM mcr.microsoft.com/dotnet/aspnet:6.0");
                    await writer.WriteLineAsync(@"COPY --from=build-env /app/out .");
                    foreach (var item in argumentList)
                    {
                        await writer.WriteLineAsync($@"ADD {item} /");
                    }
                    await writer.WriteLineAsync(@$"ENTRYPOINT [""dotnet"", ""{ZippedFileName}.dll""]");
                }
            }
            catch (Exception ex)
            {
                var createImageException = (DockerRuntimeException)ex;
                createImageException.HumanReadableErrorMessage = "Exception happened while creating docker image, check process path?";

                return createImageException;
            }

            return null;
        }

        public async Task<DockerRuntimeException> CreateDockerImage(string imageName)
        {
            try
            {
                using (var processTest = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "CMD.exe",
                        Arguments = $"/C docker build -t {imageName} -f Dockerfile .",
                        UseShellExecute = false,
                        WorkingDirectory = ProjectPath
                    }
                })
                {
                    processTest.Start();
                    await processTest.WaitForExitAsync();
                }
            }
            catch (Exception ex)
            {
                var createImageException = (DockerRuntimeException)ex;
                createImageException.HumanReadableErrorMessage = "Exception happened while building docker image, check process path?";

                return createImageException;
            }

            return null;
        }

        public async Task<DockerRuntimeException> DeleteDockerImage(string imageName)
        {
            try
            {
                await Process.Start("CMD.exe", "/C docker rm runtime-container").WaitForExitAsync();

                await Process.Start("CMD.exe", $"/C docker image rm {imageName}").WaitForExitAsync();
            }
            catch (Exception ex)
            {
                var createImageException = (DockerRuntimeException)ex;
                createImageException.HumanReadableErrorMessage = DELETE_DOCKER_FILE_EXCEPTION;

                return createImageException;
            }

            return null;
        }

        public async Task<DockerRuntimeException> GetProcessedOutput()
        {
            try
            {
                await Process.Start("CMD.exe", $"/C docker cp runtime-container:/imageEdited.jpg {RUNTIME_FOLDER_ROUTE}").WaitForExitAsync();
            }
            catch (Exception ex)
            {
                var createImageException = (DockerRuntimeException)ex;
                createImageException.HumanReadableErrorMessage = GET_FILE_OUTPUT_EXCEPTION;

                return createImageException;
            }
            return null;
        }

        public DockerRuntimeException Initialize(string runtimeFolderPath)
        {
            return (DockerRuntimeException)SetFolderPath(runtimeFolderPath);
        }

        public async Task<string> RunDockerImageUnsafe(string imageName)
        {
            string imageOutput = String.Empty;

            try
            {
                using (var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "CMD.exe",
                        Arguments = $"/C docker run --name runtime-container {imageName}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WorkingDirectory = ProjectPath
                    }
                })
                {

                    proc.Start();
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        imageOutput = await proc.StandardOutput.ReadLineAsync();
                    }
                    await proc.WaitForExitAsync();
                }
            }
            catch (Exception ex)
            {
                var createImageException = (DockerRuntimeException)ex;
                createImageException.HumanReadableErrorMessage = "Exception happened while executing docker image, check process path?";

                throw createImageException;
            }

            return imageOutput;
        }

        public async Task<string> RunDockerImageUnsafeWithArguments(string imageName, List<string> args)
        {
            string imageOutput = String.Empty;

            string listOfArgs = "";
            foreach (var arg in args)
                listOfArgs += " " + arg;

            try
            {
                using (var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "CMD.exe",
                        Arguments = $"/C docker run --name runtime-container {imageName}{listOfArgs}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WorkingDirectory = ProjectPath
                    }
                })
                {

                    proc.Start();
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        imageOutput = await proc.StandardOutput.ReadLineAsync();
                    }
                    await proc.WaitForExitAsync();
                }
            }
            catch (Exception ex)
            {
                var createImageException = (DockerRuntimeException)ex;
                createImageException.HumanReadableErrorMessage = "Exception happened while executing docker image, check process path?";

                throw createImageException;
            }

            return imageOutput;
        }
    }
}
