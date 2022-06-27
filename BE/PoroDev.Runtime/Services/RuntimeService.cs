using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Extensions.Contracts;
using PoroDev.Runtime.Services.Contracts;
using System.Diagnostics;
using static PoroDev.Runtime.Constants.Consts;
using static PoroDev.Runtime.Extensions.DockerWriter;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using PoroDev.Runtime.Extensions;

namespace PoroDev.Runtime.Services
{
    public class RuntimeService : IRuntimeService
    {

        private readonly IRequestClient<RuntimeData> _createRequestClient;
        private readonly IMapper _mapper;
        private readonly IDockerImageService _dockerImageService;
        private readonly IZipManipulator _zipManipulator;

        public RuntimeService(IRequestClient<RuntimeData> createRequestClient, 
            IMapper mapper, 
            IDockerImageService dockerImageService, 
            IZipManipulator  zipManipulator)
        {
            _createRequestClient = createRequestClient;
            _mapper = mapper;
            _dockerImageService = dockerImageService;
            _zipManipulator = zipManipulator;
        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId)
        {
            ZippedFileException pathException = _zipManipulator.Initialize(RUNTIME_FOLDER_ROUTE);

            if (pathException != null)
            {
                var responseModel = CreateResponseModel<RuntimeData>(pathException);

                return responseModel;
            }

            ZippedFileException extractionException = _zipManipulator.ExtractZipToPath();

            if(extractionException != null)
            {
                var responseModel = CreateResponseModel<RuntimeData>(extractionException);

                return responseModel;
            }

            var imageName = Guid.NewGuid().ToString();
            Stopwatch stopwatch = new();

            await CreateDockerfile(PROJECT_PATH);          

            await _dockerImageService.CreateDockerImage(imageName, PROJECT_PATH);

            DateTimeOffset dateStarted = DateTimeOffset.UtcNow;
            
            stopwatch.Start();

            string imageOutput = await _dockerImageService.RunDockerImage(imageName, PROJECT_PATH);

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

            var deleteException = _zipManipulator.DeleteUnzippedFile();

            if(deleteException != null)
            {
                var responseModel = CreateResponseModel<RuntimeData>(extractionException);

                return responseModel;
            }

            var dbResponse = await _createRequestClient.GetResponse<CommunicationModel<RuntimeData>>(newRuntimeData);

            return dbResponse.Message;

        }
    }
}