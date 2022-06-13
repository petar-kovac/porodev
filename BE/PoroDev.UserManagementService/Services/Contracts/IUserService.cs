using PoroDev.Common.Contracts.Create;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<UserCreateResponseDatabaseToService> CreateUser(UserCreateRequestGatewayToService model);

        //Task<DataUserModel> GetUserByMail(string mail);

        //Task<UserCreateModelGateway> UpdateUser(UserCreateModelGateway model);

        //Task<UserCreateModelGateway> DeleteUser(string mail);

        //Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, UserEnums.UserRole role);

        //Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel);
    }
}