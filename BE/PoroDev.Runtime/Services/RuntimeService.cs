using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Extensions;
using PoroDev.Runtime.Extensions.Contracts;
using PoroDev.Runtime.Services.Contracts;

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
            IZipManipulator zipManipulator)
        {
            _createRequestClient = createRequestClient;
            _mapper = mapper;
            _dockerImageService = dockerImageService;
            _zipManipulator = zipManipulator;
        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId)
        {
            var exceptionThatHappened = ExceptionHelper.InitializeAndExtract(_zipManipulator, _dockerImageService);
            if (exceptionThatHappened != null)
                return exceptionThatHappened;

            var runTimeResult = await _dockerImageService.CreateAndRunDockerImage(_zipManipulator, userId, projectId);

            var deleteException = _zipManipulator.DeleteUnzippedFile();

            if (deleteException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(deleteException);

                return responseModel;
            }

            var dbResponse = await _createRequestClient.GetResponse<CommunicationModel<RuntimeData>>(runTimeResult.Entity);
            return dbResponse.Message;
        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId, List<string> argumentList)
        {
            var exceptionThatHappened = ExceptionHelper.InitializeAndExtract(_zipManipulator, _dockerImageService);
            if (exceptionThatHappened != null)
                return exceptionThatHappened;

            var runtimeResult = await _dockerImageService.CreateAndRunDockerImageWithParameteres(argumentList, _zipManipulator, userId, projectId);
            var deleteException = _zipManipulator.DeleteUnzippedFile();

            if (deleteException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(deleteException);

                return responseModel;
            }

            var dbResponse = await _createRequestClient.GetResponse<CommunicationModel<RuntimeData>>(runtimeResult.Entity);
            return dbResponse.Message;
        }
    }
}