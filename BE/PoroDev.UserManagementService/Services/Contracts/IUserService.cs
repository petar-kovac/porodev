using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Enums;
using PoroDev.UserManagementService.Models.UserModels;
using PoroDev.Common.Contracts.DeleteUser;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<UserCreateResponseDatabaseToService> CreateUser(UserCreateRequestGatewayToService model);

        Task<UserReadByEmailResponseDatabaseToService> ReadUserByEmail(UserReadByEmailRequestGatewayToService model);

        //Task<DataUserModel> GetUserByMail(string mail);

        //Task<UserCreateModelGateway> UpdateUser(UserCreateModelGateway model);

        Task<UserDeleteResponseDatabaseToService> DeleteUser(UserDeleteRequestGatewayToService model);

        //Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, UserEnums.UserRole role);

        //Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel);
    }
}