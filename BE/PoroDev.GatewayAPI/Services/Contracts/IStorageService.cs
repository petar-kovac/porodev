using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;


namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IStorageService
    {
        Task<FileUploadModel> UploadFile(FileUploadRequestGatewayToService uploadModel);

        Task<FileDownloadMessage> DownloadFile(FileDownloadRequestGatewayToService downloadModel);
    }
}
