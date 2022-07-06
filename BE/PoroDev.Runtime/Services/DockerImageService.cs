using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Services.Contracts;
using System.Diagnostics;
using static PoroDev.Runtime.Constants.Consts;

namespace PoroDev.Runtime.Services
{
    public class DockerImageService : RuntimePathsAbstract, IDockerImageService
    {
        private readonly IZipManipulator _zipManipulator;

        public DockerImageService(IZipManipulator zipManipulator) : base()
        {
            _zipManipulator = zipManipulator;
        }

        public async Task<List<string>> CheckAndCreateDockerfile(List<string> argumentList)
        {
            var lastIndex = argumentList.FindLastIndex(x => Guid.TryParse(x, out Guid rand));
            var argumentListWithFileNames = new List<string>(argumentList);
            for (int i = 0; i <= lastIndex; i++)
            {
                argumentListWithFileNames[i] = "image.jpg";
            }
            if (lastIndex != -1)
            {
                File.Copy(Path.Combine(RUNTIME_FOLDER_ROUTE, "image.jpg"), Path.Combine(_zipManipulator.ProjectPath, "image.jpg"));
                await CreateDockerfile(argumentListWithFileNames);
            }
            else
            {
                await CreateDockerfile();
            }

            return argumentListWithFileNames;
        }

        public async Task<CommunicationModel<RuntimeData>> CreateAndRunDockerImage(Guid userId, Guid projectId)
        {
            var imageName = Guid.NewGuid().ToString();
            Stopwatch stopwatch = new();

            await CreateDockerfile();

            await CreateDockerImage(imageName);

            DateTimeOffset dateStarted = DateTimeOffset.UtcNow;

            stopwatch.Start();

            string imageOutput = string.Empty;

            try
            {
                imageOutput = await RunDockerImageUnsafe(imageName);
            }
            catch (DockerRuntimeException ex)
            {
                stopwatch.Stop();

                await DeleteDockerImage(imageName);

                _zipManipulator.DeleteUnzippedFile();

                var responseModel = new CommunicationModel<RuntimeData>(ex);

                return responseModel;
            }

            stopwatch.Stop();

            await DeleteDockerImage(imageName);

            RuntimeData newRuntimeData = new(userId, projectId, dateStarted, stopwatch.ElapsedMilliseconds, imageOutput);

            var responsemodel = new CommunicationModel<RuntimeData>()
            {
                Entity = newRuntimeData
            };

            return responsemodel;
        }

        public async Task<CommunicationModel<RuntimeData>> CreateAndRunDockerImageWithParameteres(List<string> argumentList, Guid userId, Guid projectId)
        {
            var imageName = Guid.NewGuid().ToString();
            Stopwatch stopwatch = new();

            List<string> fileNames = await CheckAndCreateDockerfile(argumentList);

            await CreateDockerImage(imageName);

            DateTimeOffset dateStarted = DateTimeOffset.UtcNow;

            stopwatch.Start();

            string imageOutput = string.Empty;

            try
            {
                imageOutput = await RunDockerImageUnsafeWithArguments(imageName, fileNames);
            }
            catch (DockerRuntimeException ex)
            {
                stopwatch.Stop();

                await DeleteDockerImage(imageName);

                _zipManipulator.DeleteUnzippedFile();

                var responseModel = new CommunicationModel<RuntimeData>(ex);

                return responseModel;
            }

            stopwatch.Stop();

            if (argumentList.FindLastIndex(x => Guid.TryParse(x, out Guid rand)) != -1)
                await GetProcessedOutput();

            await DeleteDockerImage(imageName);

            string argumentsAsString = string.Empty;

            foreach (var argument in argumentList)
            {
                argumentsAsString += argument + "|";
            }

            argumentsAsString = argumentsAsString.Remove(argumentsAsString.Length - 1);

            RuntimeData newRuntimeData = new(userId, projectId, dateStarted, stopwatch.ElapsedMilliseconds, imageOutput, argumentsAsString);

            var responsemodel = new CommunicationModel<RuntimeData>()
            {
                Entity = newRuntimeData
            };

            return responsemodel;
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
            string imageOutput = string.Empty;

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
            string imageOutput = string.Empty;

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