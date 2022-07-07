using MassTransit;
using MongoDB.Bson;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Services.Contracts;
using static PoroDev.Runtime.Constants.Consts;

namespace PoroDev.Runtime.Services
{
    public class RuntimeHelper : IRuntimeHelper
    {
        private readonly IZipManipulator _zipManipulator;

        private readonly IDockerImageService _dockerImageService;

        private readonly IRequestClient<FileDownloadRequestServiceToDatabase> _downloadFileRequestClient;

        public RuntimeHelper(IZipManipulator zipManipulator,
            IDockerImageService dockerImageService,
            IRequestClient<FileDownloadRequestServiceToDatabase> downloadFileRequestClient)
        {
            _zipManipulator = zipManipulator;
            _dockerImageService = dockerImageService;
            _downloadFileRequestClient = downloadFileRequestClient;
        }

        public async Task<CommunicationModel<List<String>>> InitializeFileArguments(List<string> argList, Guid userId)
        {
            var lastIndex = argList.FindLastIndex(x => ObjectId.TryParse(x, out ObjectId rand));

            for (int i = 0; i <= lastIndex; i++)
            {
                FileDownloadRequestServiceToDatabase requestToDb = new(argList[i], userId);

                var fileContext = await _downloadFileRequestClient.GetResponse<CommunicationModel<FileDownloadMessage>>(requestToDb);

                argList[i] = fileContext.Message.Entity.FileName;

                await File.WriteAllBytesAsync(Path.Combine(_zipManipulator.ProjectPath, $"{fileContext.Message.Entity.FileName}"), fileContext.Message.Entity.File);
            }

            return new CommunicationModel<List<string>>(argList);
        }

        public async Task<CommunicationModel<RuntimeData>> InitializeAndExtract(string projectId, Guid userId)
        {
            if (!Directory.Exists(RUNTIME_FOLDER_ROUTE))
                System.IO.Directory.CreateDirectory(RUNTIME_FOLDER_ROUTE);

            FileDownloadRequestServiceToDatabase requestToDb = new(projectId, userId);

            var projectContext = await _downloadFileRequestClient.GetResponse<CommunicationModel<FileDownloadMessage>>(requestToDb);

            await File.WriteAllBytesAsync(Path.Combine(RUNTIME_FOLDER_ROUTE, $"{projectContext.Message.Entity.FileName}"), projectContext.Message.Entity.File);

            ZippedFileException pathException = _zipManipulator.Initialize(RUNTIME_FOLDER_ROUTE);

            if (pathException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(pathException);

                return responseModel;
            }

            DockerRuntimeException dockerException = _dockerImageService.Initialize(RUNTIME_FOLDER_ROUTE);

            if (dockerException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(dockerException);

                return responseModel;
            }

            ZippedFileException extractionException = _zipManipulator.ExtractZipToPath();

            if (extractionException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(extractionException);

                return responseModel;
            }

            return null;
        }
    }
}