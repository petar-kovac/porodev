using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;

using PoroDev.StorageService.Services.Contracts;

namespace PoroDev.StorageService.Services
{
    public class StorageService : IStorageService
    {
        private readonly IRequestClient<FileUploadRequestServiceToDatabase> _uploadRequestClient;
        private readonly IRequestClient<FileDownloadMsg> _downloadRequestClient;

        public StorageService(IRequestClient<FileUploadRequestServiceToDatabase> uploadRequestClient, IRequestClient<FileDownloadMsg> downloadRequestClient)
        {
            _uploadRequestClient = uploadRequestClient;
            _downloadRequestClient = downloadRequestClient;
        }

        public async Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestServiceToDatabase uploadModel)
        {
            //FileUploadModel model = new(uploadModel.FileName, uploadModel.File, uploadModel.UserId);
        
            var response = await _uploadRequestClient.GetResponse<CommunicationModel<FileUploadModel>>(uploadModel);
            return response.Message;
        }

        public async Task<CommunicationModel<FileDownloadMsg>> DownloadFile(FileDownloadMsg downloadModel)
        {
            FileDownloadMsg model = new(downloadModel.FileName);

            var response = await _downloadRequestClient.GetResponse<CommunicationModel<FileDownloadMsg>>(model);

            return response.Message;

        }
    }
}