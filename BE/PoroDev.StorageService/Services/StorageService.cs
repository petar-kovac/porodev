using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DeleteFile;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;

using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Services
{
    public class StorageService : IStorageService
    {
        private readonly IRequestClient<FileUploadRequestServiceToDatabase> _uploadRequestClient;
        private readonly IRequestClient<FileDownloadRequestServiceToDatabase> _downloadRequestClient;
        private readonly IRequestClient<FileReadModel> _readRequestClient;
        private readonly IRequestClient<FileDeleteRequestServiceToDatabase> _deleteRequestClient;

        public StorageService
            (IRequestClient<FileUploadRequestServiceToDatabase> uploadRequestClient,
            IRequestClient<FileDownloadRequestServiceToDatabase> downloadRequestClient,
            IRequestClient<FileReadModel> readRequestClient,
            IRequestClient<FileDeleteRequestServiceToDatabase> deleteRequestClient)
        {
            _uploadRequestClient = uploadRequestClient;
            _downloadRequestClient = downloadRequestClient;
            _readRequestClient = readRequestClient;
            _deleteRequestClient = deleteRequestClient;
        }

        public async Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestServiceToDatabase uploadModel)
        {
            var response = await _uploadRequestClient.GetResponse<CommunicationModel<FileUploadModel>>(uploadModel);

            return response.Message;
        }

        public async Task<CommunicationModel<FileDownloadMessage>> DownloadFile(FileDownloadRequestServiceToDatabase downloadModel)
        {
            var response = await _downloadRequestClient.GetResponse<CommunicationModel<FileDownloadMessage>>(downloadModel);

            return response.Message;
        }

        public async Task<CommunicationModel<FileReadModel>> ReadFiles(FileReadRequestServiceToDatabase readModel)
        {
            var response = await _readRequestClient.GetResponse<CommunicationModel<FileReadModel>>(readModel);

            return response.Message;
        }

        public async Task<CommunicationModel<FileDeleteMessage>> DeleteFile(FileDeleteRequestServiceToDatabase deleteModel)
        {
            var response = await _deleteRequestClient.GetResponse<CommunicationModel<FileDeleteMessage>>(deleteModel);

            return response.Message;
        }
    }
}