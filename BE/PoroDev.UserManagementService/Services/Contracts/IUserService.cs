using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Enums;
using PoroDev.UserManagementService.Models.UserModels;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<UserCreateResponseDatabaseToService> CreateUser(UserCreateRequestGatewayToService model);

        //Task<DataUserModel> GetUserByMail(string mail);


        Task<UserUpdateResponseDatabaseToService> UpdateUser(UserUpdateRequestGatewayToService model);

        //Task<UserCreateModelGateway> DeleteUser(string mail);

        //Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, UserEnums.UserRole role);

        //Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel);
    }
}