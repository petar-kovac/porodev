using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;

namespace PoroDev.StorageService.Services.Contracts
{
    public interface IStorageService
    {
        Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestGatewayToService uploadModel);

        Task<CommunicationModel<FileDownloadModel>> DownloadFile(FileDownloadRequestGatewayToService downloadModel);
    }
}