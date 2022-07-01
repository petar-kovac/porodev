using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.UploadFile;

namespace PoroDev.StorageService.Services.Contracts
{
    public interface IStorageService
    {
        // Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model);
        Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestGatewayToService uploadModel);

        Task<CommunicationModel<FileDownloadMsg>> DownloadFile(FileDownloadMsg downloadModel);
    }
}