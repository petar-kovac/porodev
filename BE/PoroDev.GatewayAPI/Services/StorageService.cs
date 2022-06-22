using MassTransit;
using PoroDev.Common.Models.StorageModels.DownloadFile;
using PoroDev.Common.Models.StorageModels.UploadFile;
using PoroDev.GatewayAPI.Services.Contracts;

namespace PoroDev.GatewayAPI.Services
{
    public class StorageService : IStorageService
    {
        private readonly IRequestClient<FileDownloadModel> _downloadRequestClient;
        private readonly IRequestClient<FileUploadModel> _uploadRequestClient;

        public StorageService(IRequestClient<FileDownloadModel> downloadRequestClient, IRequestClient<FileUploadModel> uploadRequestClient)
        {
            _downloadRequestClient = downloadRequestClient;
            _uploadRequestClient = uploadRequestClient;
        }

        public Task<FileDownloadModel> Download(FileDownloadModel downloadModel)
        {
            throw new NotImplementedException();
        }

        public Task<FileUploadModel> Upload(FileUploadModel uploadModel)
        {
           if(uploadModel.File == null || uploadModel.UserId == null)
            {

            }
        }
    }
}
