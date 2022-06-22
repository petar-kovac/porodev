using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Contracts.UserMenagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IUserManagementService
    {
        Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel);

        Task<DeleteUserModel> DeleteUser(UserDeleteRequestGatewayToService deleteModel);

        Task<LoginUserModel> LoginUser(UserLoginRequestGatewayToService loginModel);

        Task<DataUserModel> ReadUserByEmail(string email);

        Task<DataUserModel> ReadUserById(UserReadByIdRequestGatewayToService readModel);

        Task<DataUserModel> UpdateUser(UserUpdateRequestGatewayToService updateModel);

        Task<RegisterUserResponse> RegisterUser(RegisterUserRequestGatewayToService registerModel);

    }
}