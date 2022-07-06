﻿using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Exceptions;
using PoroDev.GatewayAPI.Services.Contracts;
using static PoroDev.GatewayAPI.Constants.Constats;
using static PoroDev.GatewayAPI.Helpers.ExceptionFactory;

namespace PoroDev.GatewayAPI.Services
{
    public class StorageService : IStorageService
    {
        private readonly IRequestClient<FileDownloadRequestGatewayToService> _downloadRequestClient;
        private readonly IRequestClient<FileUploadRequestGatewayToService> _uploadRequestClient;
        private readonly IRequestClient<FileReadRequestGatewayToService> _readRequestClient;

        public StorageService
            (IRequestClient<FileDownloadRequestGatewayToService> downloadRequestClient, IRequestClient<FileUploadRequestGatewayToService> uploadRequestClient, 
            IRequestClient<FileReadRequestGatewayToService> readRequestClient)
        {
            _downloadRequestClient = downloadRequestClient;
            _uploadRequestClient = uploadRequestClient;
            _readRequestClient = readRequestClient;
        }

        public async Task<FileUploadModel> UploadFile(FileUploadRequestGatewayToService uploadModel)
        {
            if (uploadModel.File is null)
            {
                ThrowException(nameof(FileUploadFormatException), FileUploadExceptionMessage);
            }

            var responseContext = await _uploadRequestClient.GetResponse<CommunicationModel<FileUploadModel>>(uploadModel);

            if (responseContext.Message.ExceptionName != null)
                ThrowException(responseContext.Message.ExceptionName, responseContext.Message.HumanReadableMessage);

            return responseContext.Message.Entity;
        }

        public async Task<FileDownloadMessage> DownloadFile(FileDownloadRequestGatewayToService downloadModel)
        {
            var responseContext = await _downloadRequestClient.GetResponse<CommunicationModel<FileDownloadMessage>>(downloadModel);

            return responseContext.Message.Entity;
        }

        public async Task<FileReadModel> ReadFiles(FileReadRequestGatewayToService readModel)
        {
            var responseContext = await _readRequestClient.GetResponse<CommunicationModel<FileReadModel>>(readModel);
            var response = responseContext.Message.Entity;

            return response;
        }
    }
}