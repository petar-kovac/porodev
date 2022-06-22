using PoroDev.Common.Models.StorageModels.DownloadFile;
using PoroDev.Common.Models.StorageModels.UploadFile;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IStorageService
    {
        Task<FileUploadModel> Upload(FileUploadModel uploadModel);

        Task<FileDownloadModel> Download(FileDownloadModel downloadModel);
    }
}
