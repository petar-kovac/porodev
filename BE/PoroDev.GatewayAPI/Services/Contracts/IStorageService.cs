using PoroDev.Common.Models.StorageModels.DownloadFile;
using PoroDev.Common.Models.StorageModels.UploadFile;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IStorageService
    {
        Task<FileUploadModel> UploadFile(FileUploadRequestGatewayToService uploadModel);

        Task<FileDownloadModel> DownloadFile(FileDownloadModel downloadModel);
    }
}
