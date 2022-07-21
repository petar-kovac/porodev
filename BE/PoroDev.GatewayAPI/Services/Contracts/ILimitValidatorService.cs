using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface ILimitValidatorService
    {
        Task ValidateUpload(Guid userId, long uploadRequestSize);

        Task ValidateDownload(Guid userId, string fileId);

        Task ValidateRuntime(Guid userId);

    }
}
