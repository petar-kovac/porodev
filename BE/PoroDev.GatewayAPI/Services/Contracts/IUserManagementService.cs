using PoroDev.Common.Models.UserModels.Create;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IUserManagementService
    {
        Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel);
    }
}
