using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Extensions.Contracts;
using PoroDev.Runtime.Services.Contracts;
using System.Diagnostics;
using static PoroDev.Runtime.Constants.Consts;
using static PoroDev.Runtime.Extensions.DockerWriter;

namespace PoroDev.Runtime.Services
{
    public class RuntimeService : IRuntimeService
    {

        private readonly IRequestClient<RuntimeData> _createRequestClient;
        private readonly IMapper _mapper;
        private readonly IDockerImageService _dockerImageService;

        public RuntimeService(IRequestClient<RuntimeData> createRequestClient, IMapper mapper, IDockerImageService dockerImageService)
        {
            _createRequestClient = createRequestClient;
            _mapper = mapper;
            _dockerImageService = dockerImageService;
        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId)
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(ZIPPED_FILE_ROUTE, RUNTIME_FOLDER_ROUTE);

            await CreateDockerfile(PROJECT_PATH);

            var imageName = Guid.NewGuid().ToString();

            await _dockerImageService.CreateDockerImage(imageName, PROJECT_PATH);

            DateTimeOffset dateStarted = DateTimeOffset.UtcNow;
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var imageOutput = await _dockerImageService.RunDockerImage(imageName, PROJECT_PATH);

            stopwatch.Stop();

            await _dockerImageService.DeleteDockerImage(imageName);

            RuntimeData newRuntimeData = new()
            {
                ExceptionHappened = imageOutput == "" ? true : false,
                ExecutionStart = dateStarted,
                ExecutionTime = imageOutput == String.Empty ? 0 : stopwatch.ElapsedMilliseconds,
                UserId = userId,
                FileId = projectId,
                Id = Guid.NewGuid(),
                ExecutionOutput = imageOutput
            };

            var dbResponse = await _createRequestClient.GetResponse<CommunicationModel<RuntimeData>>(newRuntimeData);

            Directory.Delete(Path.Combine(RUNTIME_FOLDER_ROUTE, ZIPPED_FILE_NAME), true);

            return dbResponse.Message;

        }

        public Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId, List<string> argumentList)
        {
            foreach (var argument in argumentList)
            {
                if (Guid.TryParse(argument, out Guid argumentId));
                    //postoji ID slike u argumentima
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(ZIPPED_FILE_ROUTE, RUNTIME_FOLDER_ROUTE);

            await CreateDockerfile(PROJECT_PATH);
        }
    }
}