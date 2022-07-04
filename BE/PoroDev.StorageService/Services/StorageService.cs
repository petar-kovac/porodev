using MassTransit;
using PoroDev.Common.Contracts;
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
        private readonly IRequestClient<FileReadRequestServiceToDatabase> _readRequestClient;

        public StorageService
            (IRequestClient<FileUploadRequestServiceToDatabase> uploadRequestClient, IRequestClient<FileDownloadRequestServiceToDatabase> downloadRequestClient, 
            IRequestClient<FileReadRequestServiceToDatabase> readRequestClient)
        {
            _uploadRequestClient = uploadRequestClient;
            _downloadRequestClient = downloadRequestClient;
            _readRequestClient = readRequestClient;
        }

        public async Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestServiceToDatabase uploadModel)
        {
            //FileUploadModel model = new(uploadModel.FileName, uploadModel.File, uploadModel.UserId);
        
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
    }
}