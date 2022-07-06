using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Services.Contracts;

namespace PoroDev.Runtime.Services
{
    public class RuntimeService : IRuntimeService
    {
        private readonly IRequestClient<RuntimeData> _createRequestClient;
        private readonly IMapper _mapper;
        private readonly IDockerImageService _dockerImageService;
        private readonly IZipManipulator _zipManipulator;
        private readonly IRuntimeHelper _runtimeHelper;

        public RuntimeService(IRequestClient<RuntimeData> createRequestClient,
            IMapper mapper,
            IDockerImageService dockerImageService,
            IRuntimeHelper runtimeHelper,
            IZipManipulator zipManipulator)
        {
            _createRequestClient = createRequestClient;
            _mapper = mapper;
            _runtimeHelper = runtimeHelper;
            _dockerImageService = dockerImageService;
            _zipManipulator = zipManipulator;
        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId)
        {
            var exceptionThatHappened = _runtimeHelper.InitializeAndExtract();
            if (exceptionThatHappened != null)
                return exceptionThatHappened;

            var runTimeResult = await _dockerImageService.CreateAndRunDockerImage(userId, projectId);

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
            var exceptionThatHappened = _runtimeHelper.InitializeAndExtract();
            if (exceptionThatHappened != null)
                return exceptionThatHappened;

            var runtimeResult = await _dockerImageService.CreateAndRunDockerImageWithParameteres(argumentList, userId, projectId);
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