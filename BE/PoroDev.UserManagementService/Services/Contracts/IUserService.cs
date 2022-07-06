using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.Create;
using PoroDev.Common.Contracts.UserManagement.DeleteAllUsers;
using PoroDev.Common.Contracts.UserManagement.DeleteUser;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime;
using PoroDev.Common.Contracts.UserManagement.ReadUser;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model);

        Task<CommunicationModel<DataUserModel>> ReadUserByEmail(UserReadByEmailRequestGatewayToService model);

        Task<CommunicationModel<DeleteUserModel>> DeleteAllUsers(UserDeleteAllRequestGatewayToService model);

        Task<CommunicationModel<DataUserModel>> ReadUserById(UserReadByIdRequestGatewayToService model);

        Task<CommunicationModel<DataUserModel>> ReadUserByIdWithRuntimeData(UserReadByIdWithRuntimeRequestGatewayToService model);

        Task<CommunicationModel<DataUserModel>> UpdateUser(UserUpdateRequestGatewayToService model);

        Task<CommunicationModel<DeleteUserModel>> DeleteUser(UserDeleteRequestGatewayToService model);

        Task<CommunicationModel<RegisterUserResponse>> RegisterUser(RegisterUserRequestGatewayToService registerModel);

        Task<CommunicationModel<LoginUserModel>> LoginUser(UserLoginRequestGatewayToService userToLoginModel);
    }
}