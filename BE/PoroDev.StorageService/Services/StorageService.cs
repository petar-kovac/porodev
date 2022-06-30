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
        private readonly IRequestClient<FileDownloadModel> _downloadRequestClient;
      
        public StorageService(IRequestClient<FileUploadRequestServiceToDatabase> uploadRequestClient, IRequestClient<FileDownloadModel> downloadRequestClient)
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
    }
}
