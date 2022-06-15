using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.LoginUser;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Enums;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.RegisterUser;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model);

        Task<CommunicationModel<DataUserModel>> ReadUserByEmail(UserReadByEmailRequestGatewayToService model);

        //Task<DataUserModel> GetUserByMail(string mail);



        Task<UserUpdateResponseDatabaseToService> UpdateUser(UserUpdateRequestGatewayToService model);

        Task<CommunicationModel<DeleteUserModel>> DeleteUser(UserDeleteRequestGatewayToService model);

        Task<CommunicationModel<RegisterUserResponse>> RegisterUser(RegisterUserRequestGatewayToService registerModel);

        //Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel);
        Task<CommunicationModel<LoginUserModel>> LoginUser(UserLoginRequestGatewayToService userToLoginModel);
    }
}