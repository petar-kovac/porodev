using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Exceptions;
using PoroDev.GatewayAPI.Models.StorageService;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.Constats;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;
using static PoroDev.Common.MassTransit.Extensions;

namespace PoroDev.GatewayAPI.Services
{
    public class StorageService : IStorageService
    {
        private readonly IRequestClient<FileDownloadRequestGatewayToService> _downloadRequestClient;
        private readonly IRequestClient<IUploadRequest> _uploadRequestClient;
        private readonly IRequestClient<FileReadRequestGatewayToService> _readRequestClient;
        private readonly IRequestClient<FileDeleteRequestGatewayToService> _deleteRequestClient;
        private readonly IMapper _mapper;

        public StorageService
            (IRequestClient<FileDownloadRequestGatewayToService> downloadRequestClient,
            IRequestClient<IUploadRequest> uploadRequestClient,
            IRequestClient<FileReadRequestGatewayToService> readRequestClient,
            IRequestClient<FileDeleteRequestGatewayToService> deleteRequestClient,
            IMapper mapper)
        {
            _downloadRequestClient = downloadRequestClient;
            _uploadRequestClient = uploadRequestClient;
            _readRequestClient = readRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _mapper = mapper;
        }

        public async Task<FileUploadResponse> UploadFile(FileUploadRequest uploadModel)
        {
            if (uploadModel.File is null)
                ThrowException(nameof(FileUploadFormatException), FileUploadExceptionMessage);

            var fileUploadResponseContext = await _uploadRequestClient.GetResponse<CommunicationModel<FileUploadResponse>>(new
            {
                FileName = uploadModel.FileName,
                File = uploadModel.File,
                ContentType = uploadModel.ContentType,
                UserId = uploadModel.UserId
            });

            if (fileUploadResponseContext.Message.ExceptionName != null)
                ThrowException(fileUploadResponseContext.Message.ExceptionName, fileUploadResponseContext.Message.HumanReadableMessage);

            FileUploadResponse fileUploadResponse = fileUploadResponseContext.Message.Entity;

            return fileUploadResponse;
        }

        public async Task<FileDownloadResponse> DownloadFile(FileDownloadRequestGatewayToService downloadModel)
        {
            var responseContext = await _downloadRequestClient.GetResponse<CommunicationModel<FileDownloadMessage>>(downloadModel);

            if (responseContext.Message.ExceptionName != null)
            {
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);
            }
            var response = _mapper.Map<FileDownloadResponse>(responseContext.Message.Entity);
            response.File = await responseContext.Message.Entity.File.Value;

            return response;
        }

        public async Task<FileReadModel> ReadFiles(FileReadRequestGatewayToService readModel)
        {
            var responseContext = await _readRequestClient.GetResponse<CommunicationModel<FileReadModel>>(readModel);
            var response = responseContext.Message.Entity;

            return response;
        }

        public async Task<FileDeleteMessage> DeleteFile(FileDeleteRequestGatewayToService deleteModel)
        {
            var responseContext = await _deleteRequestClient.GetResponse<CommunicationModel<FileDeleteMessage>>(deleteModel);

            if (responseContext.Message.ExceptionName != null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            var response = responseContext.Message.Entity;

            return response;
        }
    }
}