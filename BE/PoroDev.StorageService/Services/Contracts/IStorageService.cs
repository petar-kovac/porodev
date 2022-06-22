using PoroDev.Common.Contracts;
using PoroDev.Common.Models.StorageModels.UploadFile;

namespace PoroDev.StorageService.Services.Contracts
{
    public interface IStorageService
    {
        // Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model);
        Task<CommunicationModel<FileUploadModel>> UploadFile(FileUploadRequestGatewayToService uploadModel);
    }
}
