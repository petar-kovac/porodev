using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Enums;
using PoroDev.UserManagementService.Models.UserModels;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model);

        Task<UserReadByEmailResponseDatabaseToService> ReadUserByEmail(UserReadByEmailRequestGatewayToService model);

        //Task<DataUserModel> GetUserByMail(string mail);


        Task<UserUpdateResponseDatabaseToService> UpdateUser(UserUpdateRequestGatewayToService model);

        Task<UserDeleteResponseDatabaseToService> DeleteUser(UserDeleteRequestGatewayToService model);

        //Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, UserEnums.UserRole role);

        //Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel);
    }
}